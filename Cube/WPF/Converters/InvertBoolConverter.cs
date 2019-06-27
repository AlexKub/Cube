//using Merlion.ECR.Update.Core.Log;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Merlion.WPF.Core.Converters
{
    /// <summary>
    /// Инвертирование boolean значнеия
    /// </summary>
    public class InvertBoolConverter : IValueConverter
    {
        //static LogSet m_loger = StaticMethods.GetFileLogSet<InvertBoolConverter>();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return !(bool)value;
            }
            catch (Exception ex)
            {
                //m_loger.Log("Возникло исключение при инвертировании bool в конвертере InvertBoolConverter", ex
                //    , new LogParameter("Тип переданного значения", value == null ? "NULL" : value.GetType().FullName));

                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                return !(bool)value;
            }
            catch (Exception ex)
            {
                //m_loger.Log("Возникло исключение при инвертировании bool обратно в конвертере InvertBoolConverter", ex
                //    , new LogParameter("Тип переданного значения", value == null ? "NULL" : value.GetType().FullName));

                return false;
            }
        }
    }
}
