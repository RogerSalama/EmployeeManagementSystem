using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManagementSystem.Entities
{
    [PrimaryKey(nameof(RequestID), nameof(ApprovedByRoleId))]
    public class VacationApproval
    {
        // Part of Composite Key
        public int RequestID { get; set; }
        [ForeignKey("RequestID")]
        public VacationRequest VacationRequest { get; set; }

        // Part of Composite Key
        // Composite Key (Part 2) → FK to AspNetRoles
        public string ApprovedByRoleId { get; set; }

        [ForeignKey(nameof(ApprovedByRoleId))]
        public IdentityRole ApprovedByRole { get; set; }
        public int ApprovedByID { get; set; }
        [ForeignKey("ApprovedByID")]
        public Employee ApprovedByEmployee { get; set; }

        public DateTime? ApprovalDate { get; set; } //nullable lehad ma el manager yerod
        public string? Decision { get; set; }   // Approved / Rejected and nullable lehad ma el manager yerod       
    }
}
