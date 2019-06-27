using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <remarks/>
    [Serializable]
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    [System.ComponentModel.DesignerCategory("code")]
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", ElementName = "RTCManifest", IsNullable = false)]
    [XmlInclude(typeof(ECRManifest))]
    public class RTCManifest : XmlManifest
    {
        private ManifestHeader _header;

        private List<ManifestFileItem> _contentField = new List<ManifestFileItem>();

        /// <summary>
        /// Имя файла Манифеста по умолчанию
        /// </summary>
        public static readonly string DefaultFileName = "RTCManifest.xml";

        /// <summary>
        /// Флаг корневой папки
        /// </summary>
        public static string RootDirectoryFlag = "ROOT";

        /// <summary>
        /// Версия Манифеста
        /// </summary>
        [XmlIgnore]
        public int Version
        {
            get
            {
                if (Header == null)
                    Header = new ManifestHeader();

                return Header.Version;
            }
            set
            {
                if (Header == null)
                    Header = new ManifestHeader();

                Header.Version = value;
            }
        }

        /// <summary>
        /// Заголовок Манифеста
        /// </summary>
        public ManifestHeader Header
        {
            get
            {
                if (_header == null)
                    _header = new ManifestHeader();

                return this._header;
            }
            set
            {
                if (value != null)
                    this._header = value;
            }
        }

        /// <summary>
        /// Список файлов в Манифесте
        /// </summary>
        [XmlArray(ElementName = "Content")]
        [XmlArrayItem("Item", IsNullable = false)]
        public ManifestFileItem[] Files
        {
            get
            {
                return this._contentField.ToArray();
            }
            set
            {
                this._contentField = new List<ManifestFileItem>(value);
            }
        }

        /// <summary>
        /// Загрузка манифеста по умолчанию (DefaultFileName в текущей папке)
        /// </summary>
        /// <returns></returns>
        public static RTCManifest Load()
        {
            return Load(DefaultFileName);
        }

        public static RTCManifest Create(IEnumerable<IManifestFileItem> files = null, string pathName = null)
        {
            RTCManifest manifest = new RTCManifest();

            if (!string.IsNullOrEmpty(pathName))
                manifest.FileInfo = new FileInfo(pathName);

            if (files != null)
                manifest.AddItems(files);

            return manifest;
        }

        /// <summary>
        /// Загрузка манифеста по указанному пути
        /// </summary>
        /// <returns></returns>
        public static RTCManifest Load(string FilePath)
        {
            return Deserialize<RTCManifest>(FilePath, false);
        }

        /// <summary>
        /// Добавление нового файла в манифест
        /// </summary>
        /// <param name="item">Данные о файле</param>
        /// <exception cref="NullReferenceException">При передаче пустой ссылки</exception>
        public void AddItem(IManifestFileItem item)
        {
            if (item == null)
                throw new NullReferenceException("Попытка добавления информации о файле в манифест по пустой ссылке");

            ManifestFileItem newItem = new ManifestFileItem();
            newItem.UpdateBy(item);

            AddItem(newItem);
        }
        public void AddItem(ManifestFileItem item)
        {
            if (item == null)
                return;

            if (_contentField == null)
            {
                _contentField = new List<ManifestFileItem>() { item };
                return;
            }

            _contentField.Add(item);
        }

        /// <summary>
        /// Добавление инфомрации о файлах в манифест
        /// </summary>
        /// <param name="files">Добавляемые файлы</param>
        /// <exception cref="NullReferenceException">При передаче пустой ссылки</exception>
        public void AddItems(IEnumerable<IManifestFileItem> files)
        {
            if (files == null)
                throw new ArgumentNullException("Попытка добавить файлы в манифест по пустой ссылке");

            foreach (var file in files)
                AddItem(file);
        }

        /// <summary>
        /// Удаляет все файлы из Манифеста
        /// </summary>
        public void ClearFiles()
        {
            _contentField.Clear();
        }

        /// <summary>
        /// Количество файлов в Манифесте
        /// </summary>
        /// <returns>Возвращает количество файлов в Манифесте</returns>
        public int FilesCount()
        {
            return _contentField.Count;
        }

        /// <summary>
        /// Удаление элемента из манифесте по точному соответствию
        /// </summary>
        /// <param name="item">Удаляемый элемент</param>
        public void RemoveItem(IManifestFileItem item)
        {
            if (item == null)
                return;

            if (_contentField.Count == 0)
                return;

            ManifestFileItem curent = null;
            for (int i = 0; i < _contentField.Count; i++)
            {
                curent = _contentField[i];

                if (curent != null && curent.Equals(item))
                {
                    _contentField.Remove(curent);
                    break;
                }
            }
        }

        /// <summary>
        /// Замена элемента по точному соответствию. Если соответствия не найдено, вставка НЕ производится
        /// </summary>
        /// <param name="oldItem">Старый элемент</param>
        /// <param name="newItem">Новый элемент</param>
        public void ReplaceItem(ManifestFileItem oldItem, ManifestFileItem newItem)
        {
            if (oldItem == null || newItem == null)
                return;

            if (_contentField.Count == 0)
                return;

            ManifestFileItem curent = null;
            for (int i = 0; i < _contentField.Count; i++)
            {
                curent = _contentField[i];

                if (curent != null && curent.Equals(oldItem))
                {
                    _contentField[i] = newItem;
                    break;
                }
            }
        }

        /// <summary>
        /// Замена параметров для первого файла с таким же именем в манифесте
        /// </summary>
        /// <param name="item">Файл с новыми параметрами</param>
        public void UpdateItemBy(IManifestFileItem item)
        {
            if (item == null || item.FileName != null)
                return;

            foreach (var i in _contentField)
                if (item.FileName.Equals(i.FileName))
                {
                    i.UpdateBy(item);
                    break;
                }
        }

        public bool HasFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return false;

            if (_contentField.Count == 0)
                return false;

            foreach (var file in Files)
            {
                if (fileName.Equals(file.FileName, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Проверка наличия элемента с такими же значениями в Манифесте
        /// </summary>
        /// <param name="item">Проверяемый элемент</param>
        /// <returns>Возвращает флаг наличия в манифесте элемента с такими же значениями</returns>
        public bool Contains(IManifestFileItem item)
        {
            if (item == null)
                return false;

            foreach (var curItem in _contentField)
                if (item.Equals(curItem))
                    return true;

            return false;
        }

        /// <summary>
        /// Проверка на наличие файлов в Манифесте
        /// </summary>
        /// <returns>Возвращает флаг наличия файлов</returns>
        public bool IsEmpty()
        {
            var files = Files;

            if (files != null)
                if (files.Length > 0)
                    return false;

            return true;
        }

        /// <summary>
        /// Получение информации о файле по имени
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>Возвращает информацию о соответствующем файле в манифесте или null, если соответствий не найдено</returns>
        public ManifestFileItem GetItemByFileName(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;

            if (_contentField.Count == 0)
                return null;

            foreach (var file in Files)
            {
                if (fileName.Equals(file.FileName, StringComparison.InvariantCultureIgnoreCase))
                    return file;
            }

            return null;
        }

        /// <summary>
        /// Получение списка всех библиотек (*.dll) в манифесте
        /// </summary>
        /// <returns>Возвращает список всех dll в Манифесте</returns>
        public IEnumerable<ManifestFileItem> GetAssemblies()
        {
            var result = new List<ManifestFileItem>();
            if (Files == null || Files.Length == 0)
                return result;

            try
            {
                foreach (var file in Files)
                    if (file.IsAssembly)
                        result.Add(file);

                return result;
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при запросе регистрируемых библиотек из манифеста ECR", ex);
                return result;
            }
        }

        /// <summary>
        /// Обновить текущий манифест указанным
        /// </summary>
        /// <param name="manifest">Манифест, которым обновляется текущим</param>
        public void UpdateBy(RTCManifest manifest)
        {
            if (manifest == null)
                return;

            if (manifest.Files != null)
                this.UpdateBy(manifest.Files);
        }

        /// <summary>
        /// Обновление манифеста набором файлов. Новые будут добавлены, старые обновлены.
        /// </summary>
        /// <param name="newItems">Новые данные для обновления</param>
        public void UpdateBy(IEnumerable<IManifestFileItem> newItems)
        {
            if (newItems == null)
                return;

            ManifestFileItem curentItem = null;

            if (FilesCount() == 0)
                AddItems(newItems);
            else
                foreach (var newItem in newItems)
                {
                    curentItem = GetItemByFileName(newItem.FileName);
                    if (curentItem != null)
                        curentItem.UpdateBy(newItem);
                    else
                        this.AddItem(newItem);
                }
        }


        protected override XmlManifest GetDefault()
        {
            var defaultManifest = new RTCManifest();
            defaultManifest.Header = new ManifestHeader();
            defaultManifest.Header.Version = 0;
            defaultManifest.Files = new ManifestFileItem[0];

            return defaultManifest;
        }

        protected override string GetDefaultFileName()
        {
            return DefaultFileName;
        }

        /// <summary>
        /// Сохраняет Манифест по указанному пути
        /// </summary>
        /// <param name="pathName">Имя манифеста. По умолчанию берётся из FileInfo</param>
        /// <exception cref="ArgumentNullException">Если не задано имя файла и не инициализирвоан FileInfo</exception>
        public override void Save(string pathName = null)
        {
            if (string.IsNullOrEmpty(pathName))
                if (FileInfo != null)
                    pathName = FileInfo.FullName;
                else
                    pathName = DefaultFileName;

            Serialize(this, pathName);
        }

        string DebugDisplay()
        {
            return "Version: " + Version.ToString() + " | Count: " + (Files == null ? "null" : Files.Length.ToString());
        }
    }


}