using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    //public int ContractID { get; set; }
    //[ForeignKey("ContractID")]
    //public SalaryContract SalaryContract { get; set; }
    public ICollection<SalaryContract> SalaryContracts { get; set; } = new List<SalaryContract>(); //employee has collection of salary contracts to show contaract history

    //public int RoleLevel { get; set; }
    //public Role Role { get; set; } //el identity table does this shit on its own

    public int? ManagerID { get; set; }
    [ForeignKey("ManagerID")]
    public Employee Manager { get; set; }

    public int Type1_Balance { get; set; }
    public int Type2_Balance { get; set; }

    public int Vacation_Level_ID { get; set; }
    [ForeignKey("Vacation_Level_ID")]
    public VacationLevel Vacation_Level { get; set; } //yaani eh?

    public int EmployementStatus { get; set; }

    // 👇 new relation to Identity User
    public string UserID { get; set; }
    [ForeignKey("UserID")]
    public ApplicationUser User { get; set; }






    // Employee.cs
    public ICollection<Assigned_Projects> Assigned_Projects { get; set; } = new List<Assigned_Projects>();

}
