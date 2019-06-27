using System.Text;

namespace PJT.Transactions
{
    /// <summary>
    /// Параметр ошибки
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class ErrorParameter : Log.ILogParameter
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Значение параметра
        /// </summary>
        public string Value { get; private set; }

        public ErrorParameter(string name, string value)
        {
            Name = name;
            Value = GetString(value);
        }

        string GetString(string value)
        {
            return value == null ? "NULL" : string.IsNullOrEmpty(value) ? "EMPTY" : value;
        }

        string DebugDisplay()
        {
            return ((string.IsNullOrEmpty(Name) ? "NO_NAME" : Name)
                + " : "
                + ((Value == null) ? "NULL_VAL" : Value));
        }

        public StringBuilder AppendParameter(StringBuilder sb, string indent = "")
        {
            return sb.Append(indent).Append(Name).Append(": ").AppendLine(Value);
        }
    }
}