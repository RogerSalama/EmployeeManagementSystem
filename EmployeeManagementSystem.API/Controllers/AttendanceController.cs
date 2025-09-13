using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using EmployeeManagementSystem.Entities;
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
        private readonly DBCheckin _checkinService;
        public AttendanceController(PunchService punchService, timeStamp timestamp,DBCheckin dBCheckin)
        {
            _punchService = punchService;
            _timestamp = timestamp;
            _checkinService = dBCheckin;
            
        }

        [HttpPost("Check-in")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInRequest request)
        {

            int projectId = request.ProjectId;
 

            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var employeeId = int.Parse(employeeIdClaim);

            var timestamp = DateTime.UtcNow;


            var sessionId = await _punchService.ProjectCheckin(timestamp, employeeId, projectId);

            if (sessionId != -1)
            { 
               return Ok(new { sessionId , message = "Session generated" });
            }

            return BadRequest(new { message = "Check-in failed. You may already have an active session." });
        }

        [HttpPost("Check-out")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Checkout([FromBody] CheckOutRequest request)
        {
            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });


            var employeeId = int.Parse(employeeIdClaim);
            int sessionId = request.sessionId;
            var timestamp = DateTime.UtcNow;

            var ischeckedout = await _checkinService.DBCheck_out(sessionId, employeeId);


            if (ischeckedout)
            {
                return Ok(new { sessionId, message = "Session generated" });
            }

            return BadRequest(new { message = "Check-out failed" });

            //implement the checkout function that takes session ID and employee ID and finds last open log and close it,
            //then put an end time at the open session.
            //then queries for all logs in current session and creates a summary of all open projects along with their corresponding duration,
            //even if one project has more than one log, give a summary.
            //note ALL ACTIVE projects not all projects.( fe projects hyb2a time zero) return this summary as json or DTO.
        }

        [HttpPost("change-proj")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ChangeProject([FromBody] ChangeprojReq request)
        {
            
            int projectId = request.projectId;
            int sessionId = request.sessionId;


            var employeeIdClaim = User.FindFirst("EmployeeId")?.Value;
            if (string.IsNullOrEmpty(employeeIdClaim))
                return Unauthorized(new { message = "Employee ID not found in token." });

            var employeeId = int.Parse(employeeIdClaim);

            var change_proj = await _checkinService.DBChange_proj(sessionId,employeeId, projectId);

            // TODO: Validate session ownership
            // e.g., ensure session belongs to employeeId and is editable
            // var session = await _attendanceService.GetSessionByIdAsync(sessionId);
            // if (session.EmployeeId != employeeId) return Forbid("Cannot edit another employee's session");

            // TODO: Update the session project in the database
            // await _attendanceService.ChangeProjectAsync(sessionId, projectId);

            switch (change_proj)
            {
                case 0: return BadRequest(new { message = "-----the session is closed-----" });
                case -1: return BadRequest(new { message = "-----there is no open log in the current session-----" });
                case 1: return Ok(new {message = "project changed in the current session successfully"});
                default: return BadRequest(new { message = "default rejection message" });
            }
            
        }

      

    }
}
