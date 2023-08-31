using CVAVT.Models;
using CVAVT.SQLiteDB;
using CVAVT.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    class TeilnehmerListeViewModel : WpfLibraryLT.BaseViewModel
    {
        // Commands
        public ICommand ExportListeCmd { get; set; }
        public ICommand SchliessenCmd { get; set; }
        public ICommand TeilnehmerLoeschenCmd { get; set; }

        // Eventhandler
        public event EventHandler OnRequestCloseWindow;
        // Für das Ende der Zellenbearbeitung
        public void DataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var editedTeilnehmer = e.Row.Item as Teilnehmer;
                if (editedTeilnehmer != null)
                {
                    // Änderungen speichern
                    using (CVAVTContext context = new CVAVTContext())
                    {
                        context.Entry(editedTeilnehmer).State = EntityState.Modified;
                        context.SaveChanges();
                    }
                }
            }
        }

        // Properties
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
            }
        }
        public int? AktivitaetenMaxTeilnehmer { get; set; }
        public string TeilnehmerName { get; set; }

        public ObservableCollection<Teilnehmer> TeilnehmerListe { get; set; }

        // für ausgewählten Teilnehmer
        private Teilnehmer _selectedTeilnehmer;
        public Teilnehmer SelectedTeilnehmer
        {
            get { return _selectedTeilnehmer; }
            set
            {
                _selectedTeilnehmer = value;
                OnPropertyChanged("SelectedTeilnehmer");
            }
        }

        // für Ausgewählte Aktivität
        private Aktivitaet _selectedAktivitaet;
        public Aktivitaet SelectedAktivitaet
        {
            get { return _selectedAktivitaet; }
            set
            {
                _selectedAktivitaet = value;
                OnPropertyChanged("SelectedAktivitaet");
                FillList();
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
        public TeilnehmerListeViewModel(Aktivitaet aktivitaet)
        {
            // Für Teilnehmerliste instanzieren
            TeilnehmerListe = new ObservableCollection<Teilnehmer>();
            SchliessenCmd = new WpfLibraryLT.RelayCommand(Schliessen);
            TeilnehmerLoeschenCmd = new WpfLibraryLT.RelayCommand(TeilnehmerLoeschen);

            // für ausgewählte aktivität
            SelectedAktivitaet = aktivitaet;
            // optional
            ExportListeCmd = new WpfLibraryLT.RelayCommand(ExportListe);
            // für Datenbank change
            //_useMSSQLSMVerbindung = useMSSQLSMVerbindung;

            //if (_useMSSQLSMVerbindung)
            //{
            //    using (CVAVTContext context = new CVAVTContext())
            //    {
            //        if (aktivitaet == null)
            //        {
            //            throw new ArgumentNullException(nameof(aktivitaet), "Aktivität darf nicht null sein");
            //        }
            //        else
            //        {
            //            // Die Daten aus aktivität werden in die Propeties geschrieben
            //            AktivitaetenMaxTeilnehmer = aktivitaet.AktivitaetenMaxTeilnehmer;
            //            AktivitaetenIstTeilnehmer = context.Teilnehmer.Count(t => t.AktivitaetIdfk == aktivitaet.AktivitaetenId);
            //        }
            //    }
            //}
            //else
            
                using SQLiteKontext context = new SQLiteKontext();
                {
                    if (aktivitaet == null)
                    {
                        throw new ArgumentNullException(nameof(aktivitaet), "Aktivität darf nicht null sein");
                    }
                    else
                    {
                        // Die Daten aus aktivität werden in die Propeties geschrieben
                        AktivitaetenMaxTeilnehmer = aktivitaet.AktivitaetenMaxTeilnehmer;
                        AktivitaetenIstTeilnehmer = context.Teilnehmer.Count(t => t.AktivitaetIdfk == aktivitaet.AktivitaetenId);
                    }
                }
            





            FillList();

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

        private void ExportListe()
        {
            // Nummer sicher dass eine Aktivität ausgewählt ist
            // falls Export Teilnehmer Liste irgendwie anders aufgerufen wurde
            if (SelectedAktivitaet == null)
            {
                MessageBox.Show("Es wurde keine Aktivität ausgewählt.", "Aktivität auswählen", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int selectedAktivitaetId = SelectedAktivitaet.AktivitaetenId;

            List<Teilnehmer> teilnehmerListe;
            //if (_useMSSQLSMVerbindung)
            //{
            //    using (CVAVTContext context = new CVAVTContext())
            //    {
            //        // Filtere die Teilnehmerliste nach der ausgewählten Aktivität
            //        teilnehmerListe = context.Teilnehmer.Where(t => t.AktivitaetIdfk == selectedAktivitaetId).ToList();

            //        // Ohne Filterung werden ALLE teilnehmer von ALLEN aktivitäten angezeigt
            //        //teilnehmerListe = context.Teilnehmer.ToList();
            //    }
            //}
            //else
            
                using (SQLiteKontext context = new SQLiteKontext())
                {
                    // Filtere die Teilnehmerliste nach der ausgewählten Aktivität
                    teilnehmerListe = context.Teilnehmer.Where(t => t.AktivitaetIdfk == selectedAktivitaetId).ToList();

                    // Ohne Filterung werden ALLE teilnehmer von ALLEN aktivitäten angezeigt
                    //teilnehmerListe = context.Teilnehmer.ToList();
                }
            
            // StringBuilder-Instanz zum Erstellen der CSV-Daten
            StringBuilder csvData = new StringBuilder();

            // Zuerst der Titel
            csvData.AppendLine($"Teilnehmer von: {SelectedAktivitaet.AktivitaetenName}");
            // Dann Header
            csvData.AppendLine("Teilnehmer Name");

            // test für Anzeige von Aktivität
            // braucht es so nicht
            //StringBuilder csvData2 = new StringBuilder();
            //csvData2.AppendLine($"{SelectedAktivitaet.AktivitaetenName}");

            // füge Teilnehmer zur Teilnehmer Liste Hinzu
            foreach (var teilnehmer in teilnehmerListe)
            {
                csvData.AppendLine($"{teilnehmer.TeilnehmerName}");
            }

            // Erstelle den SaveFileDialog
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV Dateien (*.csv)|*.csv",
                Title = "Teilnehmerliste exportieren",
                FileName = "TeilnehmerListe.csv" // Standardmäßiger Dateiname
            };

            // Dialog öffnen und überprüfen, ob der Benutzer "Speichern" ausgewählt hat
            if (saveFileDialog.ShowDialog() == true)
            {
                // Hole den ausgewählten Pfad und Dateinamen
                string exportPath = saveFileDialog.FileName;

                // Schreibe die CSV-Daten in die Datei
                //File.WriteAllText(exportPath, csvData.ToString());
                File.WriteAllText(exportPath, csvData.ToString(), Encoding.UTF8);

                // Gib eine Meldung aus, um den erfolgreichen Export anzuzeigen
                MessageBox.Show("Teilnehmerliste wurde erfolgreich als CSV exportiert.", "Export erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }

        private void Schliessen()
        {
            Verlassen();
        }

        private void TeilnehmerLoeschen()
        {
            MessageBoxResult result = MessageBox.Show("Teilnehmer Wirklich Löschen?", "Löschen Bestätigen", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //if (_useMSSQLSMVerbindung)
                //{
                //    using (CVAVTContext context = new CVAVTContext())
                //    {
                //        Teilnehmer teilN = context.Teilnehmer.Where(a => a.TeilnehmerId == SelectedTeilnehmer.TeilnehmerId).FirstOrDefault();
                //        if (teilN != null)
                //        {
                //            context.Teilnehmer.Remove(teilN);
                //            context.SaveChanges();
                //        }
                //    }
                //}
                //else
                

                    using (SQLiteKontext context = new SQLiteKontext())
                    {
                        Teilnehmer teilN = context.Teilnehmer.Where(a => a.TeilnehmerId == SelectedTeilnehmer.TeilnehmerId).FirstOrDefault();
                        if (teilN != null)
                        {
                            context.Teilnehmer.Remove(teilN);
                            context.SaveChanges();
                        }
                    }
                


                FillList();

            }
        }

        // Füllen der Teilnehmerliste
        private void FillList()
        {
            TeilnehmerListe.Clear();
            if (SelectedAktivitaet != null)
            {
                ////if (_useMSSQLSMVerbindung)
                ////{
                ////    using (CVAVTContext context = new CVAVTContext())
                ////    {
                ////        var teilNehmer = context.Teilnehmer
                ////            .Where(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId)
                ////            .ToList();

                ////        foreach (Teilnehmer member in teilNehmer)
                ////        {
                ////            TeilnehmerListe.Add(member);
                ////        }

                ////    }
                ////}
                ////else
                
                    using (SQLiteKontext context = new SQLiteKontext())
                    {
                        var teilNehmer = context.Teilnehmer
                            .Where(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId)
                            .ToList();
                        //var teilNehmer = context.Teilnehmer
                        //    .Where(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId)
                        //    .ToList();

                        foreach (Teilnehmer member in teilNehmer)
                        {
                            TeilnehmerListe.Add(member);
                        }

                    }
                
            }

            // Setzen Sie das erste Element der Liste als das ausgewählte Teilnehmerobjekt.
            SelectedTeilnehmer = TeilnehmerListe.FirstOrDefault();

        }

    }
}
