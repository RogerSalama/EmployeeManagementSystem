using EmployeeManagementSystem.API.Data;
using Microsoft.AspNetCore.Identity;
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

        public TokenGeneration(IConfiguration config, ApplicationDbContext context)
        {
            _config = config;
            _context = context;
        }

        public string GenerateJwtToken(string email)
        {

            // Reads the secret key (Jwt:Key) from appsettings.json.
            // Converts it to bytes and creates a SymmetricSecurityKey for signing the token. 
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            // Creates signing credentials using the secret key and the HMAC SHA256 algorithm.
            // This ensures the token cant be tampered with.
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //I now want to get the employeeId
        

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, email), // define the role from the email of the user
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
