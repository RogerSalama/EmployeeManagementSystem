using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Services
{
    public class CalculateOvertime
    {
        private readonly ApplicationDbContext _context;
        private readonly OvertimeMultiplier _multiplier;

        public CalculateOvertime(ApplicationDbContext context, OvertimeMultiplier multiplier)
        {
            _context = context;
            _multiplier = multiplier;
        }

        //This function is called after we call checkOut function
        //Inside the service that does the checkout, at the end just call this service
        public async Task<string> calcOvertime (int SessionId)
        {
            var session = await _context.Attendance
                .FirstOrDefaultAsync(a => a.SessionID == SessionId);

            if (session == null || session.CheckOut == null)
                throw new InvalidOperationException("Session not found or still active.");

            TimeSpan SessionDuration = session.CheckOut.Value - session.CheckIn;

            var AFKDruation = await _context.AFKEvent
                .Where(b => b.SessionID ==SessionId)
                .SumAsync(e => EF.Functions.DateDiffMinute(e.EndAt, e.StartAt)) ?? 0; //Default to 0 if null (no afk events)

            var workedHours = (SessionDuration.TotalMinutes - AFKDruation) / 60;

            var multiplierID = await _multiplier.getMultiplierID();

            if (workedHours > 9) //CHANGE LATER TO BE IN CONFIGURATION FILE NOT HARDCODED !!!!!!!!!!!!!!!!!!!!
            {
                workedHours = workedHours - 9;
                var overtime = new Overtime
                {
                    EmployeeID = session.EmployeeID,
                    SessionID = SessionId,
                    OvertimeRuleID = multiplierID,
                    Date = DateTime.UtcNow.Date,
                    HoursWorked = workedHours
                };
                _context.Overtime.Add(overtime);
                await _context.SaveChangesAsync();
            }

            return "Overtime Added!";
        }

    }
}
