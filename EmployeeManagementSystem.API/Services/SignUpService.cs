using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using EmployeeManagementSystem.API.Services;
using EmployeeManagementSystem.API.DataTransferObjects;

namespace EmployeeManagementSystem.API.Services 
{
    public class SignUpService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly PasswordService _passwordService;


        public SignUpService(UserManager<ApplicationUser> userManager, PasswordService passwordService)
        {
            _userManager = userManager;
            _passwordService = passwordService;
        }

        public async Task<SignupResult> RegisterUserAsync(string email, string password, string employeeName)
        {
            // check if a user alreaady exists with the email entered
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            
            if (existingUser != null)
            {
                return new SignupResult
                {
                    Success = false,
                    ErrorMessage = "Email is Already registered"
                };
            }

            // validate password strength
            if (!_passwordService.ValidatePassword(password))
            {
                return new SignupResult
                {
                    Success = false,
                    ErrorMessage = "Password does not meet security requirements."
                };
            }

            // Password checks are passed, goes on to create a user
            var user = new ApplicationUser
            {
                Email = email,
                UserName = employeeName,
                LockoutEnabled = true,
            };

            // create a user with ASP.Net identity
            var result = await _userManager.CreateAsync(user, password);

            // in case of an unforseen error causing the sign up to not complete
            if (!result.Succeeded)
            {
                return new SignupResult
                {
                    Success = false,
                    ErrorMessage = "Something went wrong with the sign up process, please try again "
                }; 
            }

            //kolo tmm el tmm

            return new SignupResult
            {
                Success = true
            };
            
        }

    }
    public class SignupResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
