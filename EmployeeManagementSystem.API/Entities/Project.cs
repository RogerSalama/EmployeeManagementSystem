using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities
{
    public class Project
    {
        [Key]
        public int ProjectID { get; set; }
        public int ProjectHead { get; set; }
        [ForeignKey("ProjectHead")]
        public Employee Employee { get; set; }
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }  //Turn into enum
        [Precision(9, 2)]
        public decimal Revenue { get; set; }
        [Precision(9, 2)]
        public decimal Expenses { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
