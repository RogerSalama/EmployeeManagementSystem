using EmployeeManagementSystem.Entities;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser
{
    // add the extra fields from your old Account class
    public DateTime LastLoginTime { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;

    // optional: link back to Employee
    public int? EmployeeID { get; set; }
    public Employee Employee { get; set; }

    public int LockoutCycleCount = 4;

}
