using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.DataTransferObjects;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/dashboard")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("productivity")]
        public async Task<IActionResult> GetProductivity()
        {
            // TODO: Get productivity metrics
            return Ok(new { });
        }

        [HttpGet("project-costs")]
        public async Task<IActionResult> GetProjectCosts()
        {
            // TODO: Get project costs
            return Ok(new { });
        }
    }
}
