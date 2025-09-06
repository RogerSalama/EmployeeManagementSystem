namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class UserInput
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public DateTime EmploymentDate { get; set; }
        public double Salary { get; set; }
    }
}
