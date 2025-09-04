using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.DirectoryServices.ActiveDirectory;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
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
        public string Military_Status { get; set; } //Turn into enum later

        public int ContractID { get; set; }
        [ForeignKey("ContractID")]
        public SalaryContract SalaryContract { get; set; }

        public int RoleLevel { get; set; }
        [ForeignKey("RoleLevel")]
        public Role Role { get; set; }

        public int ManagerID { get; set; }
        [ForeignKey("ManagerID")]
        public Employee Manager { get; set; }

        public int Type1_Balance { get; set; }
        public int Type2_Balance { get; set; }

        public int Vacation_Level_ID { get; set; }
        [ForeignKey("Vacation_Level_ID")]
        public Vacation_Level Vacation_Level { get; set; }

        public int EmployementStatus { get; set; } //Turn into enum later
    }
}