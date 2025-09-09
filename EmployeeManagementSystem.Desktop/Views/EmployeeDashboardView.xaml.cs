using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EmployeeManagementSystem.Desktop.Views
{
    /// <summary>
    /// Interaction logic for EmployeeDashboardView.xaml
    /// </summary>
    public partial class EmployeeDashboardView : Window
    {
        public EmployeeDashboardView()
        {
            InitializeComponent();
        }

        private void CheckInButton_Click(object sender, RoutedEventArgs e)
        {
            CheckInButton.IsEnabled = false;
            CheckOutButton.IsEnabled = true;

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void CheckOutButton_Click(object sender, RoutedEventArgs e)
        {
            CheckOutButton.IsEnabled = false;
            CheckInButton.IsEnabled = true;

        }
    }
}
