using System;
using System.Globalization;
using System.Windows.Data;

namespace DocTemplate.Properties.Resources.Converters
{
    public class StringToBoolean : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (string)value == (string)parameter;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
