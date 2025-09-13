using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagementSystem.API.Services
{
    public class DBCheckin
    {
        private readonly ApplicationDbContext _context;

        public DBCheckin(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> DBChange_proj(int sessionID, int EmployeeID, int ProjectID)
        {
            // we have to check of the session is closed --> in that case there is no session to log into
            var sessionexits = await _context.Attendance
                .Where(ss => ss.CheckOut == null)
                .OrderByDescending(ss => ss.SessionID == sessionID)
                .FirstOrDefaultAsync();

            if(sessionexits == null)
            {
                // there is no open sessions with this sessionID
                return 0;
            }

            // we have to check if the project status is Active so that the employee can log into it
            // ############_______________CODE_IN_PROGRESS_______________############

            // we have to check of the lastlog exists for the specified session and the end time is null (it is Datetime.minvalue for now)
            var lastOpenLog = await _context.EmployeeProject
             .Where(ep => ep.EmployeeID == EmployeeID && ep.EndTime == DateTime.MinValue)
             .OrderByDescending(ep => ep.SessionID) // get the latest open session
             .FirstOrDefaultAsync();
            Console.WriteLine("the last open log is ==>> ",lastOpenLog);

            if (lastOpenLog != null)
            {
                lastOpenLog.EndTime = DateTime.Now;

                var NewProjectLog = new Employee_Project
                {
                    SessionID = sessionID,
                    EmployeeID = EmployeeID,
                    StartTime = DateTime.Now,
                    ProjectID = ProjectID,
                    AdjustedAt = DateTime.MinValue,
                };

                _context.EmployeeProject.Add(NewProjectLog);
                await _context.SaveChangesAsync();
                
                return 1;
            }

            return -1;
        }
        public async Task<bool> DBCheck_out(int sessionID, int EmployeeID)
        {
            

                return true;
         }

       
        

    }
}
