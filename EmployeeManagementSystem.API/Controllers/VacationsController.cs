using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/vacations")]
    public class VacationsController : ControllerBase
    {
        private readonly VacationService _vacationService;
        public VacationsController(VacationService vacationService)
        {
           _vacationService = vacationService;

        }
        [HttpPost("request")]
        [Authorize(Roles = "Employee,HeadOfUnit,Manager,Owner,Accountant")] // employees can call; managers may create on behalf
        public async Task<IActionResult> RequestVacation([FromBody] VacationRequestCreateDto dto)
        {
            //Note that I take in "Reason" but don't use it

            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var employeeId = int.Parse(employeeIdClaim);

            if (dto.StartDate == null || dto.EndDate==null)
            {
                return BadRequest(new {message = "missing information"});
            }
            var start = dto.StartDate.Value;   // safe because we checked null
            var end = dto.EndDate.Value;

            if (start > end)
                return BadRequest(new { message = "StartDate must be before EndDate" });

            var result = await _vacationService.CanTakeVacationAsync(employeeId, start, end);
            if (result.canTake) {
                var reqId = await _vacationService.CreateVacationRequestAsync(employeeId, start, end);
                return Ok(new { reqId, message = "Request Created" });
            }
            else return BadRequest(new { message = "Can't request vacation" });
        }
        [HttpGet("me")]
        [Authorize(Roles = "Employee,HeadOfUnit,Manager,Owner,Accountant")]
        public async Task<IActionResult> GetMyVacationRequests([FromQuery] int page = 1, [FromQuery] int pageSize = 5)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var employeeId = int.Parse(employeeIdClaim);

            var res = await _vacationService.GetEmployeeRequestsAsync(employeeId, page, pageSize);
            return Ok(res);

            
        }


        [HttpGet("pending")]
        [Authorize(Roles = "Manager,HeadOfUnit,Owner")]
        public async Task<IActionResult> GetPendingApprovals([FromQuery] int page = 1, [FromQuery] int pageSize = 50)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var employeeId = int.Parse(employeeIdClaim);
            var res = await _vacationService.GetPendingApprovalsAsync(employeeId);
            return Ok(res);

        }

        [HttpPost("{requestId}/approve")]
        [Authorize(Roles = "Manager,Head Of Unit,Admin")]
        public async Task<IActionResult> ApproveVacation(int requestId, [FromBody] ApprovalDto dto)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });
            var employeeId = int.Parse(employeeIdClaim);

            string role;
            if (User.IsInRole("Admin"))
            {
                role = "Admin";
            }
            else if (User.IsInRole("Manager"){
                role = "Manager";
            }
            else if (!User.IsInRole("Head of Unit"){
                role = "Head of Unit";

            }
            else
            {
                return BadRequest();
            }


            var res = await _vacationService.ApproveVacationAsync(requestId, employeeId);
            return NotImplemented();
        }

        private IActionResult NotImplemented()
        {
            throw new NotImplementedException();
        }
    }
}
