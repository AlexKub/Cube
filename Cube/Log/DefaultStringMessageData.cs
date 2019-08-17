using System;
using System.Collections.Generic;

namespace Cube.Log
{
    /// <summary>
    /// Данные строкового логирования по умолчанию
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    internal struct DefaultStringMessageData : IStringLogingContext, IExceptionCarrier
    {
        /// <summary>
        /// Сообщение логирования
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Флаг наличия исключения
        /// </summary>
        public bool HasException => Exception != null;
        /// <summary>
        /// Логируемое исключение
        /// </summary>
        public Exception Exception { get; set; }
        /// <summary>
        /// Флаг наличия параметров
        /// </summary>
        public bool HasParameters => Parameters != null && Parameters.Count > 0;
        /// <summary>
        /// Логируемые параметры
        /// </summary>
        public ICollection<IStringLogParameter> Parameters { get; set; }

        /// <summary>
        /// A Message... | ?(HasException |) Parameters: N
        /// </summary>
        /// <returns></returns>
        string DebugDisplay() => $"{(Message.TrimLength(30).LogValue())} | {(HasException ? "HasException | " : string.Empty)}Parameters: {(HasParameters ? Parameters.Count.ToString() : "0")}";
    }
}
