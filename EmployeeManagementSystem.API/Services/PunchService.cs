using System.Diagnostics.Metrics;

namespace EmployeeManagementSystem.API.Services
{
    public class PunchService : BackgroundService
    {
        private readonly timeStamp _timestamp;

        private static List<string> _vector = new List<string>();

        public PunchService(timeStamp timeStamp)
        {
            _timestamp = timeStamp;
           
        }
        public async Task<bool> PunchingSystem(string timestamp)
        {
            _vector.Add(timestamp);
            return true;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                // get the live time
                string timeNow = await _timestamp.GetCurrentTimeAsync();


                if (timeNow == "00:00:00")
                {
                    PunchEOD();

                    // ⏳ wait 1 minute so we don’t trigger PunchEOD multiple times at 00:00
                    await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                }
                else
                {
                    // ⏳ check again in 10 seconds
                    await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
                }
            }
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
