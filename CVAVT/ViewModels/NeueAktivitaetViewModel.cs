using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    internal class NeueAktivitaetViewModel : WpfLibrary.BaseViewModel
    {
        // AktiviaetAnlegen
        public ICommand AktivitaetAnlegenCmd { get; set; }

        // Anlegen Abbrechen
        public ICommand AnlegenAbbrechenCmd { get; set; }

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
        public NeueAktivitaetViewModel()
        {
            AktivitaetAnlegenCmd = new WpfLibrary.RelayCommand(AktivitaetAnlegen);
            AnlegenAbbrechenCmd = new WpfLibrary.RelayCommand(AnlegenAbbrechen);
        }

        private void AktivitaetAnlegen()
        {
            throw new NotImplementedException();
        }

        private void AnlegenAbbrechen()
        {
            throw new NotImplementedException();
        }
    }
}
