using System;
using System.Text;

namespace Cube.Log
{
    /// <summary>
    /// Строковый параметр логирования
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class StringLogParameter : IStringLogParameter
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Значение параметра
        /// </summary>
        public string Value { get; private set; }
        /// <summary>
        /// СТроковый параметр логирования
        /// </summary>
        /// <param name="name">Имя параметра</param>
        /// <param name="value">Значение параметра</param>
        public StringLogParameter(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public void Append(StringBuilder sb, string indent)
        {
            sb.Append(Name).Append(": '").Append(Value.LogValue()).AppendLine("'");
        }

        string DebugDisplay() => $"'{Name.LogValue()}': '{Value.LogValue()}'";
    }
}
