using CVAVT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CVAVT.HilfsKlassen;

namespace CVAVT.ViewModels
{
    internal class EditAktivitaetViewModel : WpfLibrary.BaseViewModel
    {
        // Commands
        public ICommand EditSaveCmd { get; set; }
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
            EditSaveCmd = new WpfLibrary.RelayCommand(EditSave);
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
        /// Hilfsmethode zum Kombinieren von Datum und Zeit für Datenbank Speicherung
        /// </summary>
        /// <param name="date"></param>
        /// <param name="time"></param>
        /// <returns></returns>
        private DateTime CombineDateAndTime(DateTime date, DateTime time)
        {
            return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
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



        /// <summary>
        /// EditSave ist das Command zum Speichern der Änderungen
        /// </summary>
        private void EditSave()
        {
            using (CVAVTContext context = new CVAVTContext())
            {
                Aktivitaet aktivEd = context.Aktivitaet.Where(a => a.AktivitaetenId == _aktivitaet.AktivitaetenId).FirstOrDefault();
                if (aktivEd != null)
                {
                    if (!PruefHelfer.FelderGueltig(AktivitaetenName, SelectedLeiter, AktivitaetenArt, AktivitaetenDauer, AktivitaetenInformation)) { }
                    else
                    {
                        aktivEd.AktivitaetenName = AktivitaetenName;
                        // Für Combobox
                        aktivEd.LeiterIdfk = SelectedLeiter.LeiterId;
                        // ----
                        aktivEd.AktivitaetenArt = AktivitaetenArt;

                        // Umänderungen damit Datum und Uhrzeit besser und richtig verarbeitet werden
                        aktivEd.AktivitaetenDatum = CombineDateAndTime(AktivitaetenDatum, AktivitaetenZeit);
                        aktivEd.AktivitaetenZeit = CombineDateAndTime(AktivitaetenDatum, AktivitaetenZeit);
                        // -- ursprünglich
                        //aktivEd.AktivitaetenDatum = AktivitaetenDatum;
                        //aktivEd.AktivitaetenZeit = AktivitaetenZeit;
                        // -------
                        aktivEd.AktivitaetenDauer = AktivitaetenDauer;
                        aktivEd.AktivitaetenMaxTeilnehmer = AktivitaetenMaxTeilnehmer;
                        aktivEd.AktivitaetenVorwissenNoetig = AktivitaetenVorwissenNoetig;
                        aktivEd.AktivitaetenInformation = AktivitaetenInformation;
                        context.SaveChanges();
                        Verlassen();
                    }

                }

            }

        }
        /// <summary>
        /// Verlässt ohne Änderungen zu Speichern
        /// </summary>
        private void EditAbbrechen()
        {
            Verlassen();
        }


    }
}
