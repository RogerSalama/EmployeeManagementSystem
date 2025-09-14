using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DataTransferObjects;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Services
{
    public class ViewProjectsService
    {
        private readonly ApplicationDbContext _context;

        public ViewProjectsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ViewProjectDTO>> GetProjectsAsync()
        {
            return await _context.Project
                .Include(p => p.Assigned_Projects) // navigation to join table
                    .ThenInclude(ep => ep.Employee) // include employees from join
                .Select(p => new ViewProjectDTO
                {
                    ProjectID = p.ProjectID,
                    ProjectName = p.ProjectName,
                    CustomerName = p.CustomerName,
                    Employees = p.Assigned_Projects
                        .Select(ep => new ViewEmployeesinProjectDto
                        {
                            Id = ep.Employee.EmployeeID,
                            Name = ep.Employee.First_Name + " " + ep.Employee.Last_Name,


                        }).ToList()
                  
                })
                .ToListAsync();
        }
    }
}
