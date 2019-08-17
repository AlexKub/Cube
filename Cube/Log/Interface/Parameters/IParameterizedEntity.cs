using System.Collections.Generic;

namespace Cube.Log
{
    /// <summary>
    /// Параметризованная сущность
    /// </summary>
    public interface IParameterizedEntity<TValue, TParameter>
        where TParameter : ILogParameter<TValue>
    {
        /// <summary>
        /// Флаг наличия параметров
        /// </summary>
        bool HasParameters { get; }
        /// <summary>
        /// Параметры
        /// </summary>
        ICollection<TParameter> Parameters { get; }

    }
}
