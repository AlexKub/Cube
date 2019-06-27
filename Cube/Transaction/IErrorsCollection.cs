using System;

namespace PJT.Transactions
{
    /// <summary>
    /// Коллекция ошибок
    /// </summary>
    public interface IErrorsCollection
    {
        /// <summary>
        /// Добавление новой ошибки транзакции
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="ex">Исключение</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        void AddError(string message, [System.Runtime.CompilerServices.CallerMemberName] string caller = null, params ErrorParameter[] parameters);
        /// <summary>
        /// Добавление новой ошибки транзакции
        /// </summary>
        /// <param name="message">Описание ошибки</param>
        /// <param name="ex">Исключение</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        void AddError(string message, Exception ex, [System.Runtime.CompilerServices.CallerMemberName] string caller = null, params ErrorParameter[] parameters);
        /// <summary>
        /// Добавление информации об ошибке при выполнении операции
        /// </summary>
        /// <param name="result">Результат операции</param>
        /// <param name="message">Дополнительное сообщение</param>
        /// <param name="parameters">Прочие параметры</param>
        /// <returns>Возвращает флаг наличия элементов и инициализированной ссылки ндля коллекции</returns>
        void AddError(IOperationResult result, string message = null, [System.Runtime.CompilerServices.CallerMemberName] string caller = null, params ErrorParameter[] parameters);
    }
}
