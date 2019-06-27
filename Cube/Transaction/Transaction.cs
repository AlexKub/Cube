using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PJT.Transactions
{
    /// <summary>
    /// Транзакция
    /// </summary>
    public abstract class Transaction : IDisposable
    {
        /// <summary>
        /// Добавление новой ошибки транзакции
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="ex">Исключение</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public abstract void AddError(string message, Exception ex = null, params ErrorParameter[] parameters);
        /// <summary>
        /// Добавление новой ошибки при выполнении операции
        /// </summary>
        /// <param name="result">Результат операции</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public abstract void AddError(OperationResult result, params ErrorParameter[] parameters);
        /// <summary>
        /// Добавление новой ошибки при выполнении операции
        /// </summary>
        /// <param name="result">Результат операции</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public abstract void AddError(Models.JIRA.JiraResponse result, params ErrorParameter[] parameters);

        public abstract void Dispose();
    }


    /// <summary>
    /// Базовый тип Транзакции
    /// </summary>
    public abstract class Transaction<TData> : Transaction
    {
        /// <summary>
        /// Выполнение транзакции
        /// </summary>
        public abstract Task<TData> Invoke();
    }

    /// <summary>
    /// Транзакция
    /// </summary>
    public abstract class ContextedTransaction<TContext, TData> : Transaction<TData>, IDisposable
        where TContext : TransactionContext
    {
        TContext m_context;

        /// <summary>
        /// Список ошибок транзакции
        /// </summary>
        public IReadOnlyList<TransactionError> Errors { get { return m_context?.Errors ?? new List<TransactionError>(); } }

        /// <summary>
        /// Флаг наличия ошибок транзакции
        /// </summary>
        public bool HasErrors { get { return Errors.Count > 0; } }

        /// <summary>
        /// Флаг корректной инициализации транзакции
        /// </summary>
        protected bool Initialized { get; set; }

        /// <summary>
        /// Контекст транзакции
        /// </summary>
        public TContext Context { get { return m_context; } }

        /// <summary>
        /// Транзакция
        /// </summary>
        public ContextedTransaction()
        {
            try
            {
                m_context = (TContext)Activator.CreateInstance(typeof(TContext), this);

                Initialized = m_context.Initialized;
            }
            catch //(Exception ex)
            {
                //TODO 'не понятно куда логировать'
                //AddError("Возникло исключение при инициализации транзакции", ex);
                Initialized = false;
            }
        }

        /// <summary>
        /// Добавление новой ошибки транзакции
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="ex">Исключение</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public sealed override void AddError(string message, Exception ex = null, params ErrorParameter[] parameters)
        {
            m_context.AddError(message, ex: ex, parameters: parameters);
        }
        /// <summary>
        /// Добавление новой ошибки при выполнении операции
        /// </summary>
        /// <param name="result">Результат операции</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public sealed override void AddError(OperationResult result, params ErrorParameter[] parameters)
        {
            m_context.AddError(result, parameters: parameters);
        }
        /// <summary>
        /// Добавление новой ошибки при выполнении операции
        /// </summary>
        /// <param name="result">Результат операции</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public sealed override void AddError(Models.JIRA.JiraResponse result, params ErrorParameter[] parameters)
        {
            m_context.AddError(result, parameters: parameters);
        }

        public override void Dispose()
        {
            try
            {
                if (m_context != null)
                    m_context.Dispose();
            }
            finally
            {
                m_context = null;
            }

        }
    }
}