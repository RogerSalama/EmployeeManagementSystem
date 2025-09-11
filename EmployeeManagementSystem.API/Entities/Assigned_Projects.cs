using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    [PrimaryKey(nameof(ProjectID),nameof(EmployeeID))]
    public class Assigned_Projects
    {
        public int EmployeeID { get; set; }
        [ForeignKey(nameof(EmployeeID))]
        public Employee Employee { get; set; }

        public int ProjectID { get; set; }
        [ForeignKey(nameof(ProjectID))]
        public Project Project { get; set; }
    }
}
