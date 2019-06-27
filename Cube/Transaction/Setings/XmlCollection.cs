using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace PJT.Settings
{
    /// <summary>
    /// Коллекция XML-элементов
    /// </summary>
    /// <typeparam name="TElement">Тип элемента в коллекции</typeparam>
    [Serializable]
    [XmlInclude(typeof(XmlDistinctCollection<>))]
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public abstract class XmlCollection<TElement> : IReadOnlyCollection<TElement>, IValidated
        where TElement : class
    {
        /// <summary>
        /// Набор элементов коллекции
        /// </summary>
        protected readonly List<TElement> m_fields = new List<TElement>();
        /// <summary>
        /// Описание ошибок валидации
        /// </summary>
        protected readonly List<string> m_validationErrors = new List<string>();

        /// <summary>
        /// Флаг корректного парсинга
        /// </summary>
        [XmlIgnore]
        public virtual bool IsValid { get; protected set; }

        /// <summary>
        /// Общий метод приведения значения от общего типа к частному
        /// </summary>
        /// <typeparam name="TValue">Целевой тип</typeparam>
        /// <param name="field">Приводимое значение</param>
        /// <returns>Возвращает значение в целевом типе или null</returns>
        protected TValue ConvertField<TValue>(TElement field)
            where TValue : class, TElement
        {
            var converted = field as TValue;

            if (converted == null)
            {
                m_validationErrors.Add($"Не удалось привести поле '{GetElementLogName(field)}' к типу '{typeof(TValue).Name}'.");
                IsValid = false;
            }

            return converted;
        }

        /// <summary>
        /// Получение узнаваемого имени элемента для логирования
        /// </summary>
        /// <param name="xmlElement">Логируемый элемент</param>
        /// <returns>Возвращает имя элемента для логирования</returns>
        protected abstract string GetElementLogName(TElement xmlElement);

        /// <summary>
        /// При десериализации элемента
        /// </summary>
        /// <param name="xmlElement">Десериализованный элемент</param>
        /// <remarks>После добавления элемента в коллекцию</remarks>
        protected abstract void OnElementAdded(TElement xmlElement);

        /// <summary>
        /// Получение набора ошибок, возникших при валидации
        /// </summary>
        /// <returns>Возвращаеет набор проблем при валидации</returns>
        public IReadOnlyList<string> GetValidationErrors()
        {
            return m_validationErrors;
        }

        [Obsolete("Используется при XML-десериализации")]
        public void Add(object value)
        {
            var field = value as TElement;

            if (field == null)
            {
                m_validationErrors.Add($"Не удалось привести значение к типу '{typeof(TElement).Name}' при десериализации элемента xml-коллекции.\r\nТип значения: '{(value == null ? "NULL" : value.GetType().FullName)}'");
                IsValid = false;
                return;
            }

            bool shoudAdd = OnElementAdding(field);

            if(shoudAdd)
            {
                m_fields.Add(field);

                OnElementAdded(field);
            }

        }

        protected abstract bool OnElementAdding(TElement element);

        #region IReadOnlyCollection<JiraField>

        public int Count => m_fields.Count;

        public IEnumerator<TElement> GetEnumerator()
        {
            return m_fields.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_fields.GetEnumerator();
        }

        #endregion

        string DebugDisplay()
        {
            return "Count: " + m_fields.Count.ToString() + (IsValid ? string.Empty : " | NotValid");
        }

    }
}