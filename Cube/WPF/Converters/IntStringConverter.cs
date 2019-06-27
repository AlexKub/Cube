using System;
using System.Globalization;
using System.Windows.Data;

namespace Merlion.WPF.Core.Converters
{
    /// <summary>
    /// Конвертер из Int в String
    /// </summary>
    public class IntStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int)
                return ((int)value).ToString();

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                return int.Parse((string)value);
            }

            throw new InvalidCastException($"Переданный тип '{(value == null ? "null" : value.GetType().FullName) }' не является строкой");
        }
    }
}
