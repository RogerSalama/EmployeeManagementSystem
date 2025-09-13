using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Services
{
    public class OvertimeMultiplier
    {
        private readonly ApplicationDbContext _context;
        public OvertimeMultiplier(ApplicationDbContext context)
        {
            _context = context;
        }

        //FUTURE UPGRADE:    CACHE THE OVERTIMERULE TABLE SO THAT YOU DONT HAVE TO QUERY THE DATABASE EACH TIME THE FUNCTION IS CALLED
        public async Task<int> getMultiplierID()
        {
            //NOTEEEEE: HolidayCalendar MUST store DateTime.UtcNow.Date as well !!!!!!!!!!!!!
            var holiday = await _context.HolidayCalendar
                .FirstOrDefaultAsync(a => a.Date == DateTime.UtcNow.Date);

            OvertimeRule? overtime;
            if (holiday ==  null || !holiday.IsHoliday) 
            {
                var dayOfWeek = DateTime.UtcNow.DayOfWeek;

                if (dayOfWeek == DayOfWeek.Friday || dayOfWeek == DayOfWeek.Saturday) //CHANGE LATER TO BE IN CONFIGURATION FILE NOT HARDCODED !!!!!!!!!!!!!!!!!!!!
                {
                    overtime = await _context.OvertimeRule
                        .FirstOrDefaultAsync(b => b.DayType == DayTypes.WeekEnd);
                }
                else
                {
                    overtime = await _context.OvertimeRule
                        .FirstOrDefaultAsync(b => b.DayType == DayTypes.WeekDay);
                }
            }
            else
            {
                overtime = await _context.OvertimeRule
                        .FirstOrDefaultAsync(b => b.DayType == DayTypes.Holiday);
            }

            if (overtime == null) 
            {
                throw new InvalidOperationException("No valid OvertimeRule found for the given day type.");
            }
            return overtime.ID;
        }
    }
}
