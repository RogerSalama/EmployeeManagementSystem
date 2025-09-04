using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities
{
    public class Employee_Project
    {
        [Key]
        public int LogID { get; set; }

        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        public int ProjectID { get; set; }
        [ForeignKey("ProjectID")]
        public Project Project { get; set; }

        public int SessionID { get; set; }
        [ForeignKey("SessionID")]
        public Attendance Attendance { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow.Date;
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        public DateTime EndTime { get; set; } = DateTime.UtcNow;
        public DateTime AdjustedAt { get; set; } = DateTime.UtcNow;

    }
}
