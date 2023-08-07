using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CVAVT.Models;
using CVAVT.ViewModels;
using CVAVT.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using CVAVT.Konverter;
using System.Globalization;

namespace CVAVT.ViewModels
{
    class MainWindowViewModel : WpfLibrary.BaseViewModel
    {
        // Neuer Teilnehmer
        public ICommand NeuTeilnehmerCmd { get; set; }

        // Neuer Leiter
        public ICommand NeuLeiterCmd { get; set; }

        // Neue Aktivität
        public ICommand NeuAktivitaetCmd { get; set; }

        // Edit Aktivität
        public ICommand EditAktivitaetCmd { get; set; }

        // Teilnehmer Liste Zeigen
        public ICommand ShowTeilnehmerCmd { get; set; }

        // Aktivität Löschen
        public ICommand AktivitaetLoeschenCmd { get; set; }

        // Program Beenden
        public ICommand BeendenCmd { get; set; }

        // Aktivitätenliste exportieren
        public ICommand ExportAktivitaetenListeCmd { get; set; }
        // Properties
        public ObservableCollection<Aktivitaet> AktivitaetenListe { get; set; }

        public Aktivitaet SelectedAktivitaet { get; set; }
        // -------------------------------------------------------
        // Zum Blättern
        // buttons fehlen noch
        //private int _position;
        //private const int Anzahl = 10;
        // ------------------------------------------
        // Event zum Schließen
        public event EventHandler OnRequestCloseWindow;
        // Aktivitaet  Properties
        public string AktivitaetenName { get; set; }
        public string AktivitaetenArt { get; set; }

        // Vergangene Einblenden
        private bool _vergangeneAnzeigen;
        public bool VergangeneAnzeigen
        {
            get { return _vergangeneAnzeigen; }
            set
            {
                _vergangeneAnzeigen = value;
                FillList();
                OnPropertyChanged("VergangeneAnzeigen");
            }
        }

        public int AktivitaetenIstTeilnehmer { get; set; }

        // Konstruktor
        public MainWindowViewModel()
        {
            AktivitaetenListe = new ObservableCollection<Aktivitaet>();
            NeuTeilnehmerCmd = new WpfLibrary.RelayCommand(NeuerTeilnehmerMenu);
            NeuLeiterCmd = new WpfLibrary.RelayCommand(NeuLeiter);
            NeuAktivitaetCmd = new WpfLibrary.RelayCommand(NeuAktivitaet);
            EditAktivitaetCmd = new WpfLibrary.RelayCommand(EditAktivitaet);
            ShowTeilnehmerCmd = new WpfLibrary.RelayCommand(ShowTeilnehmer);
            AktivitaetLoeschenCmd = new WpfLibrary.RelayCommand(AktivitaetLoeschen);
            BeendenCmd = new WpfLibrary.RelayCommand(Schliessen);
            ExportAktivitaetenListeCmd = new WpfLibrary.RelayCommand(ExportAktivitaetenListe);
            FillList();
        }



        private void Schliessen()
        {
            if (OnRequestCloseWindow != null)
                // Delegate wird aufgerufen
                OnRequestCloseWindow(this, new EventArgs());
        }



        private void AktivitaetLoeschen()
        {
            // Benutzer fragen ob wirklich gelöscht werden soll
            // Show() hat 3 Parameter
            // 1) Text der Messagebox
            // 2) Window Titel
            // 3) Definition der Buttons

            MessageBoxResult result = MessageBox.Show("Aktivität Wirklich Löschen?\nLöscht auch alle Teilnehmer", "Löschen Bestätigen", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                // Verknüpfte Teilnehmerdatensätze löschen
                using (CVAVTContext context = new CVAVTContext())
                {
                    var teilnehmer = context.Teilnehmer.Where(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId);
                    context.Teilnehmer.RemoveRange(teilnehmer);
                    context.SaveChanges();

                    // Datensatz löschen

                    // Suchvorgang in DB nach entsprechenden Eintrag
                    Aktivitaet aktiv = context.Aktivitaet.Where(a => a.AktivitaetenId == SelectedAktivitaet.AktivitaetenId).FirstOrDefault();
                    if (aktiv != null)
                    {
                        context.Aktivitaet.Remove(aktiv);
                        context.SaveChanges();
                    }
                }
                // Neu Laden der Liste
                FillList();

            }
        }

        private void ShowTeilnehmer()
        {
            TeilnehmerListe window = new TeilnehmerListe(SelectedAktivitaet);
            window.Show();
            FillList();
        }

        private void EditAktivitaet()
        {

            EditActivityWindow window = new EditActivityWindow(SelectedAktivitaet);
            window.ShowDialog();
            FillList();
        }



        private void NeuerTeilnehmerMenu()
        {
            NeuerTeilnehmer window = new NeuerTeilnehmer(SelectedAktivitaet, null);
            window.ShowDialog();
            FillList();

        }

        private void NeuLeiter()
        {
            NeuerLeiter window = new NeuerLeiter();
            window.ShowDialog();
        }

        private void NeuAktivitaet()
        {
            NeueAktivitaet window = new NeueAktivitaet(null);
            window.ShowDialog();
            FillList();

        }
        // ========================================================
        private void ExportAktivitaetenListe()
        {
            // Daten aus der Datenbank abrufen
            // Für Aktivitäten
            List<Aktivitaet> aktivitaeten;
            // Für Leiter
            List<Leiter> leiter;
            using (CVAVTContext context = new CVAVTContext())
            {
                aktivitaeten = context.Aktivitaet.ToList();
                leiter = context.Leiter.ToList();
            }

            // Konverter für Datum und Uhrzeit erstellen
            DateFormatConverter dateFormatConverter = new DateFormatConverter();
            TimeFormatConverter timeFormatConverter = new TimeFormatConverter();


            // StringBuilder-Instanz zum Erstellen der CSV-Daten
            StringBuilder csvData = new StringBuilder();

            // Füge die CSV-Header-Zeile hinzu (hier kannst du die gewünschten Spaltenüberschriften definieren)
            csvData.AppendLine("AktivitaetenName;AktivitaetenArt;LeiterName;AktivitaetenIstTeilnehmer;AktibitaetenMaxTeilnehmer;AktivitaetenDatum;AktivitaetenZeit");
            // Ohne Zeit und Datum
            //csvData.AppendLine("AktivitaetenName;AktivitaetenArt;LeiterName;AktivitaetenIstTeilnehmer;AktibitaetenMaxTeilnehmer");

            // Füge die Daten der Aktivitätenliste hinzu
            foreach (var aktivitaet in aktivitaeten)
            {
                // Finde den Leiter für die aktuelle Aktivität
                string leiterName = leiter.FirstOrDefault(l => l.LeiterId == aktivitaet.LeiterIdfk)?.LeiterName ?? "N/A";

                // Verwende die Konverter, um das Datum und die Uhrzeit in das gewünschte Format zu konvertieren
                string formattedDate = (string)dateFormatConverter.Convert(aktivitaet.AktivitaetenDatum, null, null, CultureInfo.InvariantCulture);
                string formattedTime = (string)timeFormatConverter.Convert(aktivitaet.AktivitaetenZeit, null, null, CultureInfo.InvariantCulture);

                csvData.AppendLine($"{aktivitaet.AktivitaetenName};{aktivitaet.AktivitaetenArt};{leiterName};{aktivitaet.AktivitaetenIstTeilnehmer};{aktivitaet.AktivitaetenMaxTeilnehmer};{formattedDate};{formattedTime}");

                // Ohne Zeit und Datum
                //csvData.AppendLine($"{aktivitaet.AktivitaetenName};{aktivitaet.AktivitaetenArt};{aktivitaet.LeiterIdfkNavigation?.LeiterName};{aktivitaet.AktivitaetenIstTeilnehmer};{aktivitaet.AktivitaetenMaxTeilnehmer}");
            }

            // Erstelle den SaveFileDialog
            var saveFileDialog = new SaveFileDialog
            {
                Filter = "CSV Dateien (*.csv)|*.csv",
                Title = "Aktivitätenliste exportieren",
                FileName = "AktivitaetenListe.csv" // Standardmäßiger Dateiname
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
                MessageBox.Show("Aktivitätenliste wurde erfolgreich als CSV exportiert.", "Export erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            /*
             *  ohne Speicherort auswahl
             * // Pfade und Dateinamen für den Export
             * string exportFileName = "AktivitaetenListe.csv";
             * string exportPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), exportFileName);
             * // Schreibe die CSV-Daten in die Datei
             * File.WriteAllText(exportPath, csvData.ToString());
             * // Gib eine Meldung aus, um den erfolgreichen Export anzuzeigen
             * MessageBox.Show("Aktivitätenliste wurde erfolgreich als CSV exportiert.", "Export erfolgreich", MessageBoxButton.OK, MessageBoxImage.Information);
             */
        }



        // ------------------------------------------------------

        private void FillList()
        {
            AktivitaetenListe.Clear();
            // DB zugriff
            using (CVAVTContext context = new CVAVTContext())
            {
                // Für Filterung vergangener Aktivitäten
                DateTime heute = DateTime.Today;

                // Zeige alle Aktivitäten, wenn VergangeneAnzeigen auf true gesetzt ist
                // Ansonsten zeige nur zukünftige Aktivitäten ab heute
                var aktivitaeten = context.Aktivitaet.Include(a => a.LeiterIdfkNavigation)
                    .Where(p => AktivitaetenName.IsNullOrEmpty() ? true : p.AktivitaetenName.StartsWith(AktivitaetenName))
                    .Where(p => AktivitaetenArt.IsNullOrEmpty() ? true : p.AktivitaetenArt.StartsWith(AktivitaetenArt));
                // --------------------------- für Blättern,
                // noch nicht implementiert
                //.Skip(_position).Take(Anzahl);

                if (!VergangeneAnzeigen)
                {
                    // Zeige nur zukünftige Aktivitäten ab heute
                    aktivitaeten = aktivitaeten.Where(p => p.AktivitaetenDatum >= heute);
                }


                foreach (Aktivitaet aktivity in aktivitaeten)
                {
                    AktivitaetenListe.Add(aktivity);
                }
            }
            // Aktualisiere die Anzahl der Ist-Teilnehmer für die ausgewählte Aktivität
            if (SelectedAktivitaet != null)
            {
                AktivitaetenIstTeilnehmer = SelectedAktivitaet.AktivitaetenIstTeilnehmer;
            }



        }

    }
}
