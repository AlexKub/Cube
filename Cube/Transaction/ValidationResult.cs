using System;
using System.Collections.Generic;

namespace PJT.Transactions
{
    /// <summary>
    /// Результат проверки на валидность
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class ValidationResult : IOperationResult
    {
        /// <summary>
        /// Результат проверки
        /// </summary>
        public bool Valid { get; private set; }

        /// <summary>
        /// Описание результата првоерки
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Исключение при проверке
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Флаг наличия исключения
        /// </summary>
        public bool HasException { get { return Exception != null; } }

        /// <summary>
        /// Флаг Valid
        /// </summary>
        public bool Success => Valid;
        /// <summary>
        /// Вложенная операция (всегда null)
        /// </summary>
        public IOperationResult InternalResult => null;

        public IReadOnlyList<ErrorParameter> Parameters => null;

        /// <summary>
        /// Результат проверки на валидность
        /// </summary>
        /// <param name="valid">Флаг результата</param>
        /// <param name="description">Описание результата</param>
        public ValidationResult(bool valid, string description = null)
        {
            Valid = valid;
            const string defaultDescription = "NO DESCRIPTION";

            Message = string.IsNullOrWhiteSpace(description) 
                ? (valid ? "Валидация пройдена" : defaultDescription)
                : defaultDescription;
        }
        /// <summary>
        /// Результат проверки на валидность
        /// </summary>
        /// <param name="description">Описание ошибки</param>
        /// <param name="ex">Возникшее исключение</param>
        public ValidationResult(string description, Exception ex)
        {
            Valid = false;
            Message = description;
            Exception = ex;
        }

        string DebugDisplay()
        {
            return "Result: " 
                + (Valid 
                    ? "true" 
                    : ("false" + " | Description: " + (string.IsNullOrWhiteSpace(Message) ? "NO MESSAGE" : Message)) );
        }
    }
}