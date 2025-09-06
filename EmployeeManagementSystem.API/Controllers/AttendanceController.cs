using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/attendance")]
    public class AttendanceController : ControllerBase
    {
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInRequest request)
        {
            // TODO: Process check-in
            return Ok();
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutRequest request)
        {
            // TODO: Process check-out
            return Ok();
        }
    }
}
