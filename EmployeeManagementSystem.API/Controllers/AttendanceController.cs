using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;
using System.Security.Claims;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        private readonly PunchService _punchService;
        private readonly timeStamp _timestamp;
        //private readonly DBCheckin _checkinService;
        public AttendanceController(PunchService punchService, timeStamp timestamp)
        {
            _punchService = punchService;
            _timestamp = timestamp;
            //_checkinService = CheckinService;
        }

        //[HttpPost("checkin")] 
        //public async Task<IActionResult> CheckIn()
        //{
        //    // Generate timestamp (UTC is preferred for consistency)
        //    var timestamp = await _timestamp.GetCurrentTimeAsync();

        //    // Call your service logic with timestamp
        //    var result = await _punchService.PunchingSystem(timestamp);

        //    if (result)
        //    {
        //        return Ok(new { message = "Timestamp stored!", timestamp });
        //    }
        //    else { return BadRequest(); }


        //}
        [HttpPost("checkin")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> CheckIn([FromBody] dynamic request)
        {
            
            Guid projectId;
            try
            {
                projectId = Guid.Parse((string)request.projectId);
            }
            catch
            {
                return BadRequest(new { message = "Invalid or missing ProjectId" });
            }

           
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var employeeId = int.Parse(employeeIdClaim);

            var timestamp = DateTimeOffset.UtcNow;

            // TODO: Ensure there is no active session

            // Call service to create Attendance session and get SessionID
            // var sessionId = await _punchService.CreateSessionAsync(employeeId, timestamp, projectId);

           // if (sessionId != null)
            //{
             //   return Ok(new { sessionId });
            //}

            return BadRequest(new { message = "Check-in failed. You may already have an active session." });
        }

        [HttpPost("change-proj")]
        [Authorize(Roles = "Employee")]
        public async Task<IActionResult> ChangeProject([FromBody] dynamic request)
        {
            // Get ProjectId and SessionId from JSON body
            Guid projectId;
            Guid sessionId;
            try
            {
                projectId = Guid.Parse((string)request.projectId);
                sessionId = Guid.Parse((string)request.sessionId);
            }
            catch
            {
                return BadRequest(new { message = "Invalid or missing ProjectId or SessionId" });
            }

            
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var employeeId = int.Parse(employeeIdClaim);

            // TODO: Validate session ownership
            // e.g., ensure session belongs to employeeId and is editable
            // var session = await _attendanceService.GetSessionByIdAsync(sessionId);
            // if (session.EmployeeId != employeeId) return Forbid("Cannot edit another employee's session");

            // TODO: Update the session project in the database
            // await _attendanceService.ChangeProjectAsync(sessionId, projectId);

            return Ok(new
            {
                message = "Project changed successfully",
            });
        }



    }
}
