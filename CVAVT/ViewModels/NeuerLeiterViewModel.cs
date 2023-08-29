using CVAVT.Models;
using CVAVT.SQLiteDB;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace CVAVT.ViewModels
{
    internal class NeuerLeiterViewModel : WpfLibraryLT.BaseViewModel
    {
        // Commands
        public ICommand EingabeVerwerfenCmd { get; set; }
        public ICommand EingabeSpeichernCmd { get; set; }

        // Event zum Schließen
        public event EventHandler OnRequestCloseWindow;
        // Properties für Leiternamen
        private string _leiterName;
        public string LeiterName
        {
            get { return _leiterName; }
            set
            {
                _leiterName = value;
                OnPropertyChanged(nameof(LeiterName));
            }
        }
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

        public NeuerLeiterViewModel(bool useMSSQLSMVerbindung)
        {
            EingabeVerwerfenCmd = new WpfLibraryLT.RelayCommand(EingabeVerwerfen);
            EingabeSpeichernCmd = new WpfLibraryLT.RelayCommand(EingabeSpeichern);
            // für Datenbank change
            _useMSSQLSMVerbindung = useMSSQLSMVerbindung;
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
        private void EingabeVerwerfen()
        {
            Verlassen();
        }



        private void EingabeSpeichern()
        {
            //Leiter neuerLeiter = new Leiter();
            //Erzeugt eine neue Instanz von Leiter, und LeiterName wird den Standardwert null haben(falls LeiterName ein string ist).

            // Leiter neuerLeiter = new Leiter { LeiterName = LeiterName };
            // Erzeugt eine neue Instanz von Leiter und weist LeiterName den Wert von LeiterName im ViewModel zu

            Leiter neuerLeiter = new Leiter
            {
                LeiterName = LeiterName
            };

            if (_useMSSQLSMVerbindung)
            {
                // für MSSQL
                using (CVAVTContext context = new CVAVTContext())
                {
                    context.Leiter.Add(neuerLeiter);
                    context.SaveChanges();
                }
            }
            else
            {
                // für SQLite
                using (SQLiteKontext context = new SQLiteKontext())
                {
                    context.Leiter.Add(neuerLeiter);
                    context.SaveChanges();
                }
            }

            Verlassen();

        }

    }
}
