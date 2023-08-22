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
using System.Windows.Navigation;
using System.Windows.Shapes;
using CVAVT.ViewModels;
using CVAVT.Views;

namespace CVAVT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private MainWindowViewModel _viewModel;
        private bool isDarkMode;

        public MainWindow()
        {

            InitializeComponent();
            _viewModel = new MainWindowViewModel();
            this.DataContext = _viewModel;

            _viewModel.OnRequestCloseWindow += (sender, args) => this.Close();

        }
        //Toggle
        private void ToggleButton_Click(object sender, RoutedEventArgs e)
        {
            ToggleTheme();
        }
        private void ToggleTheme()
        {
            if (isDarkMode)
            {
                // Wechsel zu Light Mode
                Resources["ButtonStyle"] = FindResource("LightButtonStyle");
            }
            else
            {
                // Wechsel zu Dark Mode
                Resources["ButtonStyle"] = FindResource("DarkButtonStyle");
            }

            isDarkMode = !isDarkMode;
        }

    }
}
