﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace CVAVT.Konverter
{
    internal class TimeFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime)
            {
                return dateTime.ToString("HH:mm");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string timeString)
            {
                if (DateTime.TryParseExact(timeString, "HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
                {
                    return time;
                }
            }
            return DependencyProperty.UnsetValue; // oder eine andere Rückgabewert, wenn die Konvertierung fehlschlägt
        }
    }
}
