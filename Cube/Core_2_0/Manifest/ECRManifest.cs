using System;
using System.Xml.Serialization;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Общий манифест (в разработке)
    /// </summary>
    [Serializable]
    [XmlRoot(Namespace = "", ElementName = "ECRManifest", IsNullable = false)]
    public class ECRManifest : RTCManifest
    {
        /// <summary>
        /// Имя файла манифеста по умолчанию
        /// </summary>
        public static readonly new string DefaultFileName = "ECRManifest.xml";

        public new static ECRManifest Load() { return Deserialize<ECRManifest>(ECRManifest.DefaultFileName); }

        public new static ECRManifest Load(string pathName) { return Deserialize<ECRManifest>(pathName); }

        /// <summary>
        /// Сохраняет Манифест по указанному пути
        /// </summary>
        /// <param name="pathName">Имя манифеста. По умолчанию берётся из FileInfo</param>
        /// <exception cref="ArgumentNullException">Если не задано имя файла и не инициализирвоан FileInfo</exception>
        public new void Save(string pathName = null)
        {
            if (string.IsNullOrEmpty(pathName))
                if (FileInfo != null)
                    pathName = FileInfo.FullName;
                else
                    pathName = DefaultFileName;

            Serialize(this, pathName);
        }

        protected override XmlManifest GetDefault()
        {
            var defaultManifest = new ECRManifest();
            defaultManifest.Header = new ManifestHeader();
            defaultManifest.Header.Version = 0;
            defaultManifest.Files = new ManifestFileItem[0];

            return defaultManifest;
        }

        protected override string GetDefaultFileName()
        {
            return DefaultFileName;
        }
    }
}
