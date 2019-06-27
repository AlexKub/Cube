namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Действия регистрации для сборок
    /// </summary>
    public enum RegistrationActions
    {
        /// <summary>
        /// Отсутствие действия (по умолчанию)
        /// </summary>
        Ignore,
        /// <summary>
        /// Регистрация
        /// </summary>
        Reg,
        /// <summary>
        /// Разрегистрация
        /// </summary>
        UnReg
    }
}
