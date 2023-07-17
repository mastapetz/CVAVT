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

        // Neue Aktivität
        public ICommand NeuAktivitaetCmd { get; set; }

        // Edit Aktivität
        public ICommand EditAktiviaetCmd { get; set; }

        // Teilnehmer Liste Zeigen
        public ICommand ShowTeilnehmerCmd { get; set; }

        // Program Beenden
        public ICommand BeendenCmd { get; set; }

        // Properties
        // Aktivitaet  Properties
        public string AktivitaetenName { get; set; }
        public string AktivitaetenLeiter { get; set; }
        public string AktivitaetenArt { get; set; }
        public DateOnly AktivitaetenDatum { get; set; }
        public TimeOnly AktivitaetenZeit { get; set; }
        public double AktivitaetenDauer { get; set; }
        public int AktivitaetenMaxTeilnehmer { get; set; }
        public bool AktivitaetenVorwissenNoetig { get; set; }
        public string AktivitaetenInformation { get; set; }

        // Konstruktor
        public MainWindowViewModel()
        {
            NeuTeilnehmerCmd = new WpfLibrary.RelayCommand(NeuerTeilnehmerMenu);
            NeuAktivitaetCmd = new WpfLibrary.RelayCommand(NeuAktivitaet);
            EditAktiviaetCmd = new WpfLibrary.RelayCommand(EditAktivitaet);
            ShowTeilnehmerCmd = new WpfLibrary.RelayCommand(ShowTeilnehmer);
            BeendenCmd = new WpfLibrary.RelayCommand(Schliessen);
        }

        private void Schliessen()
        {
            throw new NotImplementedException();
        }

        private void ShowTeilnehmer()
        {
            TeilnehmerListe window = new TeilnehmerListe();
            window.Show();
        }

        private void EditAktivitaet()
        {
            EditActivityWindow window = new EditActivityWindow();
            window.ShowDialog();
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
