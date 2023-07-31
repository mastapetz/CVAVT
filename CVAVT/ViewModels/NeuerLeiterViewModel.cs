using CVAVT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    internal class NeuerLeiterViewModel : WpfLibrary.BaseViewModel
    {
        // Commands
        public ICommand EingabeVerwerfenCmd { get; set; }
        public ICommand EingabeSpeichernCmd { get; set; }

        // Event zum Schließen
        public event EventHandler OnRequestCloseWin;
        // Properties
        //public string LeiterName { get; set; }
        public ObservableCollection<Leiter> LeiterListe { get; set; }

        // Konstruktor
        public NeuerLeiterViewModel()
        {
            LeiterListe = new ObservableCollection<Leiter>();
            EingabeVerwerfenCmd = new WpfLibrary.RelayCommand(EingabeVerwerfen);
            EingabeSpeichernCmd = new WpfLibrary.RelayCommand(EingabeSpeichern);
        }
        /// <summary>
        /// Funktion Verlassen schließt das Fenster
        /// </summary>
        private void Verlassen()
        {
            // Window schließen
            if (OnRequestCloseWin != null)
                // Delegate wird aufgerufen
                OnRequestCloseWin(this, new EventArgs());

        }
        private void EingabeVerwerfen()
        {
            Verlassen();
        }



        private void EingabeSpeichern()
        {
            throw new NotImplementedException();
        }

    }
}
