using System;
using System.Collections.Generic;

namespace PJT.Transactions
{
    /// <summary>
    /// Ошибка при транзакции
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class TransactionError : IOperationResult
    {
        readonly List<ErrorParameter> m_parameters = new List<ErrorParameter>();

        public string Message { get; private set; }

        public Exception Exception { get; private set; }

        public IReadOnlyList<ErrorParameter> Parameters => m_parameters;

        public bool HasException => Exception != null;

        public IOperationResult InternalResult { get; protected set; }

        public bool Success => false;

        public TransactionError(string message, Exception ex = null, params ErrorParameter[] parameters)
        {
            Message = message;
            Exception = ex;

            if (m_parameters != null && parameters.Length > 0)
                m_parameters.AddRange(parameters);
        }

        string DebugDisplay()
        {
            return Message.LogValue();
        }
    }
}