﻿using System;
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
        public int TeilnehmerIst { get; }
        public int TeilnehmerMax { get; }

        public event EventHandler OnRequestCloseWindow;

        // Konstruktor
        public NeuerTeilnehmerViewModel()
        {
            EingabeVerwerfenCmd = new WpfLibrary.RelayCommand(EingabeVerwerfen);
            NaechsterTeilnehmerCmd = new WpfLibrary.RelayCommand(NaechsterTeilnehmer);
            EingabeSpeichernCmd = new WpfLibrary.RelayCommand(EingabeSpeichern);
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
            throw new NotImplementedException();
        }
    }
}
