using System;

namespace PJT.Transactions
{
    /// <summary>
    /// Ошибка при выполнении операции
    /// </summary>
    public class TransactionOperationError : TransactionError
    {
        /// <summary>
        /// Результат операции
        /// </summary>
        public IOperationResult OperationResult { get; private set; }

        /// <summary>
        /// Новая ошибка при выполнении операции
        /// </summary>
        /// <param name="message">Сообщение ошибки</param>
        /// <param name="ex">Исключение</param>
        /// <param name="result">Результат операции</param>
        /// <param name="parameters">Параметры</param>
        public TransactionOperationError(string message, Exception ex, IOperationResult result, params ErrorParameter[] parameters) : base(message, ex, parameters)
        {
            OperationResult = result;
            InternalResult = result;

        }
    }
}