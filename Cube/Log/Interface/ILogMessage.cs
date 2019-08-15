namespace Cube.Log
{
    /// <summary>
    /// Собщение логирования
    /// </summary>
    /// <typeparam name="TKey">Тип ключа поиска сообщения</typeparam>
    public interface ILogMessage<TKey> 
    {
        /// <summary>
        /// Ключ поиска сообщения
        /// </summary>
        TKey MessageKey { get; }
    }
}
