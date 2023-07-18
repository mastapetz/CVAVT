using System;
using System.Collections.Generic;
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

        // Properties
        public string LeiterName { get; set; }

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
