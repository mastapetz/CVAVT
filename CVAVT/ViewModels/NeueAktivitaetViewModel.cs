using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CVAVT.ViewModels
{
    internal class NeueAktivitaetViewModel : WpfLibrary.BaseViewModel
    {
        // AktiviaetAnlegen
        public ICommand AktivitaetAnlegenCmd { get; set; }
        public ICommand AnlegenAbbrechenCmd { get; set; }

        // Konstruktor
        public NeueAktivitaetViewModel()
        {
            AktivitaetAnlegenCmd = new WpfLibrary.RelayCommand(AktivitaetAnlegen);
            AnlegenAbbrechenCmd = new WpfLibrary.RelayCommand(AnlegenAbbrechen);
        }

        private void AktivitaetAnlegen()
        {
            throw new NotImplementedException();
        }

        private void AnlegenAbbrechen()
        {
            throw new NotImplementedException();
        }
    }
}
