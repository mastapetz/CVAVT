using CVAVT.ViewModels;
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

namespace CVAVT.Views
{
    /// <summary>
    /// Interaction logic for NeuerLeiter.xaml
    /// </summary>
    public partial class NeuerLeiter : Window
    {
        private NeuerLeiterViewModel _viewModel;
        public NeuerLeiter()
        {
            InitializeComponent();
            _viewModel = new NeuerLeiterViewModel();
            this.DataContext = _viewModel;

            _viewModel.OnRequestCloseWindow += (sender, args) => this.Close();

        }
    }
}
