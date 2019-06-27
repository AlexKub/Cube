using PJT.Models;
using PJT.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PJT
{
    /// <summary>
    /// Результат операции
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class OperationResult : IOperationResult
    {
        List<OperationResult> m_subResults;

        /// <summary>
        /// Имя операции
        /// </summary>
        public string Name { get; private set; }
        /// <summary>
        /// Флаг успеха
        /// </summary>
        public bool Success { get; private set; }
        /// <summary>
        /// Исключение при запросе
        /// </summary>
        public Exception Exception { get; private set; }
        /// <summary>
        /// Местное описание исключения
        /// </summary>
        public string Message { get; private set; }
        /// <summary>
        /// Проверка наличия исключения в ответе
        /// </summary>
        public bool HasException { get { return Exception != null; } }
        /// <summary>
        /// Вложенная операция
        /// </summary>
        public IOperationResult InternalResult => m_subResults?.FirstOrDefault();
        /// <summary>
        /// Результаты вложенных операций
        /// </summary>
        public IReadOnlyList<OperationResult> SubOperationsResults => m_subResults ?? new List<OperationResult>();
        /// <summary>
        /// Метка операции
        /// </summary>
        public OperationResultMark ResultMark { get; private set; }
        /// <summary>
        /// Флаг наличия вложенных результатов
        /// </summary>
        public bool HasSubErrors => m_subResults != null && m_subResults.Any(r => !r.Success);

        public IReadOnlyList<ErrorParameter> Parameters { get; private set; }

        #region конструкторы

        /// <summary>
        /// Пустой конструктор для корректного результата
        /// </summary>
        protected OperationResult()
        {
            Success = true;
            ResultMark = OperationResultMark.Success;
        }

        public OperationResult(string name, string message, bool success = true, params ErrorParameter[] parameters)
        {
            Name = name;
            Message = message;
            Success = success;
            Parameters = parameters;
            ResultMark = success ? OperationResultMark.Success : OperationResultMark.Unexpected;
        }

        public OperationResult(string name, string message, Exception ex, params ErrorParameter[] parameters) : this(name, message, false, parameters)
        {
            Exception = ex;
            ResultMark = OperationResultMark.Error;
        }
        public OperationResult(string name, string message, OperationResult nestedOp, bool success, params ErrorParameter[] parameters) : this(name, message, success, parameters)
        {
            m_subResults = new List<OperationResult>() { nestedOp };
            ResultMark = OperationResultMark.Nested;
        }

        #endregion

        string DebugDisplay()
        {
            return Success
                ? $"Success: {(string.IsNullOrEmpty(Message) ? "НЕТ ОПИСАНИЯ" : Message)}"
                : $"Error: {(string.IsNullOrEmpty(Message) ? "НЕТ ОПИСАНИЯ" : Message)}{(HasException ? $" | ExMessage: {Exception.Message}" : string.Empty)}";
        }

        public void AddSubResult(OperationResult subRes)
        {
            if (subRes != null)
            {
                if (m_subResults == null)
                    m_subResults = new List<OperationResult>();

                m_subResults.Add(subRes);
            }
        }
    }

    /// <summary>
    /// Результат операции
    /// </summary>
    /// <typeparam name="TValue">Тип полученного значения</typeparam>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class OperationResult<TResult> : OperationResult
    {
        /// <summary>
        /// Результат операции
        /// </summary>
        public TResult Result { get; private set; }

        #region конструкторы

        /// <summary>
        /// Успешный результат
        /// </summary>
        /// <param name="result">Результат выполнения</param>
        public OperationResult(TResult result) : base()
        {
            Result = result;
        }

        public OperationResult(string name, string message, TResult result, bool success = true, params ErrorParameter[] parameters)
            : base(name, message, success)
        {
            Result = result;
        }

        public OperationResult(string name, string message, Exception ex, params ErrorParameter[] parameters)
            : base(name, message, ex, parameters) { }

        public OperationResult(string name, string message, TResult result, OperationResult nestedOp, bool success = true, params ErrorParameter[] parameters)
            : base(name, message, nestedOp, success)
        {
            Result = result;
        }

        #endregion

        string DebugDisplay()
        {
            return (Success ? "Success" : "Error") + " | " +
                (Success
                    ? (Result == null ? "NULL" : Result.ToString())
                    : (string.IsNullOrEmpty(Message)
                        ? (Exception == null ? "NULL" : Exception.Message)
                        : Message));
        }
    }
}