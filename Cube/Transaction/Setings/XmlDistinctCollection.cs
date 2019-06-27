using System;
using System.Linq;
using System.Xml.Serialization;

namespace PJT.Settings
{
    /// <summary>
    /// XML-коллекция уникальных элементов
    /// </summary>
    /// <typeparam name="TElement">Тип элемента коллекции</typeparam>
    [Serializable]
    [XmlInclude(typeof(ProjectCustomFields))]
    [XmlInclude(typeof(JiraLinkTypes))]
    public abstract class XmlDistinctCollection<TElement> : XmlCollection<TElement>
        where TElement : class, IValidated, IEquatable<TElement>
    {
        protected override bool OnElementAdding(TElement element)
        {
            if (element.IsValid)
            {
                if (m_fields.Any(f => element.Equals(f)))
                    m_validationErrors.Add($"Поле '{GetElementLogName(element)}' не валидно. Коллекция признана не валидной.");
                else
                    return true;
            }
            else
            {
                m_validationErrors.Add($"Поле '{GetElementLogName(element)}' не валидно. Коллекция признана не валидной.");
                IsValid = false; //ожидается строгая корректность всех элементов коллекции
            }

            //по умолчанию - не добавляем
            return false;
        }
    }
}