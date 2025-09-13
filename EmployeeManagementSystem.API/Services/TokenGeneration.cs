using EmployeeManagementSystem.API.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeManagementSystem.API.Services
{
    public class TokenGeneration
    {

        // IConfiguration is a built in .net service that allows access to settings from 'appsettings.json' (stores private keys or data)
        private readonly IConfiguration _config;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public TokenGeneration(IConfiguration config, ApplicationDbContext context,UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _context = context;
            _userManager = userManager;

        }

        public async Task<string> GenerateJwtToken(int employeeId)
        {

            // Reads the secret key (Jwt:Key) from appsettings.json.
            // Converts it to bytes and creates a SymmetricSecurityKey for signing the token. 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Creates signing credentials using the secret key and the HMAC SHA256 algorithm.
            // This ensures the token cant be tampered with.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            // fetch the userid using the employee id from table employees
            var userid = await _context.Employee
                .Where(e => e.EmployeeID == employeeId)
                .Select(e => e.UserID)   
                .FirstOrDefaultAsync();

            if (userid == null)
            {
                throw new Exception("No user found for this employee");
            }
            // fetch the user object using the userid from the Entity framework tables
            var user =  await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                throw new Exception("User not found in Identity");
            }
            // finally fetch the role(s) associated within this user object
            var roles =  await _userManager.GetRolesAsync(user);
            var role = roles.First();

            Console.WriteLine($"the specified role for this employee is: {role}, employeeid: {employeeId}");
            
            var claims = new[]
            {
                new Claim("EmployeeId", employeeId.ToString()), // define the role from the email of the user
                new Claim(ClaimTypes.Role, role)
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
