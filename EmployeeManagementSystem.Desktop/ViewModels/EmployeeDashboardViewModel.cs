using EmployeeManagementSystem.Desktop.Commands;
using EmployeeManagementSystem.Desktop.Models;
using EmployeeManagementSystem.Desktop.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace EmployeeManagementSystem.Desktop.ViewModels
{
    public class EmployeeDashboardViewModel : BaseViewModel
    {
        private readonly AttendanceService _attendanceService;
        private readonly ProjectService _projectService;

        public ObservableCollection<ProjectDto> Projects { get; } = new ObservableCollection<ProjectDto>();

        private ProjectDto _selectedProject;
        public ProjectDto SelectedProject
        {
            get => _selectedProject;
            set
            {
                SetProperty(ref _selectedProject, value);
                // Notify command manager that CanExecute might have changed
                ((RelayCommand)CheckInCommand).RaiseCanExecuteChanged();
            }
        }

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
            _projectService = new ProjectService();

            CheckInCommand = new RelayCommand(async _ => await CheckInAsync(), _ => SelectedProject != null);
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



        public async Task InitializeAsync()
        {
            await LoadProjectsAsync();
        }

        private async Task LoadProjectsAsync()
        {
            try
            {
                var list = await _projectService.GetAssignedProjectsAsync();
                Projects.Clear();
                foreach (var p in list) Projects.Add(p);
            }
            catch (Exception ex)
            {
                // show user friendly error, log real error
                MessageBox.Show("Failed to load projects: " + ex.Message);
            }
        }



        private async Task CheckInAsync()
        {
            if (SelectedProject == null)
            {
                MessageBox.Show("Please select a project first.");
                return;
            }

            try
            {
                bool ok = await _attendanceService.CheckInAsync(SelectedProject.Id);
                if (ok)
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
                MessageBox.Show("Error during check-in: " + ex.Message);
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
