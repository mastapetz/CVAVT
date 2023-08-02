﻿using CVAVT.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    internal class NeueAktivitaetViewModel : WpfLibrary.BaseViewModel
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

        // Konstruktor
        public NeueAktivitaetViewModel(Aktivitaet aktivitaet)
        {


            LeiterListe = new ObservableCollection<Leiter>();
            AktivitaetAnlegenCmd = new WpfLibrary.RelayCommand(AktivitaetAnlegen);
            AnlegenAbbrechenCmd = new WpfLibrary.RelayCommand(AnlegenAbbrechen);

            // DB Verbindung 
            using (CVAVTContext context = new CVAVTContext())
            {
                // Zum füllen der Leiter Liste
                var leiterListe = context.Leiter.OrderBy(l => l.LeiterName);
                foreach (var leiter in leiterListe)
                {
                    LeiterListe.Add(leiter);
                }
            }

            //// Wenn aktivitaet null ist, wird ein neues Aktivitaet-Objekt erstellt
            //if (aktivitaet == null)
            //{
            //    _aktivitaet = new Aktivitaet();
            //}
            //else
            //{
            //    _aktivitaet = aktivitaet;
            //}

            //_aktivitaet = aktivitaet;
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
            using (CVAVTContext context = new CVAVTContext())
            {
                if (_aktivitaet == null)
                {
                    // Neuer Eintrag in DB
                    // das Aktivitaet-Objekt direkt während der Initialisierung erstellt und seine Eigenschaften werden in derselben Zeile zugewiesen
                    Aktivitaet aktivitaet = new Aktivitaet
                    {
                        // Objekt Füllen
                        AktivitaetenName = AktivitaetenName,
                        // Für Combobox
                        // LeiterId wird LeiterIdfk zugewisen
                        LeiterIdfk = SelectedLeiter.LeiterId,
                        // ----
                        AktivitaetenArt = AktivitaetenArt,
                        AktivitaetenDatum = AktivitaetenDatum,
                        AktivitaetenZeit = AktivitaetenZeit,
                        AktivitaetenDauer = AktivitaetenDauer,
                        AktivitaetenMaxTeilnehmer = AktivitaetenMaxTeilnehmer,
                        AktivitaetenVorwissenNoetig = AktivitaetenVorwissenNoetig,
                        AktivitaetenInformation = AktivitaetenInformation,
                    };
                    context.Aktivitaet.Add(aktivitaet);
                    context.SaveChanges();
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
