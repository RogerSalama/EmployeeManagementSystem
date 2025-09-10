using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Entities
{
    public class Overtime
    {
        [Key]
        public int OvertimeID { get; set; }

        // FK → Employee (who worked overtime)
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        // FK → Attendance session (link to work session)
        public int SessionID { get; set; }
        [ForeignKey("SessionID")]
        public Attendance AttendanceSession { get; set; }

        // FK → Overtime rule (multiplier etc.)
        public int OvertimeRuleID { get; set; }
        [ForeignKey("OvertimeRuleID")]
        public OvertimeRule OvertimeRule { get; set; }

        public DateTime Date { get; set; }

        public double HoursWorked { get; set; }

        // Enum for status: Pending, Accepted, Rejected
        public OvertimeStatus Status { get; set; } = OvertimeStatus.Pending;

        // FK → Employee (manager who approved the overtime)
        public int? ApprovedBy { get; set; } //lazem approval for overtime so not nullable
        [ForeignKey("ApprovedBy")]
        public Employee ApprovedByEmployee { get; set; }

        public DateTime? ApprovedAt { get; set; }
    }

    public enum OvertimeStatus
    {
        Pending = 0,
        Accepted = 1,
        Rejected = 2
    }
}
