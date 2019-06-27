using System;
using System.Xml.Serialization;
using Merlion.ECR.Update.Core_2_0;
using System.Collections.Generic;
using Merlion.ECR.Update.Core.Log;

namespace Merlion.ECR.Update.Core.Manifest
{
    /// <summary>
    /// Элемент Манифеста
    /// </summary>
    [Serializable]
    //[XmlType(AnonymousType = true)]
    [System.ComponentModel.DesignerCategory("code")]
    //[XmlInclude(typeof(ControlAddInFileItem))]
    [System.Diagnostics.DebuggerDisplay("{DebugDisplay()}")]
    public class ManifestFileItem : IManifestFileItem
    {
        private static readonly LogSet m_loger = LogManager.GetDefaultLogSet<ManifestFileItem>();

        private string fileNameField;

        private string[] foldersField;

        private string versionField;

        private RegInfo m_gac = new RegInfo();
        private RegInfo m_regasm = new RegInfo();
        private RegInfo m_regsvr32 = new RegInfo();

        /// <summary>
        /// Флаг для не изменяемых (при обновлении) файлов
        /// </summary>
        [XmlAttribute]
        public bool DoNotUpdate { get; set; } = false; //STR8245

        /// <summary>
        /// Тип элемента
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName
        {
            get
            {
                return this.fileNameField;
            }
            set
            {

                this.fileNameField = Utilites.Clean_CRLF(value + string.Empty).Trim();
            }
        }

        /// <summary>
        /// Список папок для размещения
        /// </summary>
        [XmlArrayItem("Folder", IsNullable = false)]
        public string[] Folders
        {
            get
            {
                return this.foldersField;
            }
            set
            {
                if (value != null)
                {
                    for (int i = 0; i < value.Length; i++)
                    {
                        value[i] = Utilites.Clean_CRLF(value[i] + string.Empty).Trim();
                    }
                }

                this.foldersField = value;
            }
        }

        /// <summary>
        /// Возвращает или задаёт Контрольную сумму файла
        /// </summary>
        public string CRC { get; set; }

        /// <summary>
        /// Возвращает или задаёт версию файла
        /// </summary>
        public string Version { get { return this.versionField; } set { this.versionField = value; } }

        /// <summary>
        /// Флаг регистрации в GAC
        /// </summary>
        public RegInfo GAC { get { return m_gac; } set { if (value == null) m_gac = new RegInfo(); else m_gac = value; } }

        /// <summary>
        /// Флаг регистрации через RegAsm
        /// </summary>
        public RegInfo REGASM { get { return m_regasm; } set { if (value == null) m_regasm = new RegInfo(); else m_regasm = value; } }

        /// <summary>
        /// Флаг регистрции через RegSvr32
        /// </summary>
        public RegInfo REGSRV32 { get { return m_regsvr32; } set { if (value == null) m_regsvr32 = new RegInfo(); else m_regsvr32 = value; } }

        /// <summary>
        /// Флаг удаления файла
        /// </summary>
        public bool Delete { get; set; }

        /// <summary>
        /// Токен публичного ключа
        /// </summary>
        public string PublicKeyToken { get; set; }

        /// <summary>
        /// Имя ControlAddIn'а для NAV
        /// </summary>
        public string AddinName { get; set; }

        /// <summary>
        /// Флаг соответствия расширения файла *.dll
        /// </summary>
        [XmlIgnore]
        public bool IsAssembly { get { return Utilites.IsAssemblyFile(fileNameField); } }

        #region IManifestFileItem

        IRegInfo IManifestFileItem.GAC { get { return GAC; } }

        IRegInfo IManifestFileItem.REGASM { get { return REGASM; } }

        IRegInfo IManifestFileItem.REGSRV32 { get { return REGSRV32; } }

        #endregion

        public void AddFolder(string name)
        {
            if (Folders == null)
                Folders = new string[] { Utilites.Clean_CRLF(name + string.Empty).Trim() };
            else
            {
                var array = Folders;
                Array.Resize(ref array, array.Length + 1);
                array[array.Length - 1] = name;

                Folders = array;
            }
        }

        /// <summary>
        /// Удаление соответствующего каталога установки
        /// </summary>
        /// <param name="name">Имя каталога установки</param>
        public void RemoveFolder(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                m_loger.Log("На удаление каталога файла в манифесте передана пустая ссылка на имя каталога. Удаление прервано", MessageType.Warning
                    , new LogParameter("Имя файла в манифесте", LogMessageBuilder.GetStringLogVal(FileName)));
                return;
            }
            if (Folders == null)
                return;
            else
            {
                var folders = new List<string>(Folders);

                var folder = string.Empty;
                for (int i = 0; i < Folders.Length; i++)
                {
                    folder = Folders[i];

                    if (folder == null)
                        continue;

                    if (folder.ToLower().Equals(name.ToLower()))
                        folders.Remove(folder);
                }

                Folders = folders.ToArray();
            }
        }
        /// <summary>
        /// Очистка папок
        /// </summary>
        public void ClearFolders()
        {
            foldersField = new string[0];
        }

        /// <summary>
        /// Проверка наличия каталога с указаннным имененм среди текущих
        /// </summary>
        /// <param name="folder">Имя каталога для поиска</param>
        /// <returns>Возвращает флаг наличия соответствующего каталога</returns>
        public bool ContainsFolder(string folder)
        {
            if (string.IsNullOrEmpty(folder))
            {
                m_loger.Log("На проверку наличия каталога файла в манифесте передана пустая ссылка на имя каталога. Действие прервано", MessageType.Warning
                    , new LogParameter("Имя файла в манифесте", LogMessageBuilder.GetStringLogVal(FileName)));
                return false;
            }
            if (Folders == null || Folders.Length == 0)
                return false;

            var curentFolder = string.Empty;
            for (int i = 0; i < Folders.Length; i++)
            {
                curentFolder = Folders[i];
                if (curentFolder != null)
                    if (folder.ToLower().Equals(folder.ToLower()))
                        return true;
            }

            return false;
        }

        /// <summary>
        /// Обновление текущего экземпляра значениями из переданного (принимаются только экземпляры своего типа)
        /// </summary>
        /// <param name="newItemValues">Новые значения для текущего экземпляра</param>
        public virtual void UpdateBy(IManifestFileItem newItemValues)
        {
            if (newItemValues == null)
                return;

            this.FileName = newItemValues.FileName;
            this.Folders = newItemValues.Folders;
            this.Version = newItemValues.Version;
            this.CRC = newItemValues.CRC;
            this.Type = newItemValues.Type;
            this.GAC = new RegInfo(newItemValues.GAC);
            this.REGASM = new RegInfo(newItemValues.REGASM);
            this.REGSRV32 = new RegInfo(newItemValues.REGSRV32);
            this.PublicKeyToken = newItemValues.PublicKeyToken;
            this.Delete = newItemValues.Delete;
            this.AddinName = newItemValues.AddinName;
            this.DoNotUpdate = newItemValues.DoNotUpdate;
        }

        /// <summary>
        /// Сравнение всех свойств элемента на соответствие
        /// </summary>
        /// <param name="item">С кем сравниваем</param>
        /// <returns>Возвращает true, если все свойства у обоих экземпляров равны</returns>
        public virtual bool Equals(IManifestFileItem item)
        {
            if (item == null)
                return false;

            if (item == this)
                return true;

            //сначала сравниваем булевые значения
            //их проще сравнивать
            return (item.Delete == this.Delete
                && item.GAC == (IRegInfo)this.GAC
                && item.REGASM == (IRegInfo)this.REGASM
                && item.REGSRV32 == (IRegInfo)this.REGSRV32
                && item.DoNotUpdate == this.DoNotUpdate
                //строчечки
                && (string.Compare(item.Type, this.Type, true) == 0)
                && (string.Compare(item.FileName, this.fileNameField, true) == 0)
                && (string.Compare(item.Version, this.Version, true) == 0)
                && (string.Compare(item.CRC, this.CRC, true) == 0)
                && (string.Compare(item.AddinName, this.AddinName, true) == 0)
                && (string.Compare(item.PublicKeyToken, this.PublicKeyToken, true) == 0)
                //и, в конце, массивчик
                && (HasEqualsFolders(item)));
        }

        /// <summary>
        /// Проверка на соответствие папок в элементах Манифеста
        /// </summary>
        /// <param name="item">Сравниваемый элемент</param>
        /// <returns>Возвращает флаг соответствия папок в Манифестах</returns>
        public bool HasEqualsFolders(IManifestFileItem item)
        {
            if (item == null)
                return false;

            if (item.Folders == null || this.Folders == null)
                return false;

            if (item.Folders.Length == this.Folders.Length)
            {
                //приводим все имена папок в один регистр
                var eThisFolders = new List<string>();
                foreach (var folder in this.Folders)
                    eThisFolders.Add(folder.ToLower());

                var eItemFolders = new List<string>();
                foreach (var folder in item.Folders)
                    eItemFolders.Add(folder.ToLower());

                foreach (var thisFolder in this.Folders)
                {
                    if (!eItemFolders.Contains(thisFolder))
                        return false;
                }

                return true;
            }

            return false;
        }

        string DebugDisplay()
        {
            return string.Concat(FileName, " | ", Version, " | ", Delete ? "Delete" : "Add");
        }

        public ManifestFileItem Clone()
        {
            var newItem = new ManifestFileItem();
            newItem.UpdateBy(this);

            return newItem;
        }
    }
}
