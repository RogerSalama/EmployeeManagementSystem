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

           
            DataContext = new EmployeeDashboardViewModel();
            this.Loaded += async (s, e) =>
            {
                if (this.DataContext is EmployeeDashboardViewModel vm)
                {
                    // pass real API base URL and token setup if needed
                    await vm.InitializeAsync();
                }
            };
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
