namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class VacationRequestCreateDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Reason { get; set; }
    }
    public class VacationRequestDto
    {
        public DateTime RequestDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string? LastApprovedRoleId { get; set; }
    }

    public class ApprovalDto
    {
        public int ApproverId { get; set; } // from auth in real impl
        public string Note { get; set; }
    }

    public class RejectionDto
    {
        public Guid ApproverId { get; set; }
        public string Reason { get; set; }
        public bool SuggestAlternateDates { get; set; } = false;
    }

    public class VacationBalanceAdjustmentDto
    {
        public string Type { get; set; }      // Type1 / Type2
        public decimal Days { get; set; }     // positive or negative
        public string Reason { get; set; }
    }

    public class VacationPolicyDto
    {
        public string PolicyName { get; set; }
        public int Type1Days { get; set; }    // e.g., 14
        public int Type2Days { get; set; }    // e.g., 17
        public bool Type1RequiresNotice { get; set; }
        public string AccrualRulesJson { get; set; } // structured accrual rules or a richer type
        public DateTime EffectiveFrom { get; set; }
    }

    public class PayrollMarkDto
    {
        public string PayrollTransactionId { get; set; }
        public string Notes { get; set; }
    }

    public class DateRangeDto
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
    }
}
