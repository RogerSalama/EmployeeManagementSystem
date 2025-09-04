using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Entities
{
    public class Bonus
    {
        [Key]
        public int BonusID { get; set; }

        // FK → Employee (who receives the bonus)
        public int EmployeeID { get; set; }
        [ForeignKey("EmployeeID")]
        public Employee Employee { get; set; }

        // FK → BonusType (defines type of bonus, e.g. Performance, Holiday, etc.)
        public int BonusTypeID { get; set; }
        [ForeignKey("BonusTypeID")]
        public BonusType BonusType { get; set; }

        public decimal Amount { get; set; }

        // FK → Employee (manager who approved the bonus)
        public int? ApprovedBy { get; set; }
        [ForeignKey("ApprovedBy")]
        public Employee ApprovedByEmployee { get; set; }

        public DateTime? ApprovedAt { get; set; }

        [MaxLength(500)]
        public string Reason { get; set; }

        public DateTime BonusDate { get; set; }
    }
}