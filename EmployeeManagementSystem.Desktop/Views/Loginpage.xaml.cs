using EmployeeManagementSystem.Desktop.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace EmployeeManagementSystem.Desktop
{
    public partial class Loginpage : Window
    {
        private static readonly HttpClient client = new HttpClient();

        public Loginpage()
        {
            InitializeComponent();
            
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string email = UsernameBox.Text;   
            string password = PasswordBox.Password;

            var loginRequest = new
            {
                Email = email,
                Password = password
            };

            string json = JsonSerializer.Serialize(loginRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                // Adjust the URL if your API runs on a different portttttttttttttttttttttttttttttttttttttttttttttttt
                var response = await client.PostAsync("https://localhost:5001/api/auth/login", content);

                if (response.IsSuccessStatusCode)
                {
                    string responseJson = await response.Content.ReadAsStringAsync();

                    // Parse { "token": "..." }
                    using var doc = JsonDocument.Parse(responseJson);
                    string token = doc.RootElement.GetProperty("token").GetString();

                    MessageBox.Show("✅ Login successful!", "Welcome");

                    // Save token for future API calls bas hanshofha ba3den 
                    //Properties.Settings.Default.JwtToken = token;
                    //Properties.Settings.Default.Save();

                    // Navigate to dashboard
                    //var dashboard = new DashboardWindow();
                    //dashboard.Show();
                    //this.Close();
                }
                else
                {
                    MessageBlock.Text = "❌ Invalid email or password.";
                }
            }
            catch (Exception ex)
            {
                MessageBlock.Text = "⚠️ Error connecting to server: " + ex.Message;
            }
        }
    }
}
