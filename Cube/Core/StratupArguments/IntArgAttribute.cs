using System;
using System.Reflection;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// Аргумент типа Int
    /// </summary>
    public class IntArgAttribute : StartupArgValueAttribute
    {
        public IntArgAttribute(string name, int defVal = 0, char separator = DefaultSplitChar, string descr = null, string example = null) 
            : base(name, separator, descr, example)
        {
            Value = defVal;
        }

        protected override string GetStringValue<TInstance>(PropertyInfo property, TInstance instance)
        {
            var val = property.GetValue(instance);

            if (val == null)
                return "0";

            return val.ToString();
        }

        /// <summary>
        /// Парсинг входящего аргумента
        /// </summary>
        /// <param name="value">Значение входного аргумента</param>
        /// <returns>Возвращает распарсенное значение или 0</returns>
        protected override object ParseValue(string value)
        {
            if (value == null)
                return 0;

            int val = 0;
            int.TryParse(value, out val);

            return val;
        }
    }
}
