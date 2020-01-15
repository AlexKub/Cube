namespace Cube.Log
{
    /// <summary>
    /// Конечная точка логирования
    /// </summary>
    /// <typeparam name="TContext">Контекст логирования</typeparam>
    public interface ILogContextEndpoint<TContext, TData> : ILogEndpoint<TData>
        where TContext : ILogingContext
    {
        /// <summary>
        /// Логирование
        /// </summary>
        /// <param name="context">Текущий контекст логирования</param>
        /// <param name="data">Логируемые данные</param>
        /// <returns>Возвращает флаг успеха операции</returns>
        bool Log(TContext context, TData data);
    }
}
