using System;
using System.Reflection;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// Входной аттрибут приложения с несоклькими значениями
    /// </summary>
    public abstract class StartupArgMultiValueAttribute : StartupArgValueAttribute
    {
        protected readonly char m_valueSplitter;

        /// <summary>
        /// Входной аттрибут типа ключ/набор значений
        /// </summary>
        /// <param name="name">Имя входного аттрибута</param>
        /// <param name="splitChar">Разделитель имени/набора значений</param>
        /// <param name="valueSplitChar">Разделитель значений</param>
        public StartupArgMultiValueAttribute(string name, char splitChar = '=', char valueSplitChar = ',', string descr = null, string example = null) 
            : base(name, splitChar, descr, example)
        {
            m_valueSplitter = valueSplitChar;
        }

        protected override object ParseValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return new object[0];

            return value.Split(m_valueSplitter);
        }

        protected override string GetStringValue<TInstance>(PropertyInfo property, TInstance instance)
        {
            var val = property.GetValue(instance);

            var strArr = val as string[];

            if (strArr == null || strArr.Length == 0)
                return string.Empty;

            return string.Join(m_valueSplitter.ToString(), strArr);
        }
    }
}
