using System;
using System.Globalization;
using System.Windows.Data;

namespace RTCManifestGenerator.Converters
{
    public class VisibilityConverter : IValueConverter
    {
        public System.Windows.Visibility FalseValue { get; set; } = System.Windows.Visibility.Collapsed;

        public System.Windows.Visibility TrueValue { get; set; } = System.Windows.Visibility.Visible;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is bool))
                return FalseValue;

            return ((bool)value) ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null || !(value is System.Windows.Visibility))
                return false;

            return ((System.Windows.Visibility)value) == TrueValue;
        }
    }
}
