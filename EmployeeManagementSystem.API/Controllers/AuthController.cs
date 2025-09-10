
﻿using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace EmployeeManagementSystem.API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        // IConfiguration is a built in .net service that allows access to settings from 'appsettings.json' (stores private keys or data)
        private readonly IConfiguration _config;
        private readonly LockoutService _lockoutService;

        
        public AuthController (IConfiguration config, LockoutService lockoutService)
        {
            _config = config;
            _lockoutService = lockoutService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // First, check if user is locked out
            if (await _lockoutService.IsLockedOutAsync(request.Email))
            {
                return Unauthorized("Your account is locked. Please wait until the lockout period ends or contact support.");
            }

            // Then, validate password (and update lockout state internally)
            var isLoginSuccessful = await _lockoutService.CheckPasswordAndLockoutAsync(request.Email, request.Password);

            if (!isLoginSuccessful)
            {
                return Unauthorized("Invalid email or password."); // GUI can display this
            }

            // If successful, issue JWT token
            var token = GenerateJwtToken(request.Email);
            return Ok(new { token });
        }



        private string GenerateJwtToken(string email)
        {

            // Reads the secret key (Jwt:Key) from appsettings.json.
            // Converts it to bytes and creates a SymmetricSecurityKey for signing the token. 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Creates signing credentials using the secret key and the HMAC SHA256 algorithm.
            // This ensures the token cant be tampered with.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email),
                new Claim(ClaimTypes.Role, "User")
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
