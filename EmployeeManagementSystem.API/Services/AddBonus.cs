using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Services
{
    public class AddBonus
    {
        private readonly ApplicationDbContext _context;

        public AddBonus(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateBonus(int Employeeid, int ManagerID, int bonusTypeID, decimal Amount, string Reason)
        {
            DateTime dateTime = DateTime.UtcNow;
            var bonus = new Bonus
            {
                EmployeeID = Employeeid,
                BonusTypeID = bonusTypeID,
                Amount = Amount,
                ApprovedBy = ManagerID,
                ApprovedAt = dateTime,
                Reason = Reason
            };

            _context.Bonus.Add(bonus);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
