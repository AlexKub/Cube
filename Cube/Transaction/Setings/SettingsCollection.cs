using System;
using System.Linq;
using System.Xml.Serialization;
using PJT.Settings;
using PJT.Settings.Mapping;
using PJT.Transactions;

namespace PJT
{
    /// <summary>
    /// Набор настроек
    /// </summary>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class SettingsCollection
    {
        /// <summary>
        /// ID текущей коллекции
        /// </summary>
        [XmlAttribute]
        public string ID { get; set; }

        /// <summary>
        /// Описание текущей коллекции
        /// </summary>
        [XmlAttribute]
        public string Description { get; set; }

        /// <summary>
        /// Настройки JIRA
        /// </summary>
        public Jira JIRA { get; set; } = new Jira();

        /// <summary>
        /// Настройки Project
        /// </summary>
        public Project Project { get; set; } = new Settings.Project();

        /// <summary>
        /// Наборы соответствий
        /// </summary>
        public Mappings Mappings { get; set; } = new Mappings();

        string DebugDisplay()
        {
            return $"{ID.LogValue()} | {Description.LogValue()}";
        }

        /// <summary>
        /// Получение линка соответствия
        /// </summary>
        /// <param name="proj_id">ID в Project</param>
        /// <param name="mappingPropertyName">Имя свойства коллекции в Mappings</param>
        /// <param name="errorsCollection">Сбор настроек, если что не так</param>
        /// <returns>Возвращает линк по ID или null</returns>
        public GuidLink GetGuidLink(Guid proj_id, string mappingPropertyName, IErrorsCollection errorsCollection)
        {
            //получение распарсенного набора линков
            var links = Mappings?.GetGuidLinksCollection(mappingPropertyName, errorsCollection);

            if (links == null)
            {
                errorsCollection.AddError($"Не удалось получить узел '{mappingPropertyName.LogValue()}' из файла настроек");
                return null;
            }

            //ищем в настройках сопоставление
            var link = links.FirstOrDefault(c => proj_id.Equals(c.SP_ID_Guid));

            if (link == null)
            {
                errorsCollection.AddError($"Не удалось найти соответствия в узле '{mappingPropertyName.LogValue()}' по ключу '{proj_id.ToString()}' в файле настроек");
                return null;
            }

            if (!link.IsValid)
            {
                errorsCollection.AddError($"Не удалось распознать соответствие в узле '{mappingPropertyName.LogValue()}', найденном по ключу '{proj_id.ToString()}' в файле настроек"
                    , parameters: new ErrorParameter[] {
                        new ErrorParameter("SP_ID", link.SP_ID.ToString()),
                        new ErrorParameter("J_ID", link.J_ID.ToString())
                    });
                return null;
            }

            //возвращаем связку
            return link;
        }
    }
}