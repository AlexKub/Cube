using System;

namespace Cube.Log
{
    /// <summary>
    /// Сущность с экземпляром исключения
    /// </summary>
    public interface IExceptionCarrier
    {
        /// <summary>
        /// Флаг наличия исключения
        /// </summary>
        bool HasException { get; }

        /// <summary>
        /// Экзмемпляр исключения
        /// </summary>
        Exception Exception { get; }
    }
}
