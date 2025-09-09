using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem.Desktop.Services
{
    public class AttendanceService
    {
        private readonly HttpClient _httpClient;

        public AttendanceService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://localhost:8000/api/") // Replace with your actual API URL
            };

            // Add JSON headers
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json")
            );


            _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", token);

        }

        // =========================
        // CHECK-IN API REQUEST
        // =========================
        public async Task<bool> CheckInAsync(Guid projectId)
        {
            var data = new { projectId = projectId };

            var json = JsonConvert.SerializeObject(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("attendance/checkin", content);

            return response.IsSuccessStatusCode;
        }

        // =========================
        // CHECK-OUT API REQUEST
        // =========================
        //public async Task<bool> CheckOutAsync(List<TimeLogInput> timeLogs)
        //{
        //    var data = new { timeLogs = timeLogs };

        //    var json = JsonConvert.SerializeObject(data);
        //    var content = new StringContent(json, Encoding.UTF8, "application/json");

        //    var response = await _httpClient.PostAsync("attendance/checkout", content);

        //    return response.IsSuccessStatusCode;
        //}
    }

    //public class TimeLogInput
    //{
    //    public Guid ProjectId { get; set; }
    //    public int DurationMinutes { get; set; }
    //}
}
