using PJT.Settings;
using System;
using System.Collections.Generic;

namespace PJT.Transactions
{
    /// <summary>
    /// Контекст транзакций
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public abstract class TransactionContext : IDisposable, IErrorsCollection, ISettingsProvider
    {
        bool m_disposed;

        /// <summary>
        /// Набор ошибок, возникших при выполнении текущей транзакции
        /// </summary>
        readonly List<TransactionError> m_errors = new List<TransactionError>();

        /// <summary>
        /// Список ошибок транзакции
        /// </summary>
        public IReadOnlyList<TransactionError> Errors { get { return m_errors; } }

        /// <summary>
        /// Пользовательские настройки контекста
        /// </summary>
        public SettingsCollection Settings { get; protected set; }

        /// <summary>
        /// Флаг корректной инициализации
        /// </summary>
        public bool Initialized { get; protected set; }

        /// <summary>
        /// Текущая транзакция
        /// </summary>
        public Transaction Transaction { get; private set; }

        /// <summary>
        /// Флаг освобождения ресурсов
        /// </summary>
        public bool Disposed { get { return m_disposed; } }

        /// <summary>
        /// Контекст Транзакции
        /// </summary>
        /// <param name="transaction">Экземпляр транзакции</param>
        public TransactionContext(Transaction transaction)
        {
            if (transaction == null)
                throw new ArgumentNullException("Передана пустая ссылка на Транзакцию при инициализации Контекста");

            Transaction = transaction;

            //базовая инициализация.
            //предполагается перед прочими
            var success = Init();

            if (!success)
            {
                Initialized = false;
                return;
            }

            Initialized = success;
        }

        /// <summary>
        /// Базовая инициализация экземпляра
        /// </summary>
        bool Init()
        {
            //инициализация настроек
            var s = TransactionSettings.Load();

            Settings = s.CurrentCollection;

            return Validate(Settings);
        }

        /// <summary>
        /// Валидация настроек
        /// </summary>
        /// <param name="settings">Текущие настройки</param>
        /// <returns>Возвращает флаг корреткности загруженных настроек</returns>
        bool Validate(SettingsCollection settings)
        {
            if (settings == null)
            {
                AddError("Не удалось получить экземпляр настроек при инициализации контекста");
                return false;
            }

            if (settings.JIRA == null)
            {
                AddError("Не загружен экземпляр настроек JIRA инициализации контекста");
                return false;
            }

            var ps_settings = settings.Project;

            if (ps_settings == null)
            {
                AddError("Не загружен экземпляр настроек Project инициализации контекста");
                return false;
            }

            if (string.IsNullOrEmpty(ps_settings.URL))
            {
                AddError("Не указана ссылка на сервер Project в файле настроек");
                return false;
            }

            var customFields = ps_settings.CustomFields;

            if (customFields == null || customFields.Count == 0)
            {
                AddError("Не указано Настраиваемых полей для раздела Project в файле настроек");
                return false;
            }
            else
            {
                int index = 0;
                foreach (ProjectCustomField cField in customFields)
                {
                    if (!cField.IsValid)
                    {
                        AddError("Не удалось корректно распарсить ID Настраиваемого поля Project в файле настроек", parameters: new ErrorParameter[] {
                            new ErrorParameter("Key", cField.Key.LogValue())
                            , new ErrorParameter("Description", cField.Description.LogValue())
                            , new ErrorParameter("Index", index.ToString())
                        });
                    }

                    index++;
                }
            }

            return true;
        }

        /// <summary>
        /// Добавление новой ошибки транзакции
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="ex">Исключение</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public void AddError(string message, [System.Runtime.CompilerServices.CallerMemberName] string caller = null, params ErrorParameter[] parameters)
        {
            m_errors.Add(new TransactionError(message, null, Append(parameters, BaseContextParams(caller))));
        }
        /// <summary>
        /// Добавление новой ошибки транзакции
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="ex">Исключение</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public void AddError(string message, Exception ex, [System.Runtime.CompilerServices.CallerMemberName] string caller = null, params ErrorParameter[] parameters)
        {
            parameters = Append(parameters, BaseContextParams(caller));
            parameters = Append(parameters, ExceptionParameters(ex));

            m_errors.Add(new TransactionError(message, ex, parameters));
        }
        /// <summary>
        /// Добавление информации об ошибке при выполнении операции
        /// </summary>
        /// <param name="result">Результат операции</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public void AddError(IOperationResult result, string message = null, [System.Runtime.CompilerServices.CallerMemberName] string caller = null, params ErrorParameter[] parameters)
        {
            if (message == null)
                message = result == null ? "NULL OPERATION RESULT" : (string.IsNullOrEmpty(result.Message) ? "EMPTY MESSAGE" : result.Message);

            var ex = result?.Exception;
            if (ex != null)
            {
                parameters = Append(parameters, BaseContextParams(caller));
                parameters = Append(parameters, ExceptionParameters(ex));
            }

            m_errors.Add(new TransactionOperationError(message, result?.Exception, result, parameters));
        }
        /// <summary>
        /// Добавление новой ошибки при выполнении операции
        /// </summary>
        /// <param name="result">Результат операции</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        public void AddError(Models.JIRA.JiraResponse result, string message = null, [System.Runtime.CompilerServices.CallerMemberName] string caller = null, params ErrorParameter[] parameters)
        {
            if (string.IsNullOrEmpty(message))
                message = result == null ? "NULL JIRA RESULT" : (string.IsNullOrEmpty(result.Message) ? "Неожиданный json-ответ" : result.Message);

            m_errors.Add(new TransactionJiraError(result, message, result?.Exception, Append(parameters, BaseContextParams(caller))));
        }

        ErrorParameter[] BaseContextParams(string caller)
        {
            //общие параметры для всех записей в лог
            return new ErrorParameter[] {
                new ErrorParameter("Вызвавший метод", caller)
                , new ErrorParameter("Added", DateTime.Now.ToLongTimeString())
            };
        }

        ErrorParameter[] ExceptionParameters(Exception ex)
        {
            if (ex == null)
                return null;

            var projectException = ex as Microsoft.SharePoint.Client.ServerException;
            if (projectException != null)
            {
                return new ErrorParameter[] {
                    //Id для поиска в журнале логов SharePoint
                    new ErrorParameter("CorrelationId", projectException.ServerErrorTraceCorrelationId)
                };
            }

            var webException = ex as System.Net.WebException;
            if (webException != null)
            {
                return new ErrorParameter[] {
                    //ответ от сервера там, где не был отлогирован
                    new ErrorParameter("Response", Utils.Global.GetResponseFromEx(ex))
                };
            }

            return null;
        }

        /// <summary>
        /// Добавление параметров
        /// </summary>
        /// <param name="baseParams"></param>
        /// <param name="appendingParams"></param>
        /// <returns></returns>
        ErrorParameter[] Append(ErrorParameter[] baseParams, ErrorParameter[] appendingParams)
        {
            if (baseParams == null || baseParams.Length == 0)
                if (appendingParams == null)
                    return new ErrorParameter[0];
                else
                    return appendingParams;
            else
            {
                if (appendingParams == null)
                    return baseParams;

                var oldLength = baseParams.Length;
                Array.Resize(ref baseParams, baseParams.Length + appendingParams.Length);

                for (int i = oldLength, j = 0; i < baseParams.Length && j < appendingParams.Length; i++, j++)
                {
                    baseParams[i] = appendingParams[j];
                }

                return baseParams;
            }
        }

        public virtual void Dispose()
        {
            if (m_disposed)
                return;

            try
            {
                m_errors.Clear();

                Settings = null;
                Transaction = null;
            }
            finally
            {
                m_disposed = true;
            }
        }

        string DebugDisplay()
        {
            return $"Errors: {m_errors.Count.ToString()}";
        }


    }

    /// <summary>
    /// Контекст транзакций
    /// </summary>
    public abstract class TransactionContext<TData> : TransactionContext
    {
        /// <summary>
        /// Текущая транзакция
        /// </summary>
        public new Transaction<TData> Transaction => base.Transaction as Transaction<TData>;

        /// <summary>
        /// Контекст Транзакции
        /// </summary>
        /// <param name="transaction">Экземпляр транзакции</param>
        public TransactionContext(Transaction<TData> transaction)
            : base(transaction) { }
    }
}