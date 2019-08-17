using System.Collections.Generic;

namespace Cube.Log
{
    /// <summary>
    /// Параметризированный контекст логирования
    /// </summary>
    /// <typeparam name="TParameter">Тип параметра</typeparam>
    /// <typeparam name="TValue">Тип заначения параметра</typeparam>
    public interface IParametrizedLogingContext<TParameter, TValue> : ILogingContext
        where TParameter : ILogParameter<TValue>
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
