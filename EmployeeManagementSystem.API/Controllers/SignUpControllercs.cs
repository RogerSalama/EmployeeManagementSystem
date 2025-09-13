using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;


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

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] SignUpRequest request)
        {
            var result = await _signupService.RegisterUserAsync(request);

            if (!result.Success)
            {
                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { message = "Account created successfully." });
        }
    }
}