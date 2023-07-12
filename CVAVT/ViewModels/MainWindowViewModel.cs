using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CVAVT.ViewModels;
using CVAVT.Views;

namespace CVAVT.ViewModels
{
    class MainWindowViewModel : WpfLibrary.BaseViewModel
    {
        // Neuer Teilnehmer
        public ICommand NeuTeilnehmerCmd { get; set; }
        public ICommand NeuAktivitaetCmd { get; set; }

        // Konstruktor
        public MainWindowViewModel()
        {
            NeuTeilnehmerCmd = new WpfLibrary.RelayCommand(NeuerTeilnehmerMenu);
            NeuAktivitaetCmd = new WpfLibrary.RelayCommand(NeuAktivitaet);
        }



        private void NeuerTeilnehmerMenu()
        {
            NeuerTeilnehmer window = new NeuerTeilnehmer();
            window.ShowDialog();

        }
        private void NeuAktivitaet()
        {
            NeueAktivitaet window = new NeueAktivitaet();
            window.ShowDialog();

        }
    }
}
