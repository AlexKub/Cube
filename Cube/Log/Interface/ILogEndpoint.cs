namespace Cube.Log
{
    /// <summary>
    /// Конечная точка логирования
    /// </summary>
    /// <typeparam name="TMessage">Тип логируемого сообщения</typeparam>
    public interface ILogEndpoint<TMessage>
    {
        /// <summary>
        /// Логирование
        /// </summary>
        /// <param name="message">Логируемое сообщение</param>
        /// <returns>Результат операции логирования</returns>
        bool Log(TMessage message);
    }
}
