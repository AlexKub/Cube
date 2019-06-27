using PJT.Transactions;
using System;
using System.Collections.Generic;

namespace PJT
{
    /// <summary>
    /// Информация о результате выполненной операции
    /// </summary>
    public interface IOperationResult
    {
        /// <summary>
        /// Сообщение-описание результата
        /// </summary>
        string Message { get; }
        /// <summary>
        /// Исключение при выполнении (если есть)
        /// </summary>
        Exception Exception { get; }
        /// <summary>
        /// Флаг наличия исключения
        /// </summary>
        bool HasException { get; }
        /// <summary>
        /// Флаг успешного выполнения операции
        /// </summary>
        bool Success { get; }
        /// <summary>
        /// Результат вложенной операции
        /// </summary>
        IOperationResult InternalResult { get; }

        IReadOnlyList<ErrorParameter> Parameters { get; }
    }
}
