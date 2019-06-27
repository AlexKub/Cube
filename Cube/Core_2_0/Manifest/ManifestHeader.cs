namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Заголовок Манифеста
    /// </summary>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public class ManifestHeader
    {
        public int Version { get; set; }
    }
}
