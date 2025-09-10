using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using EmployeeManagementSystem.Desktop.Models;

namespace EmployeeManagementSystem.Desktop.Services
{
    public class ProjectService
    {
        private readonly HttpClient _httpClient;

        public ProjectService(string bearerToken = null)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:8000/api/") };
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(bearerToken))
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", bearerToken);
        }

        // GET assigned projects for logged-in employee
        public async Task<List<ProjectDto>> GetAssignedProjectsAsync()
        {
            var resp = await _httpClient.GetAsync("employees/me/projects");
            resp.EnsureSuccessStatusCode();
            var json = await resp.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<ProjectDto>>(json);
        }

    }
}
