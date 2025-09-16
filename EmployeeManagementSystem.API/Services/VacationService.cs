using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Services
{
    public class VacationService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VacationService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // -----------------------------
        // Employee vacation requests
        // -----------------------------
        public async Task<int> CreateVacationRequestAsync(int EmployeeId, DateTime StartDate, DateTime EndDate)
        {
            
            var req = new VacationRequest
            {
                EmployeeID = EmployeeId,
                RequestDate = DateTime.UtcNow,
                StartDate = StartDate,
                EndDate = EndDate,
                Status = "Pending",
                AwaitingApproval = (int)await GetImmediateManagerIdAsync(EmployeeId)
            };

            _context.VacationRequest.Add(req);
            await _context.SaveChangesAsync();
            return req.RequestID;
        }
        public async Task<(bool canTake, int neededDays, decimal availableDays)>
        CanTakeVacationAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            // 1. Count working days in range
            int workingDays = 0;
            for (var date = startDate.Date; date <= endDate.Date; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Friday && date.DayOfWeek != DayOfWeek.Saturday)
                    workingDays++;
            }

            // 2. Get employee and balance
            var employee = await _context.Employee
                .FirstOrDefaultAsync(e => e.EmployeeID == employeeId);

            if (employee == null)
                return (false, workingDays, 0);

            // 3. Compare balance
            bool canTake = employee.Type1_Balance >= workingDays;

            return (canTake, workingDays, employee.Type1_Balance);
        }

        public async Task<List<VacationRequestDto>> GetEmployeeRequestsAsync(int employeeId, int page, int pageSize)
        {
            var query = _context.VacationRequest
                .Where(v => v.EmployeeID == employeeId)
                .OrderByDescending(v => v.RequestDate)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(v => new VacationRequestDto
                {
                    RequestDate = v.RequestDate,
                    StartDate = v.StartDate,
                    EndDate = v.EndDate,
                    Status = v.Status,

                    // Get the last approved role from VacationApproval
                    //note that it returns ID not role as string
                    LastApprovedRoleId = _context.VacationApproval
                        .Where(a => a.RequestID == v.RequestID && a.Decision == "Approved")
                        .OrderByDescending(a => a.ApprovalDate)
                        .Select(a =>a.ApprovedByRoleID)
                        .FirstOrDefault()
                });

            return await query.ToListAsync();
        }

        public async Task<VacationRequest?> GetRequestByIdAsync(int requestId)
        {
            return await _context.VacationRequest
                .Include(v => v.Employee)
                .FirstOrDefaultAsync(v => v.RequestID == requestId);
        }

        public async Task<bool> CancelVacationRequestAsync(int requestId, int employeeId)
        {
            var req = await _context.VacationRequest
                .FirstOrDefaultAsync(r => r.RequestID == requestId && r.EmployeeID == employeeId);

            if (req == null || req.Status == "Approved") return false;

            req.Status = "Cancelled";
            await _context.SaveChangesAsync();
            return true;
        }

        // -----------------------------
        // Approvals
        // -----------------------------
        public async Task<List<VacationRequest>> GetPendingApprovalsAsync(int approverId)
        {
            return await _context.VacationRequest
                .Where(r => r.AwaitingApproval == approverId && r.Status == "Pending")
                .Include(r => r.Employee)
                .ToListAsync();
        }

        public async Task<bool> ApproveVacationAsync(int requestId, int approverId)
        {
            var req = await _context.VacationRequest.FirstOrDefaultAsync(r => r.RequestID == requestId);
            if (req == null) return false;

            var userid = await _context.Employee
               .Where(e => e.EmployeeID == approverId)
               .Select(e => e.UserID)
               .FirstOrDefaultAsync();
            if (userid == null) return false;
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new Exception("User not found in Identity");
            }
            // finally fetch the role(s) associated within this user object
            var roles = await _userManager.GetRolesAsync(user);



            // Record approval
            var approval = new VacationApproval
            {
                RequestID = requestId,
                ApprovedByRole = approverRole,
                ApprovedByID = approverId,
                ApprovalDate = DateTime.UtcNow,
                Decision = "Approved"
            };
            _context.VacationApproval.Add(approval);

            // Check if this approver is the Owner (top level)
            var approver = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeID == approverId);
            bool isOwner = approver?.RoleLevel == await GetOwnerRoleLevelAsync();

            if (isOwner)
            {
                req.Status = "Approved";
                req.AwaitingApproval = null;
            }
            else
            {
                // Move to next approver (manager’s boss)
                req.AwaitingApproval = await GetNextHigherManagerIdAsync(approverId);
            }

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RejectVacationAsync(Guid requestId, int approverId, int approverRole, string reason)
        {
            var req = await _context.VacationRequest.FirstOrDefaultAsync(r => r.RequestID == requestId);
            if (req == null) return false;

            var approval = new VacationApproval
            {
                RequestID = requestId,
                ApprovedByRole = approverRole,
                ApprovedByID = approverId,
                ApprovalDate = DateTime.UtcNow,
                Decision = "Rejected",
                Reason = reason
            };
            _context.VacationApproval.Add(approval);

            req.Status = "Rejected";
            req.AwaitingApproval = null;

            await _context.SaveChangesAsync();
            return true;
        }

        // -----------------------------
        // Balances / accruals
        // -----------------------------
        public async Task<(decimal type1, decimal type2)> GetEmployeeVacationBalanceAsync(int employeeId)
        {
            var emp = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeID == employeeId);
            if (emp == null) return (0, 0);

            return (emp.Type1_Balance, emp.Type2_Balance);
        }

        public async Task<bool> AdjustVacationBalanceAsync(int employeeId, decimal type1Delta, decimal type2Delta)
        {
            var emp = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeID == employeeId);
            if (emp == null) return false;

            emp.Type1_Balance += type1Delta;
            emp.Type2_Balance += type2Delta;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task RunMonthlyAccrualsAsync()
        {
            var employees = await _context.Employee.ToListAsync();

            foreach (var e in employees)
            {
                var monthsWorked = ((DateTime.UtcNow - e.HireDate).TotalDays / 30);
                if (monthsWorked < 3) continue;

                // Example unlock rule: +1 day of each type per month after first 3 months
                e.Type1_Balance += 1;
                e.Type2_Balance += 1;
            }

            await _context.SaveChangesAsync();
        }

        // -----------------------------
        // Helper methods
        // -----------------------------
        private async Task<int?> GetImmediateManagerIdAsync(int employeeId)
        {
            var emp = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeID == employeeId);
            return emp.ManagerID;
        }

        private async Task<int> GetOwnerRoleLevelAsync()
        {
            var ownerRole = await _context.Roles.FirstOrDefaultAsync(r => r.Role == "Owner");
            return ownerRole?.RoleLevel ?? -1;
        }

        private async Task<int?> GetNextHigherManagerIdAsync(int currentManagerId)
        {
            var mgr = await _context.Employee.FirstOrDefaultAsync(e => e.EmployeeID == currentManagerId);
            return mgr?.ManagerID;
        }
    }
}
