using EmployeeManagementSystem.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using EmployeeManagementSystem.Desktop.Commands;

namespace EmployeeManagementSystem.Desktop.ViewModels
{
    public class EmployeeDashboardViewModel : BaseViewModel
    {
        private readonly AttendanceService _attendanceService;

        public ICommand CheckInCommand { get; }
        public ICommand CheckOutCommand { get; }

        // Track employee's selected project
        private Guid _selectedProjectId;
        public Guid SelectedProjectId
        {
            get => _selectedProjectId;
            set => SetProperty(ref _selectedProjectId, value);
        }

        public EmployeeDashboardViewModel()
        {
            _attendanceService = new AttendanceService();

            CheckInCommand = new RelayCommand(async (_) => await CheckInAsync());
            CheckOutCommand = new RelayCommand(async (_) => await CheckOutAsync());
        }

        // =========================
        // Visibility
        // =========================
        private Visibility _checkInVisibility = Visibility.Visible;
        public Visibility CheckInVisibility
        {
            get => _checkInVisibility;
            set => SetProperty(ref _checkInVisibility, value);
        }

        private Visibility _checkOutVisibility = Visibility.Collapsed;
        public Visibility CheckOutVisibility
        {
            get => _checkOutVisibility;
            set => SetProperty(ref _checkOutVisibility, value);
        }
        // =========================

        private async Task CheckInAsync()
        {
            try
            {

                System.Diagnostics.Debug.WriteLine("Reached CheckInAsync, before calling service");
                CheckInVisibility = Visibility.Collapsed;
                CheckOutVisibility = Visibility.Visible;
                bool success = await _attendanceService.CheckInAsync(SelectedProjectId);

                if (success)
                {
                    MessageBox.Show("✅ Check-in successful!");
                    CheckInVisibility = Visibility.Collapsed;
                    CheckOutVisibility = Visibility.Visible;
                }
                else
                {
                    MessageBox.Show("❌ Check-in failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠ Error: {ex.Message}");
            }
        }

        private async Task CheckOutAsync()
        {
            try
            {
                // Example: you'd collect this from the UI
                var timeLogs = new List<TimeLogInput>
                {
                    new TimeLogInput { ProjectId = SelectedProjectId, DurationMinutes = 120 }
                };

                bool success = await _attendanceService.CheckOutAsync(timeLogs);

                if (success)
                    MessageBox.Show("✅ Check-out successful!");
                else
                    MessageBox.Show("❌ Check-out failed. Please try again.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"⚠ Error: {ex.Message}");
            }
        }
    }
}
