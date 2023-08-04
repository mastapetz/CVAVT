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
    /// Interaction logic for TeilnehmerListe.xaml
    /// </summary>
    public partial class TeilnehmerListe : Window
    {
        private TeilnehmerListeViewModel _viewModel;

        public TeilnehmerListe(Aktivitaet aktiv)
        {
            InitializeComponent();
            _viewModel = new TeilnehmerListeViewModel(aktiv);


            this.DataContext = _viewModel;
            // Registrierung am Event
            // Wir registrieren eine Lamda-Expression
            _viewModel.OnRequestCloseWindow += (sender, args) => this.Close();

            // Hier wird das DataGrid.CellEditEnding-Event abonniert
            TeilnehmerListeGrid.CellEditEnding += DataGrid_CellEditEnding;
        }

        // Eventhandler für das Ende der Zellenbearbeitung
        private void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var viewModel = DataContext as TeilnehmerListeViewModel;
            viewModel?.DataGrid_CellEditEnding(sender, e);
        }
    }
}
