using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Entities
{
    public class Payroll_Item
    {
        [Key]
        public int ItemID { get; set; }
        public int PayID { get; set; }
        [ForeignKey("PayID")]
        public Payroll Payroll { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }
        public int BonusTypeID { get; set; }
        [ForeignKey("BonusTypeID")]
        public BonusType BonusType { get; set; }
        public int SourceID { get; set; }
        public float Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public Employee EmployeeCreatedBY { get; set; }
    }
}
