namespace Cube.Log
{
    /// <summary>
    /// Параметр логирования
    /// </summary>
    /// <typeparam name="TValue">Значение параметра</typeparam>
    public interface ILogParameter<TValue>
    {
        /// <summary>
        /// Значение параметра
        /// </summary>
        TValue Value { get; }
    }
}
