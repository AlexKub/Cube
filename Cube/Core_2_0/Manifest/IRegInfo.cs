namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Информация о регистрации библиотеки
    /// </summary>
    public interface IRegInfo
    {
        /// <summary>
        /// Действие регистрации
        /// </summary>
        RegistrationActions Action { get; }
    }
}
