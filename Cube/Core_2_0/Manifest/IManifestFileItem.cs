
namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Данные о файле из манифеста
    /// </summary>
    public interface IManifestFileItem
    {
        /// <summary>
        /// Тип (пока всегда UNKNOWN)
        /// </summary>
        string Type { get; }
        /// <summary>
        /// Имя файла
        /// </summary>
        string FileName { get; }
        /// <summary>
        /// Список папок
        /// </summary>
        string[] Folders { get; }
        /// <summary>
        /// Контрольная сумма файла
        /// </summary>
        string CRC { get; }
        /// <summary>
        /// Версия файла
        /// </summary>
        string Version { get; }
        /// <summary>
        /// Флаг регистрации в GAC
        /// </summary>
        IRegInfo GAC { get; }
        /// <summary>
        /// Флаг регистрации через RegAsm
        /// </summary>
        IRegInfo REGASM { get; }
        /// <summary>
        /// Флаг регистрации через RegSvr32
        /// </summary>
        IRegInfo REGSRV32 { get; }
        /// <summary>
        /// Флаг удаления файла
        /// </summary>
        bool Delete { get; }
        /// <summary>
        /// Не обновлять при обновлении
        /// </summary>
        bool DoNotUpdate { get; }
        /// <summary>
        /// Токен публичного ключа
        /// </summary>
        string PublicKeyToken { get; }
        /// <summary>
        /// Имя ControlAddIn'а для NAV
        /// </summary>
        string AddinName { get; }
    }

}