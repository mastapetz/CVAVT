using CVAVT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    internal class NeuerTeilnehmerViewModel : WpfLibrary.BaseViewModel
    {
        // Commands
        public ICommand EingabeVerwerfenCmd { get; set; }
        public ICommand NaechsterTeilnehmerCmd { get; set; }
        public ICommand EingabeSpeichernCmd { get; set; }

        // Properties
        public string AktivitaetenName { get; set; }
        public string TeilnehmerName { get; set; }

        // für Ausgewählte Aktivität
        private Aktivitaet _selectedAktivitaet;
        public Aktivitaet SelectedAktivitaet
        {
            get { return _selectedAktivitaet; }
            set
            {
                _selectedAktivitaet = value;
                OnPropertyChanged("SelectedAktivitaet");
            }
        }

        private int _aktivitaetenIstTeilnehmer;
        public int AktivitaetenIstTeilnehmer
        {
            get { return _aktivitaetenIstTeilnehmer; }
            set
            {
                _aktivitaetenIstTeilnehmer = value;
                OnPropertyChanged("AktivitaetenIstTeilnehmer");
            }
        }
        public int? AktivitaetenMaxTeilnehmer { get; set; }

        public event EventHandler OnRequestCloseWindow;

        // Konstruktor
        public NeuerTeilnehmerViewModel(Aktivitaet aktivitaet, Teilnehmer teilnehmer)
        {
            EingabeVerwerfenCmd = new WpfLibrary.RelayCommand(EingabeVerwerfen);
            NaechsterTeilnehmerCmd = new WpfLibrary.RelayCommand(NaechsterTeilnehmer);
            EingabeSpeichernCmd = new WpfLibrary.RelayCommand(EingabeSpeichern);
            // für ausgewählte aktivität
            SelectedAktivitaet = aktivitaet;

            using (CVAVTContext context = new CVAVTContext())
            {
                if (aktivitaet == null)
                {
                    throw new ArgumentNullException(nameof(aktivitaet), "Aktivitaet darf nicht null sein.");
                }
                else
                {
                    // Die Daten aus aktivität werden in die Properties geschrieben
                    AktivitaetenName = aktivitaet.AktivitaetenName;

                    AktivitaetenMaxTeilnehmer = aktivitaet.AktivitaetenMaxTeilnehmer;

                    AktivitaetenIstTeilnehmer = context.Teilnehmer.Count(t => t.AktivitaetIdfk == aktivitaet.AktivitaetenId);
                }
                if (teilnehmer != null)
                {
                    TeilnehmerName = teilnehmer.TeilnehmerName;
                }
            }

        }
        /// <summary>
        /// Funktion Verlassen schließt das Fenster
        /// </summary>
        private void Verlassen()
        {
            // Window schließen
            if (OnRequestCloseWindow != null)
                // Delegate wird aufgerufen
                OnRequestCloseWindow(this, new EventArgs());

        }

        // Alter Name EditVerwerfen
        private void EingabeVerwerfen()
        {
            Verlassen();
        }

        private void NaechsterTeilnehmer()
        {
            throw new NotImplementedException();
        }
        // Alter Name EditSpeichern

        private void EingabeSpeichern()
        {
            using (CVAVTContext context = new CVAVTContext())
            {

                // Neuer Eintrag
                Teilnehmer teilnehmer = new Teilnehmer();
                teilnehmer.TeilnehmerName = TeilnehmerName;
                teilnehmer.AktivitaetIdfk = SelectedAktivitaet.AktivitaetenId;
                context.Teilnehmer.Add(teilnehmer);
                context.SaveChanges();

            }
            Verlassen();

        }
    }
}
