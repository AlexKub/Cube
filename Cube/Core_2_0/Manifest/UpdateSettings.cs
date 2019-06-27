using System;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Общие редактируемые настройки обновления
    /// </summary>
    [Serializable]
    public class UpdateSettings : XmlManifest
    {
        /// <summary>
        /// Имя файла настроек по умолчанию
        /// </summary>
        public static readonly string DefaultFileName = "UpdateSettings.xml";

        /// <summary>
        /// Возвращает или задаёт флаг проверки на наличие сборок в GAC при запуске приложения
        /// </summary>
        public bool CheckGAC { get; set; } = true;

        /// <summary>
        /// Возвращает или задаёт первый сервер авторизации, к которому будем коннектиться
        /// </summary>
        public string Default_LDAP { get; set; }

        /// <summary>
        /// Версия метода мониторинга кассы
        /// </summary>
        public int StatusMethodVersion { get; set; }

        /// <summary>
        /// Таймаут опроса балансёра (в минутах)
        /// </summary>
        public int BalancerRequestTimeout { get; set; } = 1;

        /// <summary>
        /// Загрузка настроек
        /// </summary>
        /// <param name="pathName">Полный путь к файлу для загрузки (по умолчанию - имя файла по умолчанию)</param>
        /// <returns>Возвращает настройки считанные из файла</returns>
        public static UpdateSettings Load(string pathName = null)
        {
            if (string.IsNullOrEmpty(pathName))
                pathName = DefaultFileName;

            return Deserialize<UpdateSettings>(pathName);
        }

        /// <summary>
        /// Сохраняет внесённые изменения в файле настроек
        /// </summary>
        /// <param name="pathName">Полный путь к файлу настроек. Если не указан - берётся путь по умолчанию</param>
        public override void Save(string pathName = null)
        {
            if (string.IsNullOrEmpty(pathName))
                pathName = base.FileInfo == null ? DefaultFileName : base.FileInfo.FullName;

            Serialize(this, pathName);
        }

        protected override XmlManifest GetDefault()
        {
            var settings = new UpdateSettings();

            settings.CheckGAC = true;
            settings.Default_LDAP = string.Empty;
            settings.StatusMethodVersion = 3;

            return settings;

        }

        protected override string GetDefaultFileName()
        {
            return DefaultFileName;
        }
    }
}
