using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace DocTemplate.Properties.Resources.Converters
{
   
    public sealed class BooleanToVisibility : BooleanConverter<Visibility>
    {
        public BooleanToVisibility() :
            base(Visibility.Visible, Visibility.Collapsed)
        { }
    }
    
    public class BooleanConverter<T> : IValueConverter
    {
        public BooleanConverter(T trueValue, T falseValue)
        {
            True = trueValue;
            False = falseValue;
        }

        public T True { get; set; }
        public T False { get; set; }

        /// <summary>
        /// Конвертация
        /// </summary>
        /// <param name="value">Значение для конвертации</param>
        /// <param name="targetType">Конечный тип</param>
        /// <param name="parameter">Параметры для конвертации</param>
        /// <param name="culture">Культура программы</param>
        /// <returns>Конвертированное значение</returns>
        public virtual object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is bool && ((bool)value) ? True : False;
        }
        /// <summary>
        /// Обратная конвертация
        /// </summary>
        /// <param name="value">Значение для конвертации</param>
        /// <param name="targetType">Конечный тип</param>
        /// <param name="parameter">Параметры для конвертации</param>
        /// <param name="culture">Культура программы</param>
        /// <returns>Изначальное значение</returns>
        public virtual object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value is T && EqualityComparer<T>.Default.Equals((T)value, True);
        }
    }
}
