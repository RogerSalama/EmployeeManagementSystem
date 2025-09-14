using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ViewProjectsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ViewProjectsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewProjectDTO>>> GetProjects()
        {
            var projects = await _context.Project
                .Include(p => p.Assigned_Projects)
                    .ThenInclude(ap => ap.Employee)
                .Select(p => new ViewProjectDTO
                {
                    ProjectID = p.ProjectID,
                    ProjectName = p.ProjectName,
                    CustomerName = p.CustomerName,

                    Employees = p.Assigned_Projects
                        .Select(ap => new ViewEmployeesinProjectDto
                        {
                            Id = ap.Employee.EmployeeID,
                            Name = ap.Employee.First_Name + " " + ap.Employee.Last_Name
                        })
                        .ToList()
                })
                .ToListAsync();

            return Ok(projects);
        }

    }
}
