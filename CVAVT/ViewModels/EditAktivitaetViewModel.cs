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
    internal class EditAktivitaetViewModel : WpfLibrary.BaseViewModel
    {
        // Commands
        public ICommand AktivitaetEditCmd { get; set; }
        public ICommand EditAbbrechenCmd { get; set; }

        // Event dient zur Entkopplung zwischen ViewModel und View 
        public event EventHandler OnRequestCloseWin;
        // Aktivitaet  Properties
        public string AktivitaetenName { get; set; }
        public string AktivitaetenArt { get; set; }
        public DateTime AktivitaetenDatum { get; set; }
        public DateTime AktivitaetenZeit { get; set; }
        public double AktivitaetenDauer { get; set; }
        public int? AktivitaetenMaxTeilnehmer { get; set; }
        public bool AktivitaetenVorwissenNoetig { get; set; }
        public string AktivitaetenInformation { get; set; }

        private Aktivitaet _aktivitaet;

        // Für Leiter Combobox
        public ObservableCollection<Leiter> LeiterListe { get; set; }
        private Leiter _leiter;
        public Leiter SelectedLeiter
        {
            get { return _leiter; }
            set
            {
                _leiter = value;
                OnPropertyChanged("SelectedLeiter");
            }
        }
        // -------------------------------


        // Konstruktor
        public EditAktivitaetViewModel(Aktivitaet aktivitaet)
        {
            LeiterListe = new ObservableCollection<Leiter>();
            AktivitaetEditCmd = new WpfLibrary.RelayCommand(AktivitaetEdit);
            EditAbbrechenCmd = new WpfLibrary.RelayCommand(EditAbbrechen);

            // DB Verbindung zum füllen derLeiter Liste
            using (CVAVTContext context = new CVAVTContext())
            {
                var leiterListe = context.Leiter.OrderBy(l => l.LeiterName);
                foreach (var leiter in leiterListe)
                {
                    LeiterListe.Add(leiter);
                }
            }
            if (aktivitaet != null)
            {
                // Die Daten aus aktivität werden in die Properties geschrieben
                AktivitaetenName = aktivitaet.AktivitaetenName;
                // Für Combobox
                SelectedLeiter = aktivitaet.LeiterIdfkNavigation;
                // ----
                AktivitaetenArt = aktivitaet.AktivitaetenArt;
                AktivitaetenDatum = aktivitaet.AktivitaetenDatum;
                AktivitaetenZeit = aktivitaet.AktivitaetenZeit;
                AktivitaetenDauer = aktivitaet.AktivitaetenDauer;
                AktivitaetenMaxTeilnehmer = aktivitaet.AktivitaetenMaxTeilnehmer;
                AktivitaetenVorwissenNoetig = aktivitaet.AktivitaetenVorwissenNoetig;
                AktivitaetenInformation = aktivitaet.AktivitaetenInformation;
            }
            _aktivitaet = aktivitaet;
        }


        /// <summary>
        /// Verlassen schließt das Fenster
        /// </summary>
        private void Verlassen()
        {
            // Window schließen
            if (OnRequestCloseWin != null)
                // Delegate wird aufgerufen
                OnRequestCloseWin(this, new EventArgs());

        }


        /// <summary>
        /// AktivitaetEdit ist das Command zum Speichern der Änderungen
        /// </summary>
        private void AktivitaetEdit()
        {
            using (CVAVTContext context = new CVAVTContext())
            {
                Aktivitaet aktivEd = context.Aktivitaet.Where(a => a.AktivitaetenId == _aktivitaet.AktivitaetenId).FirstOrDefault();
                if (aktivEd != null)
                {
                    aktivEd.AktivitaetenName = AktivitaetenName;
                    // Für Combobox
                    aktivEd.LeiterIdfk = SelectedLeiter.LeiterId;
                    // ----
                    aktivEd.AktivitaetenArt = AktivitaetenArt;
                    aktivEd.AktivitaetenDatum = AktivitaetenDatum;
                    aktivEd.AktivitaetenZeit = AktivitaetenZeit;
                    aktivEd.AktivitaetenDauer = AktivitaetenDauer;
                    aktivEd.AktivitaetenMaxTeilnehmer = AktivitaetenMaxTeilnehmer;
                    aktivEd.AktivitaetenVorwissenNoetig = AktivitaetenVorwissenNoetig;
                    aktivEd.AktivitaetenInformation = AktivitaetenInformation;
                    context.SaveChanges();

                }
                Verlassen();
            }

        }

        private void EditAbbrechen()
        {
            Verlassen();
        }


    }
}
