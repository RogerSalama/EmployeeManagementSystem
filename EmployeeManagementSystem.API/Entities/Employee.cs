using EmployeeManagementSystem.Entities;
using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Key]
    public int EmployeeID { get; set; }

    public string National_ID { get; set; }
    public string First_Name { get; set; }
    public string Last_Name { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HireDate { get; set; }
    public string Address { get; set; }
    public string Military_Status { get; set; }

    public int ContractID { get; set; }
    public SalaryContract SalaryContract { get; set; }

    public int RoleLevel { get; set; }
    //public Role Role { get; set; } //el identity table does this shit on its own

    public int ManagerID { get; set; }
    public Employee Manager { get; set; }

    public int Type1_Balance { get; set; }
    public int Type2_Balance { get; set; }

    public int Vacation_Level_ID { get; set; }
    public VacationLevel Vacation_Level { get; set; }

    public int EmployementStatus { get; set; }

    // 👇 new relation to Identity User
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }
}
