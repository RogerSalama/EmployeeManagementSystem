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
        public Employee Employee { get; set; } //msh el mfrood tb2a list??
        public string ProjectName { get; set; }
        public string CustomerName { get; set; }
        public string Status { get; set; }  //Turn into enum
        [Precision(9, 2)]
        public decimal Revenue { get; set; } = 0; //fel awel khales
        [Precision(9, 2)]
        public decimal Expenses { get; set; } = 0; //fel awel khales brdo
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;








        // Project.cs
        public ICollection<Assigned_Projects> Assigned_Projects { get; set; } = new List<Assigned_Projects>();

    }
}
