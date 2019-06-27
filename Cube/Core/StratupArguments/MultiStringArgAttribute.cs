using System.Reflection;

namespace Merlion.ECR.Update.Core.StartupArguments
{
    /// <summary>
    /// АРгумент с набором строковых значений
    /// </summary>
    public class MultiStringArgAttribute : StartupArgMultiValueAttribute
    {
        /// <summary>
        /// Строковые значения входного аргумента
        /// </summary>
        public string[] Values { get { return Value as string[]; } }

        public MultiStringArgAttribute(string name, char splitChar = '=', char valueSplitChar = ',', string descr = null, string example = null) : base(name, splitChar, valueSplitChar, descr, example) { }

        protected override string GetStringValue<TInstance>(PropertyInfo property, TInstance instance)
        {
            var vals = property.GetValue(instance) as string[];

            if (vals == null || vals.Length == 0)
                return string.Empty;

            return string.Join(m_valueSplitter.ToString(), vals);
        }

        protected override bool PropertyHasValue<TInstance>(PropertyInfo property, TInstance instance)
        {
            var val = property.GetValue(instance);

            var strArr = val as string[];

            if (strArr == null)
                return false;

            return strArr.Length > 0;
        }
    }
}
