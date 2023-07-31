using CVAVT.Models;
using CVAVT.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    class TeilnehmerListeViewModel : WpfLibrary.BaseViewModel
    {
        // Commands
        public ICommand NeuTeilnehmerCmd { get; set; }
        public ICommand ExportListeCmd { get; set; }
        public ICommand SchliessenCmd { get; set; }
        public ICommand TeilnehmerLoeschenCmd { get; set; }
        public ICommand TeilnehmerEditCmd { get; set; }

        // Properties
        public int TeilnehmerIst { get; set; }
        public int TeilnehmerMax { get; set; }
        public string TeilnehmerName { get; set; }

        public event EventHandler OnRequestCloseWindow;
        public Aktivitaet SelectedAktivitaet { get; set; }

        // Konstruktor

        public TeilnehmerListeViewModel()
        {
            NeuTeilnehmerCmd = new WpfLibrary.RelayCommand(NeuerTeilnehmerMenu);
            ExportListeCmd = new WpfLibrary.RelayCommand(ExportListe);
            SchliessenCmd = new WpfLibrary.RelayCommand(Schliessen);
            TeilnehmerLoeschenCmd = new WpfLibrary.RelayCommand(TeilnehmerLoeschen);
            TeilnehmerEditCmd = new WpfLibrary.RelayCommand(TeilnehmerEdit);
        }

        private void NeuerTeilnehmerMenu()
        {
            NeuerTeilnehmer window = new NeuerTeilnehmer(SelectedAktivitaet, null);
            window.ShowDialog();
        }

        private void ExportListe()
        {
            throw new NotImplementedException();
        }

        private void Schliessen()
        {
            throw new NotImplementedException();
        }

        private void TeilnehmerLoeschen()
        {
            throw new NotImplementedException();
        }

        private void TeilnehmerEdit()
        {
            throw new NotImplementedException();
        }
    }
}
