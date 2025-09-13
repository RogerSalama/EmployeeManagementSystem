using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.API.DataTransferObjects;
using EmployeeManagementSystem.API.Services;
using EmployeeManagementSystem.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.API.Services 
{
    public class SignUpService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly PasswordService _passwordService;
        private readonly ApplicationDbContext _context; 

        public SignUpService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager,PasswordService passwordService, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _passwordService = passwordService;
            _context = context;
        }

        public async Task<SignupResult> RegisterUserAsync(SignUpRequest request)
        {
            // Check if a user already exists with the email
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                return new SignupResult { Success = false, ErrorMessage = "Email is already registered." };
            }

            // Check if the specified role exists
            if (!await _roleManager.RoleExistsAsync(request.Role))
            {
                return new SignupResult { Success = false, ErrorMessage = $"The role '{request.Role}' does not exist." };
            }//list implementation, probably can remove this block of code, as the roles will be presented as a dropdown list in the gui

            // Validate password strength
            if (!_passwordService.ValidatePassword(request.Password))
            {
                return new SignupResult { Success = false, ErrorMessage = "Password does not meet security requirements." };
            }

            // Create the Identity user object
            var user = new ApplicationUser
            {
                UserName = request.Email, // Use email for the username to ensure it's unique
                Email = request.Email,
                LockoutEnabled = true,
            };

            // Use a transaction to ensure atomicity
            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // STEP 1: Create the user in ASP.NET Identity
                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    await transaction.RollbackAsync();
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    return new SignupResult { Success = false, ErrorMessage = $"User creation failed: {errors}" };
                }

                // STEP 2: Create the corresponding Employee entity
                var employee = new Employee
                {
                    UserID = user.Id, // Link to the Identity user
                    First_Name = request.FirstName,
                    Last_Name = request.LastName,
                    National_ID = request.NationalID,
                    BirthDate = request.DateOfBirth,
                    HireDate = request.DateOfHire,
                    Address = request.Address,
                    Military_Status = request.MilitaryStatus,
                    ManagerID = request.ManagerID,
                    EmployementStatus = 1, // Assuming 1 represents "Active"
                    // Set default vacation balances
                    Type1_Balance = 0,
                    Type2_Balance = 0,
                    // might need a default Vacation_Level_ID or logic to assign one
                    // Vacation_Level_ID = 1
                };

                // STEP 3: Create the initial salary contract
                var salaryContract = new SalaryContract
                {
                    Amount = request.InitialSalary,
                    EffectiveFrom = request.DateOfHire,
                    EffectiveTo = DateTime.MaxValue, // Or another far-future date
                    Employee = employee, // EF Core will link this
                    CreatedBy = 1 // Placeholder for an admin or system user ID, adjust as needed
                };

                // Add the contract to the employee's contracts
                employee.SalaryContracts.Add(salaryContract);

                // Add the new employee to the context
                _context.Employee.Add(employee);
                await _context.SaveChangesAsync();


                // STEP 4: Assign the user to the specified role
                var roleResult = await _userManager.AddToRoleAsync(user, request.Role);
                if (!roleResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    return new SignupResult { Success = false, ErrorMessage = $"Could not add user to role: {roleErrors}" };
                }

                // If all steps succeeded, commit the transaction
                await transaction.CommitAsync();

                return new SignupResult { Success = true };
            }
            catch (Exception ex)
            {
                // If anything goes wrong, roll back the entire transaction
                await transaction.RollbackAsync();
                // It's a good practice to log the exception (ex)
                return new SignupResult { Success = false, ErrorMessage = "An unexpected error occurred during signup." };
            }
        }
    }
    //    public async Task<SignupResult> RegisterUserAsync(string email, string password, string employeeName)
    //    {
    //        // check if a user alreaady exists with the email entered
    //        var existingUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            
    //        if (existingUser != null)
    //        {
    //            return new SignupResult
    //            {
    //                Success = false,
    //                ErrorMessage = "Email is Already registered"
    //            };
    //        }

    //        // validate password strength
    //        if (!_passwordService.ValidatePassword(password))
    //        {
    //            return new SignupResult
    //            {
    //                Success = false,
    //                ErrorMessage = "Password does not meet security requirements."
    //            };
    //        }

    //        // Password checks are passed, goes on to create a user
    //        var user = new ApplicationUser
    //        {
    //            Email = email,
    //            UserName = employeeName,
    //            LockoutEnabled = true,
    //        };

    //        // create a user with ASP.Net identity
    //        var result = await _userManager.CreateAsync(user, password);

    //        // in case of an unforseen error causing the sign up to not complete
    //        if (!result.Succeeded)
    //        {
    //            return new SignupResult
    //            {
    //                Success = false,
    //                ErrorMessage = "Something went wrong with the sign up process, please try again "
    //            }; 
    //        }

    //        //kolo tmm el tmm

    //        return new SignupResult
    //        {
    //            Success = true
    //        };
            
    //    }

    //}
    public class SignupResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }
}
