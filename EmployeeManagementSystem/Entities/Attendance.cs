using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    [PrimaryKey(nameof(SessionID))]
    public class Attendance
    {
        public int SessionID { get; set; }   // PK

        // FK → Employee
        public int EmployeeID { get; set; }
        [ForeignKey(nameof(EmployeeID))]
        public Employee Employee { get; set; }

        public DateTime Date { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        // Total time spent AFK (in minutes or as TimeSpan)
        public TimeSpan AFK_Time { get; set; }
    }
}
