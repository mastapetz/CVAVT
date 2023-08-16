using CVAVT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CVAVT.HilfsKlassen
{
    public static class PruefHelfer
    {
        public static bool FelderGueltig(string aktivitaetenName, Leiter selectedLeiter, string aktivitaetenArt, double aktivitaetenDauer, string aktivitaetenInformation)
        {
            if (string.IsNullOrEmpty(aktivitaetenName))
            {
                MessageBox.Show("Das Feld 'Aktivitätsname' muss ausgefüllt sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (selectedLeiter == null || selectedLeiter.LeiterId < 0)
            {
                MessageBox.Show("Bitte einen Leiter auswählen.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (string.IsNullOrEmpty(aktivitaetenArt))
            {
                MessageBox.Show("Das Feld 'Aktivitäten Art' muss ausgefüllt sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (double.IsNaN(aktivitaetenDauer) || double.IsInfinity(aktivitaetenDauer) || aktivitaetenDauer <= 0)
            {
                MessageBox.Show("Das Feld 'Dauer' muss ausgefüllt sein und eine gültige Zahl größer als 0 sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(aktivitaetenInformation))
            {
                MessageBox.Show("Das Feld 'Aktivitäten Information' muss ausgefüllt sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        public static bool FelderGuelting(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Das Feld 'Name' muss ausgefüllt sein.", "Fehler", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }
    }


}
