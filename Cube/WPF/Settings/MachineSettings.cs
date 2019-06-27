using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace RTCManifestGenerator
{
    /// <summary>
    /// Настройки для текущей машины
    /// </summary>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("{MachineName}")]
    public class MachineSettings
    {
        List<NamedSettingsValue> m_lastDirs = new List<NamedSettingsValue>();
        SettingsCollectionWrapper m_dirs;
        List<NamedSettingsValue> m_lastFiles = new List<NamedSettingsValue>();
        SettingsCollectionWrapper m_files;

        /// <summary>
        /// Действия с сохраняемыми в Настройках каталогами
        /// </summary>
        public SettingsCollectionWrapper Directories { get { return m_dirs; } }

        /// <summary>
        /// Действия с сохраняемыми в Настройках файлами
        /// </summary>
        public SettingsCollectionWrapper Files { get { return m_files; } }

        /// <summary>
        /// Имя компьютера
        /// </summary>
        [XmlAttribute]
        public string MachineName { get; set; }

        [XmlArrayItem(ElementName = "Directory")]
        [XmlArray(ElementName = "Directories")]
        /// <summary>
        /// Поледние каталоги Приложения (Для работы с каталогами использовать Directories)
        /// </summary>
        public NamedSettingsValue[] LastDirectories
        {
            get { return m_lastDirs.ToArray(); }
            set
            {
                m_lastDirs.Clear();

                if (value != null)
                    m_lastDirs.AddRange(value);
            }
        }

        [XmlArrayItem(ElementName = "File")]
        [XmlArray(ElementName = "Files")]
        /// <summary>
        /// Поледние каталоги Приложения (Для работы с каталогами использовать Directories)
        /// </summary>
        public NamedSettingsValue[] LastFiles
        {
            get { return m_lastFiles.ToArray(); }
            set
            {
                m_lastFiles.Clear();

                if (value != null)
                    m_lastFiles.AddRange(value);
            }
        }

        /// <summary>
        /// !!Конструктор для сериализатора!! Использовать в коде с параметрами
        /// </summary>
        public MachineSettings() { }

        public MachineSettings(MG_Settings settingsFile)
        {
            m_dirs = new SettingsCollectionWrapper(settingsFile, m_lastDirs);
            m_files = new SettingsCollectionWrapper(settingsFile, m_lastFiles);
        }

        public static MachineSettings ForCurentMachine(MG_Settings settingsFile)
        {
            MachineSettings ms = null;
            var otherUsers = settingsFile == null ? new UserSettings[0] : settingsFile.UsersSettings;
            var machineName = GetMachineName();
            if (otherUsers.Length > 0)
            {
                foreach (var u in otherUsers) //берём настройки текущей машины от первого пользователя, у кого они есть
                {
                    var ums = u.MachinesSettings;
                    if (ums != null && ums.Length > 0)
                    {
                        var curentMachine = ums.FirstOrDefault(s => machineName.Equals(s.MachineName));
                        if (curentMachine != null)
                        {
                            ms = curentMachine;
                            break;
                        }
                    }
                }
            }

            if (ms == null)
            {
                ms = new MachineSettings(settingsFile);
                ms.MachineName = GetMachineName();
            }

            return ms;
        }

        public void InitSettingsFile(MG_Settings settings)
        {
            m_dirs = new SettingsCollectionWrapper(settings, m_lastDirs);
            m_files = new SettingsCollectionWrapper(settings, m_lastFiles);
        }

        public static string GetMachineName()
        {
            return Environment.MachineName;
        }

    }
}
