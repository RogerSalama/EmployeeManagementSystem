using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    [PrimaryKey(nameof(Vacation_Level_ID), nameof(VacationType))]
    public class VacationLevel
    {
        public int Vacation_Level_ID { get; set; }
        public bool VacationType { get; set; }

        public int No_of_Days { get; set; }
    }
}
