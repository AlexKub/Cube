using System;

namespace Cube.Log
{
    /// <summary>
    /// Управляющий контекст логирования
    /// </summary>
    public interface ILogingContext
    {
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
