using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using EmployeeManagementSystem.API.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/signup")]
    public class SignupController : ControllerBase
    {
        private readonly SignUpService _signupService;

        public SignupController(SignUpService signupService)
        {
            _signupService = signupService;
        }

        [HttpPost("signup")]
        public async Task<IActionResult> Register([FromBody] SignUpRequest request)
        {
            var result = await _signupService.RegisterUserAsync(request.Email, request.Password, request.UserName);

            if (!result.Success)
            {
                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Account created successfully." });
        }
    }
}