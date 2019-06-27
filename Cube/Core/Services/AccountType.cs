namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Тип авторизации Сервиса
    /// </summary>
    public enum AccountType
    {
        /// <summary>
        /// Не известный
        /// </summary>
        UnKnown,
        /// <summary>
        /// Local system service
        /// </summary>
        [AccountTypeValue("LocalSystem")]
        LocalSystem,
        /// <summary>
        /// NT AUTHORITY\Local system service
        /// </summary>
        [AccountTypeValue(@"NT AUTHORITY\LocalSystem")]
        NT_LocalSystem,
        /// <summary>
        /// Network service
        /// </summary>
        [AccountTypeValue(@"Network System")]
        Network,
        /// <summary>
        /// NT AUTHORITY\Network service
        /// </summary>
        [AccountTypeValue(@"NT AUTHORITY\NetworkService")]
        NT_Network,
        /// <summary>
        /// .\Network service
        /// </summary>
        [AccountTypeValue(@".\Network System")]
        DomainNetwork,
        /// <summary>
        /// Пользователь
        /// </summary>
        LocalUser
    }
}
