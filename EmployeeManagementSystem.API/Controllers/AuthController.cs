using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EmployeeManagementSystem.API.Services;


namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly TokenGeneration _tokenGeneration;

        public AuthController(TokenGeneration tokenGeneration)
        {
             _tokenGeneration = tokenGeneration;
        }
        
        // Hardcoded users for now
        private readonly Dictionary<string, string> _users = new()
        {
            { "besheer", "1234" },
            { "saif", "5678" }
        };

       
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (_users.ContainsKey(request.Email) &&
                _users[request.Email] == request.Password)
            {
                var token = _tokenGeneration.GenerateJwtToken(request.Email);
                return Ok(new { token });
            }

            return Unauthorized("Invalid email or password");
        }

        

    }
}
