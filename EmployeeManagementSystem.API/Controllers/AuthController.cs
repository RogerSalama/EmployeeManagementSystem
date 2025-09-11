
using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using EmployeeManagementSystem.API.Services;
﻿using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly TokenGeneration _tokenGeneration;
        // IConfiguration is a built in .net service that allows access to settings from 'appsettings.json' (stores private keys or data)
        private readonly LockoutService _lockoutService;
        private readonly ApplicationDbContext _context;


        public AuthController(TokenGeneration tokenGeneration, LockoutService lockoutService, ApplicationDbContext context)
        {
            _tokenGeneration = tokenGeneration;
            _lockoutService = lockoutService;
            _context = context;
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
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);
            var employee = await _context.Employee.FirstOrDefaultAsync(u => u.UserID == user.Id);
            var token = _tokenGeneration.GenerateJwtToken(employee.EmployeeID);
            return Ok(new { token });

         }


    
        



        

    }
}
