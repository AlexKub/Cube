namespace Cube.WPF.Settings
{
    /// <summary>
    /// Видимая настройка Приложения
    /// </summary>
    /// <typeparam name="T">Тип значения</typeparam>
    public interface IVisibleApplicationSetting<T> : IApplicationSetting<T>
    {
        /// <summary>
        /// Видимое имя
        /// </summary>
        string DisplayName { get; }
        /// <summary>
        /// Подсказка
        /// </summary>
        string Tooltip { get; }
    }
}
