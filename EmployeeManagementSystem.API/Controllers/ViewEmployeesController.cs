using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewEmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ViewEmployeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewEmployeeDTO>>> GetEmployees()
        {
            var employees = await _context.Employee
                .Include(e => e.Assigned_Projects)
                    .ThenInclude(ap => ap.Project)
                .Select(e => new ViewEmployeeDTO
                {
                    EmployeeID = e.EmployeeID,
                    FullName = e.First_Name + " " + e.Last_Name,
                    Projects = e.Assigned_Projects
                        .Select(ap => ap.Project.ProjectName)
                        .ToList()
                })
                .ToListAsync();

            return Ok(employees);
        }
    }
}
