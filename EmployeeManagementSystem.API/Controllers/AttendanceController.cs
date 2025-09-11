using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;
using Microsoft.AspNetCore.Cors;
using EmployeeManagementSystem.API.Services;
using System.Diagnostics.CodeAnalysis;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly PunchService _punchService;
        private readonly timeStamp _timestamp;
        public AttendanceController(PunchService punchService, timeStamp timestamp)
        {
            _punchService = punchService;
            _timestamp = timestamp;
        }

        [HttpPost("checkin")] 
        public async Task<IActionResult> CheckIn([FromBody] CheckInRequest request)
        {   
            // Generate timestamp (UTC is preferred for consistency)
            var timestamp = await _timestamp.GetCurrentTimeAsync();

            // Call your service logic with timestamp
            var result = await _punchService.ProjectCheckin(timestamp);
           
            if (result)
            {
                return Ok(new { message = "Session started", timestamp });
            }
            else { return BadRequest(); }
             
        }
    }
}
