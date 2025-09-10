using EmployeeManagementSystem.Desktop.ViewModels;
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

namespace EmployeeManagementSystem.Desktop
{
    /// <summary>
    /// Interaction logic for MainEmployeeDashboard.xaml
    /// </summary>
    public partial class MainEmployeeDashboard : UserControl
    {
        public MainEmployeeDashboard()
        {
            InitializeComponent();

            // In real app, bind to a ViewModel with actual data
            DataContext = new EmployeeDashboardViewModel();
        }

        private void Check_in_button_Click(object sender, RoutedEventArgs e)

        {

        }

        private void Check_in_button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void Check_out_button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
