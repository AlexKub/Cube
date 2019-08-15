namespace Cube.Log
{
    /// <summary>
    /// Контекст стокового логирования
    /// </summary>
    interface IStringLogingContext : IParametrizedLogingContext<IStringLogParameter>
    {

    }

    /// <summary>
    /// Контекст стокового логирования
    /// </summary>
    /// <typeparam name="TParameter">Тип параметров</typeparam>
    interface IStringLogingContext<TParameter> : IParametrizedLogingContext<TParameter>
        where TParameter : IStringLogParameter
    {

    }
}
