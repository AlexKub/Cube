using System;

namespace Cube.Log
{
    /// <summary>
    /// Логируемые члены класса
    /// </summary>
    [Flags]
    public enum LogMembers
    {
        /// <summary>
        /// Не определено (по умолчанию)
        /// </summary>
        UnKnown = 0,
        /// <summary>
        /// Свойства
        /// </summary>
        Properties = 1,
        /// <summary>
        /// Поля
        /// </summary>
        Fields = 1 << 1
    }
}
