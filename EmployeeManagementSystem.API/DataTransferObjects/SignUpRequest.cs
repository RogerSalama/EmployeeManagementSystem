using EmployeeManagementSystem.API.Data;
namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class SignUpRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
    }
}