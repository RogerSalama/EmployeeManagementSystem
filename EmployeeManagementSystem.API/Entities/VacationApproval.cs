using System;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.Entities
{
    [PrimaryKey(nameof(RequestID), nameof(ApprovedByRole))]
    public class VacationApproval
    {
        // Part of Composite Key
        public int RequestID { get; set; }
        [ForeignKey("RequestID")]
        public VacationRequest VacationRequest { get; set; }

        // Part of Composite Key
        public int ApprovedByRole { get; set; }
        [ForeignKey("ApprovedByRole")]
        //public Role Role { get; set; }

        // FK → Employee (who approved)
        public int ApprovedByID { get; set; }
        [ForeignKey("ApprovedByID")]
        public Employee ApprovedByEmployee { get; set; }

        public DateTime ApprovalDate { get; set; }
        public string Decision { get; set; }   // Approved / Rejected
        public string Reason { get; set; }
    }
}
