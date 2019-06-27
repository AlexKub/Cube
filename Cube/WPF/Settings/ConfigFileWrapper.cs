using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace RTCManifestGenerator.Models.Settings
{
    /// <summary>
    /// Обёртка над файлом настроек *.config
    /// </summary>
    public abstract class ConfigFileWrapper
    {
        readonly List<AppSettingBase> m_appSettings = new List<AppSettingBase>();

        /// <summary>
        /// Информация о файле
        /// </summary>
        public FileInfo FileInfo { get; private set; }

        /// <summary>
        /// Узел 'appSettings'
        /// </summary>
        public const string AppSettingsTag = "appSettings";

        /// <summary>
        /// Узел 'configuration'
        /// </summary>
        public const string ConfigurationTag = "configuration";

        /// <summary>
        /// Считанные параметры из AppSettings
        /// </summary>
        public IReadOnlyList<AppSettingBase> AppSettings { get { return m_appSettings; } }

        protected static T Load<T>(string pathName, bool throwEx = false)
            where T : ConfigFileWrapper, new()
        {
            var wrapper = new T();

            try
            {
                wrapper.FileInfo = new FileInfo(pathName);

                var xml = new XmlDocument();
                xml.Load(pathName);

                var appSettings = wrapper.GetAppSettings(xml);

                if (appSettings != null)
                {
                    var settings = wrapper.ParseAppSettings(appSettings);

                    if (settings != null)
                        wrapper.m_appSettings.AddRange(settings);
                }
            }
            catch (Exception ex)
            {
                if (throwEx)
                    throw;
            }

            return wrapper;
        }

        /// <summary>
        /// Парсинг узла настроек
        /// </summary>
        /// <param name="node">Узел appSettings</param>
        /// <returns>Возвращает набор распарсенных компонент</returns>
        protected abstract IEnumerable<AppSettingBase> ParseAppSettings(XmlNode node);

        /// <summary>
        /// Логика поиска узла 'appSettings' в xml
        /// </summary>
        /// <param name="xml">Считанный xml</param>
        /// <returns>Возвращает распарсенный узел 'sppSettings' или null</returns>
        protected virtual XmlNode GetAppSettings(XmlDocument xml)
        {
            return xml.DocumentElement[AppSettingsTag];
        }

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        public void Save()
        {
            SaveAs(FileInfo);
        }

        /// <summary>
        /// Сохранить как
        /// </summary>
        /// <param name="file">Информация о сохраняемом файле</param>
        public void SaveAs(FileInfo file)
        {
            try
            {
                if (file == null)
                    throw new ArgumentNullException("Информация о файле не задана");

                var dir = Path.GetDirectoryName(file.FullName);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                XmlDocument xml = null;
                if (!file.Exists)
                {
                    xml = CreateXmlConfig();
                }
                else
                {
                    //перечитываем схему, на случай если в файле чего-то поменялось
                    xml = new XmlDocument();
                    xml.Load(file.FullName);
                }

                var appSettings = GetAppSettings(xml);

                foreach (var item in m_appSettings)
                {
                    item.Write(appSettings);
                }

                xml.Save(file.FullName);
            }
            catch (Exception ex)
            {

            }
        }

        XmlDocument CreateXmlConfig()
        {
            XmlDocument xml = new XmlDocument();
            xml.CreateXmlDeclaration("1.0", "UTF-8", null);
            var config = xml.CreateNode(XmlNodeType.Element, ConfigurationTag, string.Empty);
            var appSettings = xml.CreateNode(XmlNodeType.Element, AppSettingsTag, string.Empty);
            config.AppendChild(appSettings);

            foreach (var item in m_appSettings)
            {
                var setting = CreateSettingNode(xml, item.Key);

                appSettings.AppendChild(setting);
            }

            return xml;
        }

        XmlNode CreateSettingNode(XmlDocument xml, string key)
        {
            var setting = xml.CreateNode(XmlNodeType.Element, AppSettingBase.TagName, string.Empty);

            var attr = xml.CreateAttribute(AppSettingBase.KeyAttributeName);
            attr.Value = key;

            setting.Attributes.Append(attr);

            attr = xml.CreateAttribute(AppSettingBase.ValueAttributeName);
            attr.Value = string.Empty;

            setting.Attributes.Append(attr);

            return setting;
        }
    }

    /// <summary>
    /// Обёртка над файлом настроек *.config
    /// </summary>
    /// <typeparam name="T">Тип текущего наследника</typeparam>
    public abstract class ConfigFileWrapper<T>
        where T : ConfigFileWrapper
    {

    }
}
