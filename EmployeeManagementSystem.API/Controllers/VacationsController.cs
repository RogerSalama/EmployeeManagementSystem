using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/vacations")]
    public class VacationsController : ControllerBase
    {
        [HttpGet("requests")]
        public async Task<IActionResult> GetVacationRequests()
        {
            // TODO: Get vacation requests
            return Ok(new List<object>());
        }

        [HttpPost("requests")]
        public async Task<IActionResult> CreateVacationRequest([FromBody] VacationRequestInput request)
        {
            // TODO: Create vacation request
            return CreatedAtAction(nameof(GetVacationRequests), request);
        }
    }
}
