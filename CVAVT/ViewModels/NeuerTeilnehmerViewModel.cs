﻿using CVAVT.Models;
using CVAVT.SQLiteDB;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CVAVT.ViewModels
{
    internal class NeuerTeilnehmerViewModel : WpfLibraryLT.BaseViewModel
    {
        // Commands
        public ICommand EingabeVerwerfenCmd { get; set; }
        public ICommand NaechsterTeilnehmerCmd { get; set; }
        public ICommand EingabeSpeichernCmd { get; set; }

        // Properties
        public string AktivitaetenName { get; set; }

        // damit nächster Teilnehmer das Propertychange versteht
        private string _teilnehmerName;
        public string TeilnehmerName
        {
            get { return _teilnehmerName; }
            set
            {
                if (_teilnehmerName != value)
                {
                    _teilnehmerName = value;
                    OnPropertyChanged("TeilnehmerName");
                }
            }
        }

        //public string TeilnehmerName { get; set; }


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
                // if Anweisung zum aktualisieren der  Ist Teilnehmer
                if (_aktivitaetenIstTeilnehmer != value)
                {
                    _aktivitaetenIstTeilnehmer = value;
                    OnPropertyChanged("AktivitaetenIstTeilnehmer");
                }

                // ohne aktualisierung der Ist Teilnehmer
                //_aktivitaetenIstTeilnehmer = value;
                //OnPropertyChanged("AktivitaetenIstTeilnehmer");
            }
        }
        public int? AktivitaetenMaxTeilnehmer { get; set; }

        public event EventHandler OnRequestCloseWindow;

        // für Datenbank change
        //private bool _useMSSQLSMVerbindung = true;
        //public bool UseMSSQLSMVerbindung
        //{
        //    get { return _useMSSQLSMVerbindung; }
        //    set
        //    {
        //        if (_useMSSQLSMVerbindung != value)
        //        {
        //            _useMSSQLSMVerbindung = value;
        //            OnPropertyChanged(nameof(UseMSSQLSMVerbindung)); // Stelle sicher, dass das UI über die Änderung informiert wird
        //        }
        //    }
        //}

        // Konstruktor
        public NeuerTeilnehmerViewModel(Aktivitaet aktivitaet, Teilnehmer teilnehmer)
        {
            EingabeVerwerfenCmd = new WpfLibraryLT.RelayCommand(EingabeVerwerfen);
            NaechsterTeilnehmerCmd = new WpfLibraryLT.RelayCommand(NaechsterTeilnehmer);
            EingabeSpeichernCmd = new WpfLibraryLT.RelayCommand(EingabeSpeichern);

            // für ausgewählte aktivität
            SelectedAktivitaet = aktivitaet;

            // für Datenbank change
            //_useMSSQLSMVerbindung = useMSSQLSMVerbindung;

            //if (_useMSSQLSMVerbindung)
            //{
                //using (CVAVTContext context = new CVAVTContext())
                //{
            //        if (aktivitaet == null)
            //        {
            //            throw new ArgumentNullException(nameof(aktivitaet), "Aktivitaet darf nicht null sein.");
            //        }
            //        else
            //        {
            //            // Die Daten aus aktivität werden in die Properties geschrieben
            //            AktivitaetenName = aktivitaet.AktivitaetenName;

            //            AktivitaetenMaxTeilnehmer = aktivitaet.AktivitaetenMaxTeilnehmer;

            //            AktivitaetenIstTeilnehmer = context.Teilnehmer.Count(t => t.AktivitaetIdfk == aktivitaet.AktivitaetenId);
            //        }
            //        if (teilnehmer != null)
            //        {
            //            TeilnehmerName = teilnehmer.TeilnehmerName;
            //        }
            //    }

            //}
            //else
            
                using SQLiteKontext context = new SQLiteKontext();
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
        private void SaveTeilnehmer()
        {
            //using (DbContext context = _useMSSQLSMVerbindung
            //    ? new CVAVTContext() // MSSQL Verbindung
            //    : new SQLiteKontext()) // SQLite Verbindung
            //if (_useMSSQLSMVerbindung)
            //{
            //    using (CVAVTContext context = new CVAVTContext())
            //    {

            //        // Neuer Eintrag
            //        Teilnehmer teilnehmer = new Teilnehmer();
            //        teilnehmer.TeilnehmerName = TeilnehmerName;
            //        teilnehmer.AktivitaetIdfk = SelectedAktivitaet.AktivitaetenId;
            //        context.Teilnehmer.Add(teilnehmer);
            //        context.SaveChanges();

            //        AktivitaetenIstTeilnehmer = context.Teilnehmer.Count(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId);


            //    }

            //}
            //else
            
                using SQLiteKontext context = new SQLiteKontext();
                {

                    // Neuer Eintrag
                    Teilnehmer teilnehmer = new Teilnehmer();
                    teilnehmer.TeilnehmerName = TeilnehmerName;
                    teilnehmer.AktivitaetIdfk = SelectedAktivitaet.AktivitaetenId;
                    context.Teilnehmer.Add(teilnehmer);
                    context.SaveChanges();

                    AktivitaetenIstTeilnehmer = context.Teilnehmer.Count(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId);


                }

            


        }
        private void ClearTeilnehmerName()
        {
            TeilnehmerName = string.Empty;
        }

        // Alter Name EditVerwerfen
        private void EingabeVerwerfen()
        {
            Verlassen();
        }

        private void NaechsterTeilnehmer()
        {
            SaveTeilnehmer();
            ClearTeilnehmerName();
        }
        // Alter Name EditSpeichern


        private void EingabeSpeichern()
        {
            SaveTeilnehmer();
            Verlassen();

        }
    }
}
