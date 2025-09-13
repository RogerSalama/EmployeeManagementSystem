using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class SignUpRequest
    {
        // For ApplicationUser (Identity)
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; } // e.g., "Admin", "Manager", "Employee"

        // For Employee Entity
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string NationalID { get; set; }

        public DateTime DateOfBirth { get; set; }

        public DateTime DateOfHire { get; set; }

        public string Address { get; set; }

        public string MilitaryStatus { get; set; }

        public int? ManagerID { get; set; } // Nullable if not reporting to anyone initially

        // For the initial SalaryContract
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Salary must be greater than zero.")]
        public decimal InitialSalary { get; set; }
    }
}