using CVAVT.Models;
using CVAVT.SQLiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    internal class NeueAktivitaetViewModel : WpfLibraryLT.BaseViewModel
    {
        // Commands
        public ICommand AktivitaetAnlegenCmd { get; set; }
        public ICommand AnlegenAbbrechenCmd { get; set; }

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
        // für Datenbank change
        private bool _useMSSQLSMVerbindung = true;
        public bool UseMSSQLSMVerbindung
        {
            get { return _useMSSQLSMVerbindung; }
            set
            {
                if (_useMSSQLSMVerbindung != value)
                {
                    _useMSSQLSMVerbindung = value;
                    OnPropertyChanged(nameof(UseMSSQLSMVerbindung)); // Stelle sicher, dass das UI über die Änderung informiert wird
                }
            }
        }

        // Konstruktor
        public NeueAktivitaetViewModel(Aktivitaet aktivitaet, bool useMSSQLSMVerbindung)
        {
            AktivitaetAnlegenCmd = new WpfLibraryLT.RelayCommand(AktivitaetAnlegen);
            AnlegenAbbrechenCmd = new WpfLibraryLT.RelayCommand(AnlegenAbbrechen);

            // für Leiter Liste
            LeiterListe = new ObservableCollection<Leiter>();

            // für Datenbank change
            _useMSSQLSMVerbindung = useMSSQLSMVerbindung;

            if (_useMSSQLSMVerbindung)
            {
                // DB Verbindung  MSSQL
                using (CVAVTContext context = new CVAVTContext())
                {
                    // Zum füllen der Leiter Liste
                    var leiterListe = context.Leiter.OrderBy(l => l.LeiterName);
                    foreach (var leiter in leiterListe)
                    {
                        LeiterListe.Add(leiter);
                    }
                }
                _aktivitaet = aktivitaet;

            }
            else
            {
                // DB Verbindung  SQLite
                using (SQLiteKontext context = new SQLiteKontext())
                {
                    // Zum füllen der Leiter Liste
                    var leiterListe = context.Leiter.OrderBy(l => l.LeiterName);
                    foreach (var leiter in leiterListe)
                    {
                        LeiterListe.Add(leiter);
                    }
                }
                _aktivitaet = aktivitaet;

            }

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

        private void AktivitaetAnlegen()
        {
            if (_useMSSQLSMVerbindung)
            {
                using (CVAVTContext context = new CVAVTContext())
                {
                    if (_aktivitaet == null)
                    {
                        // Neuer Eintrag in DB
                        Aktivitaet aktivitaet = new Aktivitaet();
                        // Objekt Füllen
                        aktivitaet.AktivitaetenName = AktivitaetenName;
                        // Für Combobox
                        // LeiterId wird LeiterIdfk zugewisen
                        aktivitaet.LeiterIdfk = SelectedLeiter.LeiterId;
                        // ----
                        aktivitaet.AktivitaetenArt = AktivitaetenArt;
                        // Umänderungen damit Datum und Uhrzeit besser und richtig verarbeitet werden
                        aktivitaet.AktivitaetenDatum = CombineDateAndTime(AktivitaetenDatum, AktivitaetenZeit);
                        aktivitaet.AktivitaetenZeit = CombineDateAndTime(AktivitaetenDatum, AktivitaetenZeit);

                        aktivitaet.AktivitaetenDauer = AktivitaetenDauer;
                        aktivitaet.AktivitaetenMaxTeilnehmer = AktivitaetenMaxTeilnehmer;
                        aktivitaet.AktivitaetenVorwissenNoetig = AktivitaetenVorwissenNoetig;
                        aktivitaet.AktivitaetenInformation = AktivitaetenInformation;
                        context.Aktivitaet.Add(aktivitaet);
                        context.SaveChanges();
                    }
                }
            }
            else
            {
                using (SQLiteKontext context = new SQLiteKontext())
                {
                    if (_aktivitaet == null)
                    {
                        // Neuer Eintrag in DB
                        Aktivitaet aktivitaet = new Aktivitaet();
                        // Objekt Füllen
                        aktivitaet.AktivitaetenName = AktivitaetenName;
                        // Für Combobox
                        // LeiterId wird LeiterIdfk zugewisen
                        aktivitaet.LeiterIdfk = SelectedLeiter.LeiterId;
                        // ----
                        aktivitaet.AktivitaetenArt = AktivitaetenArt;
                        // Umänderungen damit Datum und Uhrzeit besser und richtig verarbeitet werden
                        aktivitaet.AktivitaetenDatum = CombineDateAndTime(AktivitaetenDatum, AktivitaetenZeit);
                        aktivitaet.AktivitaetenZeit = CombineDateAndTime(AktivitaetenDatum, AktivitaetenZeit);

                        aktivitaet.AktivitaetenDauer = AktivitaetenDauer;
                        aktivitaet.AktivitaetenMaxTeilnehmer = AktivitaetenMaxTeilnehmer;
                        aktivitaet.AktivitaetenVorwissenNoetig = AktivitaetenVorwissenNoetig;
                        aktivitaet.AktivitaetenInformation = AktivitaetenInformation;
                        context.Aktivitaet.Add(aktivitaet);
                        context.SaveChanges();
                    }
                }
            }

            Verlassen();
        }

        private void AnlegenAbbrechen()
        {
            Verlassen();
        }
    }
}
