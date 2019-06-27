using System;

namespace Merlion.ECR.Update.Core.Environment
{
    /// <summary>
    /// Результат регестрации сборок в GAC
    /// </summary>
    public enum RegistrationResult : uint
    {
        //IAssemblyCache::UninstallAssembly Method
        //https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/fusion/iassemblycache-uninstallassembly-method
        //
        //вспомогалочка
        //https://searchcode.com/codesearch/view/8668231/

        /// <summary>
        /// Не определён (используется только управляемым кодом)
        /// </summary>
        UnKnown,

        /// <summary>
        /// Успех
        /// </summary>
        Uninstalled = 1,
        /// <summary>
        /// Сборка используется приложением
        /// </summary>
        InUse = 2,
        /// <summary>
        /// Нету в GAC
        /// </summary>
        AlreadyUninstalled = 3,
        /// <summary>
        /// Не использутеся (https://searchcode.com/codesearch/view/8668231/)
        /// </summary>
        Pending = 4,
        /// <summary>
        /// Имеются win-инсталлеры, ссылающиеся на сборку
        /// </summary>
        HasInstallReferences = 5,
        /// <summary>
        /// Ссылка указанная в pRefData не найдена в GAC
        /// </summary>
        ReferenceNotFound = 6,
        /// <summary>
        /// OleInitialize Failed для Regsvr32
        /// </summary>
        OleFailed = 7,
        /// <summary>
        /// Load Failed
        /// </summary>
        LoadFailed = 8,
        /// <summary>
        /// Fail Entery point
        /// </summary>
        WrongEntryPoint = 9,
        /// <summary>
        /// Worng input args
        /// </summary>
        WrongArgs = 10,
        //наши флаги
        /// <summary>
        /// Неудача (используется только управляемым кодом)
        /// </summary>
        Fail,
        /// <summary>
        /// Сборка успешно добавлена в GAC (используется только управляемым кодом)
        /// </summary>
        Installed,
    }

}