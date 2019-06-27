using System;
using System.Reflection;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// Строковое значение
    /// </summary>
    public class StringArgAttribute : StartupArgValueAttribute
    {
        public StringArgAttribute(string name, string defaultValue = "", char splitChar = DefaultSplitChar, string descr = null, string example = null) : base(name, splitChar, descr, example)
        {
            Value = defaultValue;
        }

        protected override string GetStringValue<TInstance>(PropertyInfo property, TInstance instance)
        {
            var val = property.GetValue(instance);

            return val == null ? "" : val.ToString();
        }

        protected override bool PropertyHasValue<TInstance>(PropertyInfo property, TInstance instance)
        {
            var val = property.GetValue(instance);

            var str = val as string;

            if (str == null)
                return false;

            return (!string.IsNullOrEmpty(str));
        }

        protected override object ParseValue(string value)
        {
            return value;
        }
    }
}
