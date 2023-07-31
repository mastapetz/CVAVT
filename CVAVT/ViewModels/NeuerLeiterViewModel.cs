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
    internal class NeuerLeiterViewModel : WpfLibrary.BaseViewModel
    {
        // Commands
        public ICommand EingabeVerwerfenCmd { get; set; }
        public ICommand EingabeSpeichernCmd { get; set; }

        // Event zum Schließen
        public event EventHandler OnRequestCloseWindow;
        // Properties
        //public string LeiterName { get; set; }
        public ObservableCollection<Leiter> LeiterListe { get; set; }

        // Konstruktor
        public NeuerLeiterViewModel()
        {

            EingabeVerwerfenCmd = new WpfLibrary.RelayCommand(EingabeVerwerfen);
            EingabeSpeichernCmd = new WpfLibrary.RelayCommand(EingabeSpeichern);
        }

        private void EingabeVerwerfen()
        {
            throw new NotImplementedException();
        }



        private void EingabeSpeichern()
        {
            throw new NotImplementedException();
        }

    }
}
