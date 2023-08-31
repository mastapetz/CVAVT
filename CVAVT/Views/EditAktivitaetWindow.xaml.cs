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
    /// Interaction logic for EditActivityWindow.xaml
    /// </summary>

    public partial class EditActivityWindow : Window
    {
        private EditAktivitaetViewModel _viewModel;
        //private bool _useMSSQLSMVerbindung;

        public EditActivityWindow(Aktivitaet aktiv)
        {
            InitializeComponent();
            _viewModel = new EditAktivitaetViewModel(aktiv);
            this.DataContext = _viewModel;

            // Registrierung am Event
            // Wir registrieren eine Lamda-Expression
            _viewModel.OnRequestCloseWin += (sender, args) => this.Close();
            //_useMSSQLSMVerbindung = useMSSQLSMVerbindung;
        }
    }
}
