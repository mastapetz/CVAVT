using CVAVT.Models;
using CVAVT.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    class TeilnehmerListeViewModel : WpfLibrary.BaseViewModel
    {
        // Commands
        //public ICommand NeuTeilnehmerCmd { get; set; }
        public ICommand ExportListeCmd { get; set; }
        public ICommand SchliessenCmd { get; set; }
        public ICommand TeilnehmerLoeschenCmd { get; set; }
        public ICommand TeilnehmerEditCmd { get; set; }
        // Eventhandler
        public event EventHandler OnRequestCloseWindow;

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
        //public Teilnehmer SelectedTeilnehmer { get; set; }

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

        // Konstruktor
        public TeilnehmerListeViewModel(Aktivitaet aktivitaet)
        {
            // Für Teilnehmerliste instanzieren
            TeilnehmerListe = new ObservableCollection<Teilnehmer>();
            //NeuTeilnehmerCmd = new WpfLibrary.RelayCommand(NeuerTeilnehmerMenu);
            SchliessenCmd = new WpfLibrary.RelayCommand(Schliessen);
            TeilnehmerLoeschenCmd = new WpfLibrary.RelayCommand(TeilnehmerLoeschen);
            TeilnehmerEditCmd = new WpfLibrary.RelayCommand(TeilnehmerEdit);
            // für ausgewählte aktivität
            SelectedAktivitaet = aktivitaet;
            // optional
            ExportListeCmd = new WpfLibrary.RelayCommand(ExportListe);

            using (CVAVTContext context = new CVAVTContext())
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

        //private void NeuerTeilnehmerMenu()
        //{
        //    NeuerTeilnehmer window = new NeuerTeilnehmer(SelectedAktivitaet, null);
        //    window.ShowDialog();
        //}

        private void ExportListe()
        {
            throw new NotImplementedException();
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
                using (CVAVTContext context = new CVAVTContext())
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

        private void TeilnehmerEdit()
        {
            using (CVAVTContext context = new CVAVTContext())
            {
                Teilnehmer nameEdit = context.Teilnehmer.Where(n => n.TeilnehmerId == SelectedTeilnehmer.TeilnehmerId).FirstOrDefault();
                if (nameEdit != null)
                {
                    // Ändern des Namens
                    nameEdit.TeilnehmerName = SelectedTeilnehmer.TeilnehmerName;
                    context.SaveChanges();
                }
            }

        }

        // Füllen der Teilnehmerliste
        private void FillList()
        {
            TeilnehmerListe.Clear();
            if (SelectedAktivitaet != null)
            {
                using (CVAVTContext context = new CVAVTContext())
                {
                    var teilNehmer = context.Teilnehmer
                        .Where(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId)
                        .ToList();

                    foreach (Teilnehmer member in teilNehmer)
                    {
                        TeilnehmerListe.Add(member);
                    }

                }
            }

            // Setzen Sie das erste Element der Liste als das ausgewählte Teilnehmerobjekt.
            SelectedTeilnehmer = TeilnehmerListe.FirstOrDefault();

        }

        // Zum Updaten der Ist anzahl nach hinzufügen eines neuen Teilnehmers
        //public void UpdateTeilnehmerListe()
        //{
        //    using (CVAVTContext context = new CVAVTContext())
        //    {
        //        if (SelectedAktivitaet != null)
        //        {
        //            var teilnehmerDerAktivitaet = context.Teilnehmer
        //                .Where(t => t.AktivitaetIdfk == SelectedAktivitaet.AktivitaetenId)
        //                .ToList();

        //            TeilnehmerListe.Clear();

        //            foreach (Teilnehmer teilnehmer in teilnehmerDerAktivitaet)
        //            {
        //                TeilnehmerListe.Add(teilnehmer);
        //            }

        //            // Aktualisieren der Anzahl der Teilnehmer für die ausgewählte Aktivität
        //            AktivitaetenIstTeilnehmer = TeilnehmerListe.Count;
        //        }
        //    }
        //}

    }
}
