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
    public class SalaryContract
    {
        [Key]
        public int ContractID { get; set; }
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        [InverseProperty("SalaryContracts")] //Since there are 2 Employee properties, this specifies what this belongs to
        public Employee Employee { get; set; }
        [Precision(9, 2)]
        public decimal Amount { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        public Employee EmployeeCreatedBy { get; set; }
    }
}
