using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;

namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            // TODO: Get users from database
            return Ok(new List<object>());
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserInput user)
        {
            // TODO: Create user
            return CreatedAtAction(nameof(GetUser), new { userId = Guid.NewGuid() }, user);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(Guid userId)
        {
            // TODO: Get user by ID
            return Ok(new { });
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UserInput user)
        {
            // TODO: Update user
            return Ok();
        }
    }
}
