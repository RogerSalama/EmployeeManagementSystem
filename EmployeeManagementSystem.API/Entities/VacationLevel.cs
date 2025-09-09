using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    public class VacationLevel
    {
        [Key]
        public int Vacation_Level_ID { get; set; }
        public int Type1_Days { get; set; }

        public int Type2_Days { get; set; }

        /// ghir el fel diagram ??who changed it and why
    }
}
