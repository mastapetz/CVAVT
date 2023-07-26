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
        // AktiviaetAnlegen
        public ICommand AktivitaetEditCmd { get; set; }

        // Anlegen Abbrechen
        public ICommand EditAbbrechenCmd { get; set; }

        // Event dient zur Entkopplung zwischen ViewModel und View haben
        public event EventHandler OnRequestCloseWin;

        // Aktivitaet  Properties
        public string AktivitaetenName { get; set; }
        public string AktivitaetenLeiter { get; set; }
        public string AktivitaetenArt { get; set; }
        public DateOnly AktivitaetenDatum { get; set; }
        public TimeOnly AktivitaetenZeit { get; set; }
        public double AktivitaetenDauer { get; set; }
        public int AktivitaetenMaxTeilnehmer { get; set; }
        public bool AktivitaetenVorwissenNoetig { get; set; }
        public string AktivitaetenInformation { get; set; }
        public ObservableCollection<Aktivitaet> AktivitaetenListe { get; set; }
        public ObservableCollection<Leiter> LeiterListe { get; set; }
        public Aktivitaet SelectedAktivitaet { get; set; }

        // Konstruktor
        public EditAktivitaetViewModel(Aktivitaet aktiv)
        {
            LeiterListe = new ObservableCollection<Leiter>();
            AktivitaetEditCmd = new WpfLibrary.RelayCommand(AktivitaetEdit);
            EditAbbrechenCmd = new WpfLibrary.RelayCommand(EditAbbrechen);

            // DB Verbindung zum füllen
            using (CVAVTContext context = new CVAVTContext())
            {
                var leiterListe = context.Leiter.OrderBy(l => l.LeiterName);
                foreach (var leiter in leiterListe)
                {
                    LeiterListe.Add(leiter);
                }
            }
            if (aktiv != null)
            {

            }
        }




        private void AktivitaetEdit()
        {
            throw new NotImplementedException();
        }

        private void EditAbbrechen()
        {
            throw new NotImplementedException();
        }

    }
}
