using System;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Ошибка сериализации манифеста
    /// </summary>
    public class XmlSerializationException : Exception
    {
        /// <summary>
        /// Тип, на котором была проблема с сериализацией
        /// </summary>
        public Type NonSerialized { get; private set; }

        /// <summary>
        /// Новое исключение при сериализации
        /// </summary>
        /// <param name="description">Описание исключения</param>
        /// <param name="serializeType">Проблемный тип при сериализации</param>
        public XmlSerializationException(string description, Type serializeType) : base(description)
        {
            NonSerialized = serializeType;
        }
    }
}
