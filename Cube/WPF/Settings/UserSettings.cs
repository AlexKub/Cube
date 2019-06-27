using Merlion.ECR.Update.Core.Log;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace RTCManifestGenerator
{
    /// <summary>
    /// Настройки для текущего юзера
    /// </summary>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("{UserName}")]
    public class UserSettings
    {
        readonly static LogSet m_loger = LogManager.GetDefaultLogSet<UserSettings>();
        List<string> m_domains = new List<string>();
        MachineSettings m_curentMachineSettings;
        List<MachineSettings> m_machineSettings = new List<MachineSettings>();

        /// <summary>
        /// Ссылка на родительский файл настроек
        /// </summary>
        [XmlIgnore]
        public MG_Settings SettingsFile { get; set; }

        /// <summary>
        /// Настройки для текущей машины
        /// </summary>
        [XmlIgnore]
        public MachineSettings CurentMachineSettings { get { return m_curentMachineSettings; } }

        /// <summary>
        /// Доменные имена серверов RTC
        /// </summary>
        [XmlArrayItem(ElementName = "Domain")]
        [XmlArray(ElementName = "RTC_Domains")]
        public string[] RTC_Domains
        {
            get { return m_domains.ToArray(); }
            set
            {
                m_domains.Clear();

                if (value != null)
                    m_domains.AddRange(value);
            }
        }

        /// <summary>
        /// Доменное имя пользователя
        /// </summary>
        [XmlAttribute]
        public string UserName { get; set; }

        [XmlArray]
        /// <summary>
        /// Настройки для компьютеров, на которых заходил пользователь
        /// </summary>
        public MachineSettings[] MachinesSettings
        {
            get { return m_machineSettings.ToArray(); }
            set
            {
                m_machineSettings.Clear();

                if (value != null)
                    m_machineSettings.AddRange(value);
            }
        }

        /// <summary>
        /// Конструктор для сериализатора! В коде юзать с параметром MG_Settings!
        /// </summary>
        public UserSettings() {  }

        /// <summary>
        /// Основной конструктор
        /// </summary>
        /// <param name="settingsFile">Ссылка на обёртку файла настроек</param>
        public UserSettings(MG_Settings settingsFile)
        {
            SettingsFile = settingsFile;
        }

        internal void InitRelations()
        {
            SetCurentUserMachine();
        }

        /// <summary>
        /// Добавление нового доменного имени
        /// </summary>
        /// <param name="domainName">Доменное имя</param>
        public void AddDomain(string domainName)
        {
            if (domainName == null)
                return;

            if (!ContainsDomain(domainName))
                m_domains.Add(domainName);
        }

        /// <summary>
        /// Удаление доменного имени
        /// </summary>
        /// <param name="domainName">Доменное имя</param>
        public void RemoveDomain(string domainName)
        {
            if (domainName == null)
                return;

            m_domains.Remove(domainName);
        }

        /// <summary>
        /// Регистронезависимая проверка наличия домена в коллекции
        /// </summary>
        /// <param name="domainName">Имя домена</param>
        /// <returns>Возвращает наличие соответствующего домена в текущей коллекции</returns>
        public bool ContainsDomain(string domainName)
        {
            foreach (var domain in m_domains)
                if (domain.Equals(domainName, StringComparison.InvariantCultureIgnoreCase))
                    return true;

            return false;
        }

        /// <summary>
        /// Получение экземпляра для текущего юзера
        /// </summary>
        /// <param name="settingFile">Ссыль на файл настроек</param>
        /// <returns>Возвращает новый экземпляр с проставленным юзером - текущим </returns>
        public static UserSettings ForCurentUser(MG_Settings settingFile)
        {
            var us = new UserSettings(settingFile);
            us.UserName = Environment.UserName;

            return us;
        }

        MachineSettings GetCurentMachineSettings()
        {
            //берём десериализованные или создаём новые

            MachineSettings dus = null;
            var curentMachineName = MachineSettings.GetMachineName();

            if (m_machineSettings.Count == 0 || !m_machineSettings.Any(us => curentMachineName.Equals(us.MachineName)))
                dus = MachineSettings.ForCurentMachine(SettingsFile);
            else
                dus = m_machineSettings.FirstOrDefault(us => curentMachineName.Equals(us.MachineName));

            if (dus == null)
            {
                m_loger.Log("Не удалось получить настроек для пользователя, создан экземпляр по умолчанию");
                dus = MachineSettings.ForCurentMachine(SettingsFile);
            }

            return dus;
        }

        void SetCurentUserMachine()
        {
            m_curentMachineSettings = GetCurentMachineSettings();

            if (!m_machineSettings.Contains(m_curentMachineSettings))
                m_machineSettings.Add(m_curentMachineSettings);
        }

    }
}
