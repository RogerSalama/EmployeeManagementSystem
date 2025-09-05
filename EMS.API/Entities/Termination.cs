using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagementSystem.Entities
{
    public class Termination
    {
        [Key]
        public int TerminationID { get; set; }

        // Foreign Key
        public int EmployeeID { get; set; }

        // Navigation Property
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        public DateTime TerminationDate { get; set; }
    }
}
