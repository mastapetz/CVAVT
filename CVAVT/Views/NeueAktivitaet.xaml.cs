using CVAVT.Models;
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
    /// Interaction logic for NeueAktivitaet.xaml
    /// </summary>
    public partial class NeueAktivitaet : Window
    {
        private NeueAktivitaetViewModel _viewModel;
        public NeueAktivitaet(Aktivitaet aktiv)
        {
            InitializeComponent();
            _viewModel = new NeueAktivitaetViewModel(aktiv);
            this.DataContext = _viewModel;

            // Registrierung am Event
            // Wir registrieren eine Lamda-Expression
            _viewModel.OnRequestCloseWin += (sender, args) => this.Close();
        }

        // Damit heutiges Datum beim Laden angezeigt wird statt 01.01 0001
        private void AktivitaetenDatePicker_Loaded(object sender, RoutedEventArgs e)
        {
            AktivitaetenDatePicker.SelectedDate = DateTime.Today;
        }
    }
}
