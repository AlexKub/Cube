using System;

namespace Cube.Log.Interface
{
    /// <summary>
    /// Логирование исключений
    /// </summary>
    public interface IExceptionLoger
    {
        /// <summary>
        /// Логирование исключения
        /// </summary>
        /// <param name="ex">Экземпляр исключения</param>
        /// <param name="description">Описание исключения</param>
        /// <param name="parameters">Параметры</param>
        /// <returns>Возвращает флаг успешного логирования</returns>
        bool Log(Exception ex, string description, params IStringLogParameter[] parameters);
    }
}
