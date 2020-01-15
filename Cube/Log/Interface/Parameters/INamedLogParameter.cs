namespace Cube.Log
{
    /// <summary>
    /// Именованный параметр логирования
    /// </summary>
    /// <typeparam name="TValue">Тип значения параметра</typeparam>
    public interface INamedLogParameter<TValue> : ILogParameter<TValue>
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        string Name { get; }
    }
}
