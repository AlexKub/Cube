namespace Cube.Log
{
    /// <summary>
    /// Уникальный параметр логирования
    /// </summary>
    /// <typeparam name="TKey">Уникальный ключ параметра</typeparam>
    /// <typeparam name="TValue">Значение параметра</typeparam>
    public interface IUniqueLogParameter<TKey, TValue> : ILogParameter<TValue>
    {
        /// <summary>
        /// Уникальный ключ параметра
        /// </summary>
        TKey Key { get; }
    }
}
