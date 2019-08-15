using System.Collections.Generic;

namespace Cube.Log
{
    /// <summary>
    /// Параметризированный контекст логирования
    /// </summary>
    public interface IParametrizedLogingContext<TKey, TValue, TParameter> : ILogingContext
        where TParameter : ILogParameter<TKey, TValue>
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
