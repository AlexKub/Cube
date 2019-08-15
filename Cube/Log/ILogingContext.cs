using System;

namespace Cube.Log
{
    /// <summary>
    /// Контекст логирования
    /// </summary>
    public interface ILogingContext
    {
        /// <summary>
        /// Экземпляр исключения
        /// </summary>
        Exception Exception { get; }
        /// <summary>
        /// Описание логируемого события
        /// </summary>
        string Message { get; }
        /// <summary>
        /// Флаги логирования
        /// </summary>
        int Flags { get; }
        /// <summary>
        /// Уровень логирования
        /// </summary>
        int Level { get; }
    }

}
