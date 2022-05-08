using System;
using System.Globalization;
using System.Windows.Data;

namespace DocTemplate.Properties.Resources.Converters
{
    public class RadioButtonToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;
            return name == parameter.ToString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
