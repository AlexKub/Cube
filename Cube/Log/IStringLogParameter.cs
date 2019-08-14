namespace Cube.Log
{
    /// <summary>
    /// Логируемый параметр
    /// </summary>
    public interface IStringLogParameter
    {
        /// <summary>
        /// Имя параметра
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Значение параметра
        /// </summary>
        string Value { get; }
        /// <summary>
        /// Преобразование к логируемой строке
        /// </summary>
        /// <returns>Логируемая строка</returns>
        string ToLogString();
    }
}
