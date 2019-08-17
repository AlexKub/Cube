namespace Cube.Log
{
    /// <summary>
    /// Контекст стокового логирования
    /// </summary>
    interface IStringParameterizedEntity : IParameterizedEntity<string, IStringLogParameter>
    {

    }

    /// <summary>
    /// Контекст стокового логирования
    /// </summary>
    /// <typeparam name="TParameter">Тип параметров</typeparam>
    interface IStringParameterizedEntity<TParameter> : IParameterizedEntity<string, TParameter>
        where TParameter : IStringLogParameter
    {

    }
}
