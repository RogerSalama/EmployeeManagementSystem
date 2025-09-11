using System;
using System.Diagnostics.Metrics;
using EmployeeManagementSystem.API.Data;
using EmployeeManagementSystem.Entities;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagementSystem.API.Services
{
    public class PunchService
    {
        private readonly timeStamp _timestamp;

        private static List<string> _vector = new List<string>();
        private readonly ApplicationDbContext _context;
        public PunchService(timeStamp timeStamp,ApplicationDbContext context)
        {
            _timestamp = timeStamp;
            _context = context;
        }
        public async Task<int> ProjectCheckin(DateTime timestamp,int EmployeeID,int ProjectID)
        {
            // Check if there's an open session for this employee in the app
            var openSession = await _context.Attendance
                .FirstOrDefaultAsync(a => a.EmployeeID ==EmployeeID
                                       && a.CheckOut == null);

            if (openSession != null)
            {
                // Already has an open session → reject or return existing SessionId
                return 0; // or return openSession.SessionId;
            }

            var attendance = new Attendance
            {
                CheckIn = timestamp,
                EmployeeID = EmployeeID,
                Date = DateTime.Today,
            };

            _context.Attendance.Add(attendance);
            await _context.SaveChangesAsync();

            var Emp_proj = new Employee_Project
            {
                StartTime = timestamp,
                ProjectID = ProjectID,
                EmployeeID = EmployeeID,
                SessionID = attendance.SessionID
            };

            return attendance.SessionID;
        }
       
        public async void PunchEOD()
        {
            // 1️ Take first and last elements
            string firstTimestamp = _vector.First();
            string lastTimestamp = _vector.Last();

            // 2️ Parse to DateTime
            if (DateTime.TryParseExact(firstTimestamp, "HH:mm:ss", null,
                                       System.Globalization.DateTimeStyles.None,
                                       out DateTime firstTime) &&
                DateTime.TryParseExact(lastTimestamp, "HH:mm:ss", null,
                                       System.Globalization.DateTimeStyles.None,
                                       out DateTime lastTime))
            {
                // 3️ Calculate working duration
                TimeSpan totalWorked = lastTime - firstTime;
                // 4 send totalWorked to database

                // Console.WriteLine($"{ts.Hours}h {ts.Minutes}m"); // 13h 45m
                
                
                // Console.WriteLine($"First Punch: {firstTime}");
                // Console.WriteLine($"Last Punch: {lastTime}");
                // Console.WriteLine($"Total Worked: {totalWorked}");
            }
            else
            {
                Console.WriteLine("❌ Failed to parse timestamps");
            }



            // 1) vector get sent to database (still don't know where exactly)
            // 2) this function will only work at 12:00 am everyday
            // 3) takes vector array as parameter filled with employee's checkins and using a for loop
            //    we process the data so we can send it to the database
            // ######################################################################
            // 1) the employee's total working time calculation will also be sent to database
            // 2) we can create a table that takes the employee id as foreign key , checkins list and today's date
            // ######################################################################
            // 1) we create 2 functions that we call on project btn click when check-in is clicked
            //  these 2 functions :
            //      a) updates the starttime for employee project when the project is selected from the dropdown menu
            //      b) updates the endtime for employee project when the project is switched from the dropdown menu

        }

    }
}
