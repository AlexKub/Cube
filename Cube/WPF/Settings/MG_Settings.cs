using System;
using System.Collections.Generic;
using Merlion.ECR.Update.Core.Manifest;
using System.Xml.Serialization;
using System.Linq;

namespace RTCManifestGenerator
{
    /// <summary>
    /// Настройки Манифест генератора
    /// </summary>
    [Serializable]
    public class MG_Settings : XmlManifest
    {
        public static string DefaultFileName = "MG_Settings.xml";

        List<UserSettings> m_usersSettings = new List<UserSettings>();
        UserSettings m_curentUserSettings;

        /// <summary>
        /// Настройки Пользователя
        /// </summary>
        [XmlArray]
        public UserSettings[] UsersSettings
        {
            get { return m_usersSettings.ToArray(); }
            set
            {
                m_usersSettings.Clear();

                if (value != null)
                    m_usersSettings.AddRange(value);
            }
        }
        /// <summary>
        /// Доменные имена серверов RTC
        /// </summary>
        [XmlIgnore]
        public string[] RTC_Domains
        {
            get { return m_curentUserSettings.RTC_Domains; }
            set { m_curentUserSettings.RTC_Domains = value; }
        }

        [XmlIgnore]
        /// <summary>
        /// Действия с сохраняемыми в Настройках каталогами
        /// </summary>
        public SettingsCollectionWrapper Directories { get { return m_curentUserSettings.CurentMachineSettings.Directories; } }

        [XmlIgnore]
        /// <summary>
        /// Действия с сохраняемыми в Настройках файлами
        /// </summary>
        public SettingsCollectionWrapper Files { get { return m_curentUserSettings.CurentMachineSettings.Files; } }

        [XmlIgnore]
        /// <summary>
        /// Поледние каталоги Приложения (Для работы с каталогами использовать Directories)
        /// </summary>
        public NamedSettingsValue[] LastDirectories
        {
            get { return m_curentUserSettings.CurentMachineSettings.LastDirectories; }
            set { m_curentUserSettings.CurentMachineSettings.LastDirectories = value; }
        }

        [XmlIgnore]
        /// <summary>
        /// Поледние каталоги Приложения (Для работы с каталогами использовать Directories)
        /// </summary>
        public NamedSettingsValue[] LastFiles
        {
            get { return m_curentUserSettings.CurentMachineSettings.LastFiles; }
            set { m_curentUserSettings.CurentMachineSettings.LastFiles = value; }
        }

        /// <summary>
        /// Добавление нового доменного имени
        /// </summary>
        /// <param name="domainName">Доменное имя</param>
        public void AddDomain(string domainName)
        {
            m_curentUserSettings.AddDomain(domainName);
        }

        /// <summary>
        /// Удаление доменного имени
        /// </summary>
        /// <param name="domainName">Доменное имя</param>
        public void RemoveDomain(string domainName)
        {
            m_curentUserSettings.RemoveDomain(domainName);
        }

        /// <summary>
        /// Регистронезависимая проверка наличия домена в коллекции
        /// </summary>
        /// <param name="domainName">Имя домена</param>
        /// <returns>Возвращает наличие соответствующего домена в текущей коллекции</returns>
        public bool ContainsDomain(string domainName)
        {
            return m_curentUserSettings.ContainsDomain(domainName);
        }

        public override void Save(string pathName = null)
        {
            if (string.IsNullOrWhiteSpace(pathName))
                pathName = DefaultFileName;

            Serialize(this, pathName);
        }

        public static MG_Settings Load()
        {
            var settings = Deserialize<MG_Settings>(DefaultFileName, true);

            settings.InitRelations();

            return settings;
        }

        protected override XmlManifest GetDefault()
        {
            return new MG_Settings();
        }


        public MG_Settings() : base() { }


        protected override string GetDefaultFileName()
        {
            return DefaultFileName;
        }

        void InitRelations()
        {
            //после инициализации всех дочерних объектов
            //инициализируем в них ссылки на родительский

            SetCurentUserSettings();

            m_curentUserSettings.SettingsFile = this;

            foreach (var us in m_usersSettings)
            {
                us.SettingsFile = this;
                us.InitRelations();

                foreach (var ms in us.MachinesSettings)
                    ms.InitSettingsFile(this);
            }
        }

        UserSettings GetCurentUserSettings()
        {
            //берём десериализованные или создаём новые

            UserSettings dus = null;
            var curentUserName = Environment.UserName;

            if (m_usersSettings.Count == 0 || !m_usersSettings.Any(us => curentUserName.Equals(us.UserName)))
                dus = UserSettings.ForCurentUser(this);
            else
            {
                dus = m_usersSettings.FirstOrDefault(us => curentUserName.Equals(us.UserName));
            }

            if (dus == null)
            {
                m_loger.Log("Не удалось получить настроек для пользователя, создан экземпляр по умолчанию");
                dus = UserSettings.ForCurentUser(this);
            }

            return dus;
        }

        void SetCurentUserSettings()
        {
            m_curentUserSettings = GetCurentUserSettings();

            if (!m_usersSettings.Contains(m_curentUserSettings))
                m_usersSettings.Add(m_curentUserSettings);
        }
    }
}
