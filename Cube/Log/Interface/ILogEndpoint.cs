namespace Cube.Log
{
    /// <summary>
    /// Конечная точка логирования
    /// </summary>
    /// <typeparam name="TData">Тип логируемых данных</typeparam>
    public interface ILogEndpoint<TData>
    {
        /// <summary>
        /// Логирование
        /// </summary>
        /// <param name="data">Логируемые данные</param>
        /// <returns>Результат операции логирования</returns>
        bool Log(TData data);
    }
}
