

using System.ComponentModel;
using System.IO;

namespace Merlion.ECR.Update.Core
{
    /// <summary>
    /// Общие константы
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Папка приложения по умолчанию
        /// </summary>
        public static readonly string RootFolder = @"C:\RoleTailoredClient_HotFixed";

        /// <summary>
        /// Имя исполняемого файла RTC
        /// </summary>
        public static string RTC_ExeFileName { get { return "Microsoft.Dynamics.Nav.Client.exe"; } }
        /// <summary>
        /// Имя процесса RTC
        /// </summary>
        public static string RTC_ProcessName { get { return "Microsoft.Dynamics.Nav.Client"; } }

        /// <summary>
        /// Имя файла лаунчера
        /// </summary>
        public static string Launcher_FileName { get { return "CitilinkLaunchCenter.exe"; } }

        /// <summary>
        /// Полное имя файла лаунчера
        /// </summary>
        public static string Launcher_ExePathName { get { return Path.Combine(RootFolder, Launcher_FileName); } }

        /// <summary>
        /// Расширения файлов
        /// </summary>
        public static class FileExtensions
        {
            /// <summary>
            /// Расшираение '.dll' в нижнем регистре
            /// </summary>
            public static readonly string DLL = ".dll";
        }

        /// <summary>
        /// Константы Манифеста
        /// </summary>
        public static class Manifest
        {
            /// <summary>
            /// Флаг корневого каталога
            /// </summary>
            public static readonly string RootDirectoryFlag = Core.Manifest.RTCManifest.RootDirectoryFlag;

            /// <summary>
            /// Имя типа файла по умолчанию
            /// </summary>
            public static readonly string DefaultItemType = "OTHER";

            /// <summary>
            /// Имя файла по умолчанию
            /// </summary>
            public static readonly string DefaultFileName = Core.Manifest.RTCManifest.DefaultFileName;

            /// <summary>
            /// Путь к манифесту обновления по умолчанию
            /// </summary>
            public static readonly string DefaultPathName = Path.Combine(RootFolder, DefaultFileName);

            /// <summary>
            /// Флаги действия
            /// </summary>
            public static class Actions
            {
                /// <summary>
                /// Добавить
                /// </summary>
                public static readonly string Add = "add";
                /// <summary>
                /// Удалить
                /// </summary>
                public static readonly string Remove = "delete";
                /// <summary>
                /// Зарегистрировать
                /// </summary>
                public static readonly string Reg = "reg";
                /// <summary>
                /// Разрегистрировать
                /// </summary>
                public static readonly string UnReg = "unreg";
            }
        }

        /// <summary>
        /// Файл Настроек обновления
        /// </summary>
        public static class UpdateSettings
        {
            /// <summary>
            /// Имя файла по умолчанию
            /// </summary>
            public static readonly string DefaultFileName = Core.Manifest.UpdateSettings.DefaultFileName;

            /// <summary>
            /// Путь к файлу настроек по умолчанию
            /// </summary>
            public static readonly string DefaultPathName = Path.Combine(RootFolder, DefaultFileName);

        }

        /// <summary>
        /// Константы логирования
        /// </summary>
        public static class Log
        {
            /// <summary>
            /// Флаги исключения сообщений
            /// </summary>
            public static class Exclude
            {
                /// <summary>
                /// Отправка на сервис мониторинга
                /// </summary>
                [Description("Логирование опроса состояния кассы 'CheckDeviceState'")]
                public static readonly string CL_Monitoring = "cl_monitoring";

                /// <summary>
                /// Получение таймаута от балансировщика
                /// </summary>
                [Description("Логирование ошибок при запросе таймаута от балансера 'GetShtrihM_CheckingTimeout'")]
                public static readonly string CL_CheckTimeout = "cl_timeoutCheck";
            }
        }

        /// <summary>
        /// Константы файлов и каталогов
        /// </summary>
        public static class Files
        {
            /// <summary>
            /// Имя папки с обновлениями
            /// </summary>
            public static readonly string UpdateDirectoryName = "Update";
            /// <summary>
            /// Имя папки с бэкапами
            /// </summary>
            public static readonly string BackupDirectoryName = "Backup";

            /// <summary>
            /// Имя папки Add-ins
            /// </summary>
            public static readonly string AddinsDirectoryName = "Add-ins";

            /// <summary>
            /// Имя папки Tools
            /// </summary>
            public static readonly string ToolsDirectoryName = "Tools";
        }

        public static class ServiceNames
        {
            /// <summary>
            /// Имя службы обновления
            /// </summary>
            public static readonly string UpdateServiceName = "ECRUpdateService";

            /// <summary>
            /// Имя службы оборудования (в службе указывается отдельно!)
            /// </summary>
            public static readonly string DeviceServiceName = "CitilinkECRService";
        }
    }
}
