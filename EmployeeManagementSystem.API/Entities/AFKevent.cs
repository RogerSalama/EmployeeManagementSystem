using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Entities
{
    public class AFKEvent
    {
        [Key]
        public int AFK_ID { get; set; }

        // Foreign Keys
        [ForeignKey(nameof(Attendance))]
        public int SessionID { get; set; }

        [ForeignKey(nameof(Employee))]
        public int EmployeeID { get; set; }

        // Navigation Properties
        public Attendance Attendance { get; set; }
        public Employee Employee { get; set; }

        // AFK timings
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
    }
}
