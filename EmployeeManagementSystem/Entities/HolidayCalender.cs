using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Entities
{
    public class HolidayCalendar
    {
        [Key]   // primary key is the Date itself
        public DateTime Date { get; set; }

        public string HolidayName { get; set; }

        public bool IsHoliday { get; set; }
    }
}
