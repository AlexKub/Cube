using System;
using System.Collections.Generic;

namespace Cube.Log
{
    /// <summary>
    /// Считывание логов
    /// </summary>
    /// <typeparam name="TMessageKey">Тип ключа сообщения</typeparam>
    /// <typeparam name="TMessage">Тип сообщения</typeparam>
    public interface ILogReader<TMessageKey, TMessage>
        where TMessage : ILogMessage<TMessageKey>
    {
        /// <summary>
        /// Считывание набора сообщений
        /// </summary>
        /// <returns>Считанный набор сообщений</returns>
        IReadOnlyCollection<TMessage> Read();
        /// <summary>
        /// Считывание сообщений, удовлетворяющие условию
        /// </summary>
        /// <param name="predicate">Условие выбора сообщений</param>
        /// <returns>Возвращает считанную коллекцию, удовлетворяющую условию</returns>
        IReadOnlyCollection<TMessage> ReadWhere(Func<TMessage, bool> predicate);
    }
}
