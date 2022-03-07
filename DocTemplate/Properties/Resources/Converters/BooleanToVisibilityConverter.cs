using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DocTemplate.Properties.Resources.Converters
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is bool isBusy)
                return isBusy ? Visibility.Visible : Visibility.Collapsed;
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Конвертирование возможно только в одну сторону");
        }
    }
}
