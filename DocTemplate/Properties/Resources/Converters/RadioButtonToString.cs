using System;
using System.Globalization;
using System.Windows.Data;

namespace DocTemplate.Properties.Resources.Converters
{
    public class RadioButtonToString : IValueConverter
    {
        /// <summary>
        /// Конвертация
        /// </summary>
        /// <param name="value">Значение для конвертации</param>
        /// <param name="targetType">Конечный тип</param>
        /// <param name="parameter">Параметры для конвертации</param>
        /// <param name="culture">Культура программы</param>
        /// <returns>Конвертированное значение</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string name = (string)value;
            return name == parameter.ToString();
        }
        /// <summary>
        /// Обратная конвертация
        /// </summary>
        /// <param name="value">Значение для конвертации</param>
        /// <param name="targetType">Конечный тип</param>
        /// <param name="parameter">Параметры для конвертации</param>
        /// <param name="culture">Культура программы</param>
        /// <returns>Изначальное значение</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return parameter;
        }
    }
}
