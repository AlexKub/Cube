using System;
using System.Collections.Generic;
using System.Linq;

namespace RTCManifestGenerator
{
    /// <summary>
    /// Работа с каталогами в файле настроек
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class SettingsCollectionWrapper
    {
        MG_Settings m_settingsFile;
        List<NamedSettingsValue> m_collection;

        /// <summary>
        /// Получение каталога по ключу из Файла настроек
        /// </summary>
        /// <param name="key">Ключ для поиска</param>
        /// <returns>Возвращаеет значение по указанному ключу</returns>
        public string this[string key] { get { return m_collection.First(d => key.Equals(d.Key)).Value; } }

        public SettingsCollectionWrapper(MG_Settings settings, List<NamedSettingsValue> collection)
        {
            m_settingsFile = settings;
            m_collection = collection;
        }

        /// <summary>
        /// Проверка наличия указанного ключа в Настройках
        /// </summary>
        /// <param name="key">Ключ для поиска</param>
        public bool Contains(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new ArgumentNullException("Ключ для поиска не задан");

            return m_collection.Any(d => key.Equals(d.Key));
        }

        /// <summary>
        /// Сохранение каталогов приложения
        /// </summary>
        /// <param name="key">Ключ для данного Пути в Настройках</param>
        /// <param name="value">Значение Пути в Настройках</param>
        /// <param name="saveChanges">Флаг обновления Файла настроек при изменении</param>
        public void Save(string key, string value, bool saveChanges = true)
        {
            Save(new NamedSettingsValue() { Key = key, Value = value }, saveChanges);
        }
        /// <summary>
        /// Сохранение каталогов приложения
        /// </summary>
        /// <param name="setting">Настройка для сохранения</param>
        /// <param name="saveChanges">Флаг обновления Файла настроек при изменении</param>
        public void Save(NamedSettingsValue setting, bool saveChanges = true)
        {
            if (setting == null)
                throw new ArgumentNullException("Передана пустая ссылка на сохраняемый путь приложения");

            if (string.IsNullOrEmpty(setting.Key))
                throw new ArgumentNullException("У переданного для сохранения каталога не задано уникальное имя (Key)");

            var existDir = m_collection.FirstOrDefault(d => setting.Key.Equals(d.Key));

            if (existDir != null)
            {
                //если переданное значение пусто или такое же - НЕ сохраняем
                saveChanges = (existDir.Value != null) && (!existDir.Value.Equals(setting.Value, StringComparison.InvariantCultureIgnoreCase));

                existDir.Value = setting.Value;

            }
            else
                m_collection.Add(setting);

            if (saveChanges)
                m_settingsFile.Save();
        }

        string DebugDisplay()
        {
            return "Count: " + m_collection.Count;
        }
    }

}
