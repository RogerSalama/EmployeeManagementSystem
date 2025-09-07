using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagementSystem.Entities
{
    public class OvertimeRule
    {
        [Key]   // simple primary key
        public int ID { get; set; }

        public string DayType { get; set; }   // e.g. "Weekend", "Holiday"
        [Precision(4, 2)]
        public decimal Multiplier { get; set; }   // e.g. 1.5, 2.0
    }
}
