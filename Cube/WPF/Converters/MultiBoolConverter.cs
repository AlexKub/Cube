using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace RTCManifestGenerator.Converters
{
    public class MultiBoolConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                foreach (var value in values.Cast<bool>())
                    if (!value)
                        return false;

                return true;
            }
            catch
            {
                return false;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
