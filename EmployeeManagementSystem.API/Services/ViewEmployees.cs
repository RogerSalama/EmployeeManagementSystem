using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

public class EmployeeService
{
    private readonly ApplicationDbContext _context;

    public EmployeeService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Employee>> ViewEmployeesAsync()
    {
        return await _context.Employee
            .Include(e => e.User)                        // link to identity
            .Include(e => e.Manager)                     // manager
            .Include(e => e.Vacation_Level)              // vacation level
            .Include(e => e.Assigned_Projects)           // join table
                .ThenInclude(ap => ap.Project)           // project details
            .ToListAsync();
    }

    public async Task<List<ViewEmployeeDTO>> ViewEmployeeDTOAsync()
    {
        return await _context.Employee
            .Include(e => e.Manager)
            .Include(e => e.Vacation_Level)
            .Include(e => e.Assigned_Projects).ThenInclude(ap => ap.Project)
            .Select(e => new ViewEmployeeDTO
            {
                EmployeeID = e.EmployeeID,
                FullName = e.First_Name + " " + e.Last_Name,
                Address = e.Address,
                Military_Status = e.Military_Status,
                ManagerName = e.Manager != null ? e.Manager.First_Name + " " + e.Manager.Last_Name : null,

                // instead of LevelName, use Type1/Type2
                VacationLevelInfo = e.Vacation_Level != null
                    ? $"Type1: {e.Vacation_Level.Type1_Days} days, Type2: {e.Vacation_Level.Type2_Days} days"
                    : null,

                Projects = e.Assigned_Projects
                            .Select(ap => ap.Project.ProjectName)
                            .ToList()
            })
            .ToListAsync();
    }

}