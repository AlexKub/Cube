using System.Collections.Generic;

namespace Cube.Log
{
    /// <summary>
    /// Параметризированный контекст логирования
    /// </summary>
    public interface IParametrizedLogingContext<TParameter> : ILogingContext
    {
        /// <summary>
        /// Флаг наличия параметров
        /// </summary>
        bool HasParameters { get; }
        /// <summary>
        /// Параметры логирования
        /// </summary>
        ICollection<TParameter> Parameters { get; }


    }
}
