namespace Cube.Log
{
    /// <summary>
    /// Логируемый параметр
    /// </summary>
    public interface IStringLogParameter : INamedLogParameter<string>
    {
        /// <summary>
        /// Добавление параметра при построении
        /// </summary>
        /// <param name="sb">Экземпляр <see cref="System.Text.StringBuilder"/>, используемый при построении конечной логируемой строки</param>
        void Append(System.Text.StringBuilder sb);
    }
}
