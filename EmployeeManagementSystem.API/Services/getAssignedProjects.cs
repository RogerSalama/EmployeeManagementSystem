using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DataTransferObjects;
using Microsoft.EntityFrameworkCore; // <- needed for ToListAsync

using System.Security.Claims;


namespace EmployeeManagementSystem.API.Services
{
    public class getAssignedProjects
    {
        private readonly ApplicationDbContext _context;

        public getAssignedProjects(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<List<ProjectDto>> GetProjectsAsync(int userId)
        {


            var projects = await _context.Assigned_Projects
                .AsNoTracking()
                .Where(pa => pa.EmployeeID == userId)
                .Select(pa => new ProjectDto {
                    Id = pa.Project.ProjectID,
                    Name = pa.Project.ProjectName
                })
                .ToListAsync();
            return projects;
        }
    }
}