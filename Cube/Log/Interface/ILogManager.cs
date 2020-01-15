namespace Cube.Log
{
    /// <summary>
    /// Менеджер логирования
    /// </summary>
    public interface ILogManager
    {
        /// <summary>
        /// Логирование
        /// </summary>
        /// <param name="context">Контекст логирования</param>
        /// <returns>Возвращает флаг успеха операции</returns>
        bool Log(ILogingContext context);
    }
    /// <summary>
    /// Менеджер логирования
    /// </summary>
    /// <typeparam name="TContext">Контекст логирования</typeparam>
    public interface ILogManager<TContext>
    {
        /// <summary>
        /// Логирование
        /// </summary>
        /// <param name="context">Контекст логирования</param>
        /// <returns>Возвращает флаг успеха операции</returns>
        bool Log(TContext context);
    }
}
