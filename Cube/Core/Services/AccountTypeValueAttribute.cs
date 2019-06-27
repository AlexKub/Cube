using System;

namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Реальные значения Типа авторизации
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class AccountTypeValueAttribute : Attribute
    {
        /// <summary>
        /// Реальные значения WMI
        /// </summary>
        public string WMI_Value { get; private set; }

        public AccountTypeValueAttribute(string value)
        {
            WMI_Value = value;
        }
    }
}
