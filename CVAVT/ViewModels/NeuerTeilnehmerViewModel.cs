using System;
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
        public ICommand EditVerwerfenCmd { get; set; }
        public ICommand NaechsterTeilnehmerCmd { get; set; }
        public ICommand EditSpeichernCmd { get; set; }

        // Properties
        public string AktivitaetenName { get; set; }
        public string TeilnehmerName { get; set; }
        public int TeilnehmerIst { get; }
        public int TeilnehmerMax { get; }

        // Konstruktor
        public NeuerTeilnehmerViewModel()
        {
            EditVerwerfenCmd = new WpfLibrary.RelayCommand(EditVerwerfen);
            NaechsterTeilnehmerCmd = new WpfLibrary.RelayCommand(NaechsterTeilnehmer);
            EditSpeichernCmd = new WpfLibrary.RelayCommand(EditSpeichern);
        }

        private void EditVerwerfen()
        {
            throw new NotImplementedException();
        }

        private void NaechsterTeilnehmer()
        {
            throw new NotImplementedException();
        }

        private void EditSpeichern()
        {
            throw new NotImplementedException();
        }
    }
}
