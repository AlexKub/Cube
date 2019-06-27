using System;

namespace Merlion.ECR.Update.Core.Environment
{
    /// <summary>
    /// Контексты регистрации Сборок
    /// </summary>
    [Flags]
    public enum AssemblyRegContext
    {
        /// <summary>
        /// Отсутствует
        /// </summary>
        None,
        /// <summary>
        /// Глобальный кеш сборок
        /// </summary>
        GAC,
        /// <summary>
        /// Регистрация через RegAsm
        /// </summary>
        RegAsm,
        /// <summary>
        /// Регистрация через RegSrv32
        /// </summary>
        RegSvr32
    }
}
