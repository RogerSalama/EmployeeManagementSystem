using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.DataTransferObjects
{
    public class BonusApproval
    {
        public int EmployeeID { get; set; }
        public int BonusTypeID { get; set; }

        [Precision(9,2)]
        public decimal Amount { get; set; }
        public string? reason { get; set; }

    }
}
