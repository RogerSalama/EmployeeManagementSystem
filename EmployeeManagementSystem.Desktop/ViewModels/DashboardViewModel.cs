using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Desktop.ViewModels
{
    public class DashboardViewModel
    {
        public string UserName { get; set; } = "Tom";
        public string AttendanceToday { get; set; } = "6.5 hours today";
        public string WeekSummary { get; set; } = "32.5h (+2.5h overtime)";
        public string MonthSummary { get; set; } = "142h (18 days present)";
        public string VacationDays { get; set; } = "23 days remaining";
    }
}
