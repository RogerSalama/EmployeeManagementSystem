using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    public class VacationRequest
    {
        [Key]
        public int RequestID { get; set; }

        // FK → Employee
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        public DateTime RequestDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // e.g. Pending/Approved/Rejected/Cancelled
        public string Status { get; set; }

        // FK → Manager (Employee)
        public int AwaitingApproval { get; set; }
        [ForeignKey("AwaitingApproval")]
        public Employee Manager { get; set; }
    }
}
