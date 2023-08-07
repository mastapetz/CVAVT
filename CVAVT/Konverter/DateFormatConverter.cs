using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CVAVT.Konverter
{
    internal class DateFormatConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                // Gibt Datum aus im Format (Beispiel): 01.01.2023; Tag.Monat.Jahr
                // CultureInfo.InvariantCulture stellt sicher das Kulturelle Unterschiede bei der Formatierung keine ROlle Spielen
                return dateTime.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture);
            }
            return string.Empty;
        }

        // Wird benötigt für  " : IValueConverter "
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString)
            {
                if (DateTime.TryParseExact(dateString, "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
                {
                    return dateTime;
                }
            }
            return DependencyProperty.UnsetValue; // oder eine andere Rückgabewert, wenn die Konvertierung fehlschlägt
        }

    }
}
