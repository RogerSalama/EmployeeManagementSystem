using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace EmployeeManagementSystem.API.Controllers
{

    [ApiController]
    [Route("employee/me")]
    public class AssignedProjectsController : ControllerBase
    {
        private readonly getAssignedProjects _getAssignedProjectsService;

        public AssignedProjectsController(getAssignedProjects getAssignedProjectsService)
        {
            _getAssignedProjectsService = getAssignedProjectsService;
        }

        [HttpGet("projects")]
        public async Task<IActionResult> projects()
        {

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId) || !int.TryParse(userId, out int employeeId))
                return Unauthorized();

            var assignedprojects = await _getAssignedProjectsService.GetProjectsAsync(employeeId);
            return Ok(assignedprojects);
        }
    }
}
