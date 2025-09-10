using EmployeeManagementSystem.API.Data;
namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class SignUpRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string UserName { get; set; }
        public required string role { get; set; }
        public required int salary { get; set; }
        public required DateTime dateOfHire { get; set; }
        public DateTime dateOfBirth { get; set; }
        public int NationalID { get; set; }
        public bool militaryStatus { get; set; }

    }
}