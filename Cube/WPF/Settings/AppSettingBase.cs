using System.Xml;

namespace RTCManifestGenerator.Models
{
    /// <summary>
    /// Узел настроек в файле config'а
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public abstract class AppSettingBase
    {
        /// <summary>
        /// Имя тега у настроек 'add'
        /// </summary>
        public const string TagName = "add";
        /// <summary>
        /// Имя аттрибута 'key'
        /// </summary>
        public const string KeyAttributeName = "key";
        /// <summary>
        /// Имя аттрибута 'value'
        /// </summary>
        public const string ValueAttributeName = "value";

        /// <summary>
        /// Ключ в файле настроек
        /// </summary>
        public string Key { get; private set; }

        /// <summary>
        /// Строковое значение
        /// </summary>
        public string StringValue { get; protected set; }

        /// <summary>
        /// Узел настроек в файле *.config
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        public AppSettingBase(string key)
        {
            Key = key;
        }

        /// <summary>
        /// Чтение значения из узла appSettings
        /// </summary>
        /// <param name="appSetings">Узел appSettings</param>
        public abstract void Read(XmlNode appSetings);

        /// <summary>
        /// Запись текущего значения
        /// </summary>
        /// <param name="appSetings">Узел appSettings</param>
        public abstract void Write(XmlNode appSetings);

        string DebugDisplay()
        {
            return Key + " | " + StringValue;
        }
    }

    /// <summary>
    /// Узел настроек в файле config'а
    /// </summary>
    /// <typeparam name="T">Тип значения</typeparam>
    public abstract class AppSettingBase<T> : AppSettingBase
    {

        public AppSettingBase(string key) : base(key)
        {

        }

        T m_value;
        /// <summary>
        /// Значение, привидённое к указанному типу
        /// </summary>
        public T Value
        {
            get { return m_value; }
            set
            {
                if(!(m_value?.Equals(value) ?? false))
                {
                    m_value = value;
                    StringValue = ConvertToString(value);
                }
            }
        }

        /// <summary>
        /// Преобразование из строки в указанный тип
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>Возвращает указанный тип</returns>
        public abstract T ConvertFrom(string value);

        /// <summary>
        /// Преобразование к строке
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>Возвращает строковое представление для записи в файл</returns>
        public abstract string ConvertToString(T value);

        /// <summary>
        /// Значение по умолчанию
        /// </summary>
        /// <returns>Возвращает значение по умолчанию для данного типа</returns>
        protected abstract T GetDefaultValue();

        /// <summary>
        /// Чтение значения из узла appSettings
        /// </summary>
        /// <param name="appSetings">Узел appSettings</param>
        public sealed override void Read(XmlNode appSetings)
        {
            var xPath = $"{TagName}[@{KeyAttributeName}='{Key}']";

            var node = appSetings.SelectSingleNode(xPath);

            Value = node == null
                ? GetDefaultValue()
                : ConvertFrom(node.Attributes[ValueAttributeName].Value);
        }

        /// <summary>
        /// Запись текущего значения
        /// </summary>
        /// <param name="appSetings">Узел appSettings</param>
        public sealed override void Write(XmlNode appSetings)
        {
            var xPath = $"{TagName}[@{KeyAttributeName}='{Key}']";

            var node = appSetings.SelectSingleNode(xPath);

            if (node != null)
                node.Attributes[ValueAttributeName].Value = StringValue;
        }
    }
}
