namespace Merlion.ECR.Update.Core.SeparatedAssemblyLoad
{
    /// <summary>
    /// Флаги состояния загруженной сборки
    /// </summary>
    internal enum LoadedState
    {
        /// <summary>
        /// Сборка не загружена в Домен
        /// </summary>
        NotLoaded,
        /// <summary>
        /// Загружена с таким же именем, но из другого файла
        /// </summary>
        LoadedDifferent,
        /// <summary>
        /// Загружена точно такая же сборка
        /// </summary>
        LoadedSame
    }
}
