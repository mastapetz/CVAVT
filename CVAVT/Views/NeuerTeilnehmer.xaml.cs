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
    /// Interaction logic for NeuerTeilnehmer.xaml
    /// </summary>
    public partial class NeuerTeilnehmer : Window
    {

        //// änderung für Update Liste
        //private NeuerTeilnehmerViewModel _viewModel;
        //private TeilnehmerListeViewModel _teilnehmerListeViewModel;

        //public NeuerTeilnehmer(Aktivitaet aktiv, Teilnehmer teilnehmer, TeilnehmerListeViewModel teilnehmerListeViewModel)
        //{
        //    InitializeComponent();
        //    _viewModel = new NeuerTeilnehmerViewModel(aktiv, teilnehmer);
        //    _teilnehmerListeViewModel = teilnehmerListeViewModel;
        //    this.DataContext = _viewModel;

        //    _viewModel.OnRequestCloseWindow += (sender, args) => this.Close();
        //}



        private NeuerTeilnehmerViewModel _viewModel;
        //private bool _useMSSQLSMVerbindung;
        public NeuerTeilnehmer(Aktivitaet aktiv, Teilnehmer teilnehmer)
        {
            InitializeComponent();
            //_useMSSQLSMVerbindung = useMSSQLSMVerbindung;
            _viewModel = new NeuerTeilnehmerViewModel(aktiv, teilnehmer);
            this.DataContext = _viewModel;

            _viewModel.OnRequestCloseWindow += (sender, args) => this.Close();

        }
    }
}
