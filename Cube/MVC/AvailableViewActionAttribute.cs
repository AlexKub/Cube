using System;

namespace LogOperators.Models.MVC
{
    /// <summary>
    /// Аттрибут для методов контролллера без параметров, что возвращают View
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class AvailableViewActionAttribute : Attribute
    {
        /// <summary>
        /// Отображаемое имя на панели действий
        /// </summary>
        public string DisplayName { get; private set; }
        /// <summary>
        /// Метод контроллера по умолчанию
        /// </summary>
        public bool IsDefault { get; private set; }
        /// <summary>
        /// Аттрибут для методов контролллера без параметров, что возвращают View
        /// </summary>
        /// <param name="displayName">Отображаемое имя на панели действий</param>
        /// <param name="isDefault">Флаг метода по умолчанию</param>
        public AvailableViewActionAttribute(string displayName, bool isDefault = false)
        {
            DisplayName = displayName;
            IsDefault = isDefault;
        }

        string DebugDisplay() => (string.IsNullOrWhiteSpace(DisplayName) ? "NO_NAME" : DisplayName) + (IsDefault ? " | Default" : "");
    }
}