using Merlion.ECR.Update.Core.Log;
using System;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Настройки логирования
    /// </summary>
    [Serializable]
    public class LogSettingsManifest : XmlManifest
    {
        /// <summary>
        /// Имя файла настроек по умолчанию
        /// </summary>
        public static readonly string DefaultFileName = "LogSettings.xml";

        LogSettings m_global;
        LogSettings m_device;
        LogSettings m_update;
        LogSettings m_launcher;

        /// <summary>
        /// Общие настройки
        /// </summary>
        public LogSettings Global { get { return GetSettings(ref m_global); } set { m_global = value; } }
        /// <summary>
        /// Настройки для Сервиса оборудования
        /// </summary>
        public LogSettings DeviceService { get { return GetSettings(ref m_device); } set { m_device = value; } }
        /// <summary>
        /// Настройки для Сервиса обновления
        /// </summary>
        public LogSettings UpdateService { get { return GetSettings(ref m_update); } set { m_update = value; } }
        /// <summary>
        /// Настройки для Центра запуска
        /// </summary>
        public LogSettings Launcher { get { return GetSettings(ref m_launcher); } set { m_launcher = value; } }

        /// <summary>
        /// Загрузка настроек
        /// </summary>
        /// <param name="pathName">Полный путь к файлу для загрузки (по умолчанию - имя файла по умолчанию)</param>
        /// <returns>Возвращает настройки считанные из файла</returns>
        public static LogSettingsManifest Load(string pathName = null)
        {
            try
            {
                if (string.IsNullOrEmpty(pathName))
                    pathName = DefaultFileName;

                var manifest = Deserialize<LogSettingsManifest>(pathName);

                return manifest;
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при запросе настроек логирования. Будет возвращён экземпляр по умолчанию", ex
                    , new LogParameter("Путь к файлу", LogMessageBuilder.GetStringLogVal(pathName)));

                return (LogSettingsManifest)new LogSettingsManifest().GetDefault();
            }
        }

        /// <summary>
        /// Сохраняет внесённые изменения в файле настроек
        /// </summary>
        /// <param name="pathName">Полный путь к файлу настроек. Если не указан - берётся путь по умолчанию</param>
        public override void Save(string pathName = null)
        {
            if (string.IsNullOrEmpty(pathName))
                pathName = FileInfo == null ? DefaultFileName : FileInfo.FullName;

            Serialize(this, pathName);
        }

        protected override XmlManifest GetDefault()
        {
            var settings = new LogSettingsManifest();

            var defaultSettings = GetDefaultLogSettings();

            settings.Global = defaultSettings;
            settings.DeviceService = defaultSettings;
            settings.UpdateService = defaultSettings;
            settings.Launcher = defaultSettings;

            return settings;

        }

        LogSettings GetDefaultLogSettings()
        {
            var defaultSettings = new LogSettings();
            defaultSettings.FileLogingFolder = string.Empty;
            defaultSettings.LogSetFlag = LogManager.DefaultLogSetFlag;
            defaultSettings.Log_Level = (int)LogLevel.Hight;

            return defaultSettings;
        }

        LogSettings GetSettings(ref LogSettings field)
        {
            if (field == null)
                field = GetDefaultLogSettings();

            return field;
        }

        protected override string GetDefaultFileName()
        {
            return DefaultFileName;
        }
    }
}
