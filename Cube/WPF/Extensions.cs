using Merlion.ECR.Update.Core;
using Merlion.ECR.Update.Core.Log;
using Merlion.ECR.Update.Core.Manifest;
using Merlion.ECR.Update.Core.SeparatedAssemblyLoad;
using Merlion.ECR.Update.Core.Win32_API;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RTCManifestGenerator
{
    public static class Extensions
    {
        static LogSet m_loger = StaticMethods.GetFileLogSet(nameof(Extensions));

        public static string ToStringSplitedBy(this IEnumerable<string> values, string separator)

        {
            StringBuilder sb = new StringBuilder();
            foreach (var val in values)
                sb.Append(val).Append(separator);

            return sb.ToString().Trim();

        }

        public static RTCManifest SelectFromFile()
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Multiselect = false;
                ofd.Title = "Выберите файл Манифеста";
                ofd.CheckFileExists = true;
                ofd.Filter = "XML | *.xml";

                if (!string.IsNullOrEmpty(Properties.Settings.Default.LoadManifestPath))
                {
                    ofd.InitialDirectory = Properties.Settings.Default.LoadManifestPath;
                }

                if (ofd.ShowDialog() != true)
                    return null;

                var pathName = ofd.FileName;

                RTCManifest manifest = null;
                switch (Path.GetFileName(pathName).ToLower())
                {
                    case "rtcmanifest.xml":
                        manifest = RTCManifest.Load(pathName);
                        break;
                    case "ecrmanifest.xml":
                        manifest = ECRManifest.Load(pathName);
                        break;
                    default:
                        manifest = RTCManifest.Load(pathName);
                        //MessageBox.Show("Не удалось определить тип манифеста");
                        break;
                }

                return manifest;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Возникло исключение при загрузке файла манифеста: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Простановка соответствующего параметра в RTCManifest
        /// </summary>
        /// <param name="item">Файл в Манифесте</param>
        /// <param name="parameter">Значение параметра</param>
        public static void ApplyParameter(this ManifestFileItem item, Models.MergeItemValue parameter)
        {
            if (parameter == null)
                m_loger.Log("При простановке параметра в одном из файлов манифеста, передана пустая ссылка на Параметр"
                    , MessageType.Warning
                    , new LogParameter("Имя файла манифеста", LogMessageBuilder.GetStringLogVal(item.FileName)));
            else
                switch (parameter.ParameterType)
                {
                    case ManifestFileParameters.Name:
                        var name = parameter.GetValue() as string;
                        if (name != null)
                            item.FileName = name;
                        else
                        {
                            m_loger.Log("Не удалось изменить имя файла в манифесте - значение параметра не определено", MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.CRC:
                        var crc = parameter.GetValue() as string;
                        if (crc != null)
                            item.CRC = crc;
                        else
                        {
                            m_loger.Log("Не удалось изменить контрольную сумму в манифесте - значение параметра не определено", MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.Folder:
                        var folder = parameter.GetValue() as string;
                        if (folder != null)
                        {
                            MergeState state = parameter.MergeState;
                            //parameter.Dispatcher.Invoke(() => { state = parameter.MergeState; });
                            switch (state)
                            {
                                case MergeState.Added:
                                    item.AddFolder(folder);
                                    break;
                                case MergeState.Changed:
                                    m_loger.Log("Логика простановки значения для изменённых папок не определена. Изменение пропущено", MessageType.Warning
                                        , new LogParameter("Имя редактируемого файла в манифесте", LogMessageBuilder.GetStringLogVal(item.FileName)));
                                    return;
                                case MergeState.Removed:
                                    if (item.ContainsFolder(folder))
                                        item.RemoveFolder(folder);
                                    else
                                        m_loger.Log("Помеченная на удаление папка не найдена в манифесте", MessageType.Warning
                                        , new LogParameter("Имя редактируемого файла в манифесте", LogMessageBuilder.GetStringLogVal(item.FileName))
                                        , new LogParameter("Имя искомого каталога", LogMessageBuilder.GetStringLogVal(folder)));
                                    break;
                                default:
                                    return;
                            }
                        }
                        else
                        {
                            m_loger.Log("Не удалось изменить контрольную сумму в манифесте - значение параметра не определено", MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.RegAsm:
                        try
                        {
                            item.REGASM.StringAction = parameter.GetValue() as string;
                        }
                        catch (Exception ex)
                        {
                            m_loger.Log("Не удалось изменить флаг регистрации RegAsm в манифесте - значение параметра не определено", ex, MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.RegGAC:
                        try
                        {
                            item.GAC.StringAction = parameter.GetValue() as string;
                        }
                        catch (Exception ex)
                        {
                            m_loger.Log("Не удалось изменить флаг регистрации RegGAC в манифесте - значение параметра не определено", ex, MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.RegSrv32:
                        try
                        {
                            item.REGSRV32.StringAction = parameter.GetValue() as string;
                        }
                        catch (Exception ex)
                        {
                            m_loger.Log("Не удалось изменить флаг регистрации RegSvr32 в манифесте - значение параметра не определено", ex, MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.Type:
                        var type = parameter.GetValue() as string;
                        if (type != null)
                            item.Type = type;
                        else
                        {
                            m_loger.Log("Не удалось изменить тип файла в манифесте - значение параметра не определено", MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.Version:
                        var version = parameter.GetValue() as string;
                        if (version != null)
                            item.Version = version;
                        else
                        {
                            m_loger.Log("Не удалось изменить версию файла в манифесте - значение параметра не определено", MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;
                    case ManifestFileParameters.Delete:
                        try
                        {
                            bool flag = (bool)parameter.GetValue();
                            item.Delete = flag;
                        }
                        catch (Exception ex)
                        {
                            m_loger.Log("Не удалось изменить флаг удаления файла в манифесте - значение параметра не определено", ex, MessageType.Warning
                                , new LogParameter("Тип значения", parameter.GetValue() == null ? "null" : parameter.GetValue().GetType().FullName));
                        }
                        break;

                    case ManifestFileParameters.PublicKeyToken:
                        var pkt = parameter.GetValue() as string;
                        item.PublicKeyToken = pkt == null ? string.Empty : pkt;

                        break;
                    case ManifestFileParameters.AddinName:
                        var addinName = parameter.GetValue() as string;
                        item.AddinName = addinName == null ? string.Empty : addinName;

                        break;
                    default:
                        m_loger.Log("Не удалось проставить параметр в файле манифеста - тип параметра не определён", MessageType.Warning
                            , new LogParameter("Тип", parameter.ParameterType.ToString()));
                        break;


                }
        }

        /// <summary>
        /// Асинхронное получение набора моделей ManifestFileGridItem из файлов Манифеста
        /// </summary>
        /// <param name="manifest">Манифест</param>
        /// <returns>Возвращает запущенную асинхронную задачу, возвращающую набор моделей для Grid'а</returns>
        public static Task<IEnumerable<ManifestFileGridItem>> GetGridFiles(this RTCManifest manifest)
        {
            return Task.Run<IEnumerable<ManifestFileGridItem>>(() =>
            {

                var result = new List<ManifestFileGridItem>();
                if (manifest.FilesCount() == 0)
                    return result;

                ManifestFileGridItem viewItem = null;

                var root = manifest.FileInfo == null ? "" : manifest.FileInfo.Directory.FullName;
                var directory = root;
                var folder = string.Empty;
                using(var domain = SeparatedAppDomain.Create())
                {
                    foreach (var file in manifest.Files)
                    {
                        folder = file.Folders.First();

                        if (folder.Equals(RTCManifest.RootDirectoryFlag))
                            directory = root;
                        else
                        {
                            //формируем каталог как указан в манифесте
                            directory = Path.Combine(root, folder);

                            //если папки нет - пробуем загрузить файл из текущего расположения файла манифеста
                            if (!Directory.Exists(directory))
                                directory = root;
                        }

                        var filePath = Path.Combine(directory, file.FileName);

                        viewItem = new ManifestFileGridItem(file, filePath, domain);

                        result.Add(viewItem);
                    }
                }

                return result;
            });
        }

        /// <summary>
        /// Получение каталога по информации о файле
        /// </summary>
        /// <param name="item">Элемент манифеста</param>
        /// <returns>Возвращает значимую папку файла или ROOT</returns>
        public static string GetFoldersString(this ManifestFileItem item)
        {
            var folders = item.Folders;

            if (folders != null && folders.Length > 0)
            {
                string separator = ItemFoldersManager.FoldersSpliter + " "; // '; '
                return string.Join(separator, folders);
            }
            else
                return string.Empty;
        }

        /// <summary>
        /// Получение индекса элемента в коллекции
        /// </summary>
        /// <typeparam name="T">Тип элементов в колелкции</typeparam>
        /// <param name="collection">Коллекция элементов</param>
        /// <param name="item">Искомый элемент</param>
        /// <returns>Возвращает индекс элемента в коллекции или -1</returns>
        public static int IndexOf<T>(this IEnumerable<T> collection, T item, Func<T, T, bool> comparison = null)
        {
            if (collection.Count() == 0)
                return -1;

            var index = -1;

            foreach (var i in collection)
            {
                index++;

                if (comparison == null ? object.ReferenceEquals(item, i) : comparison.Invoke(i, item))
                    return index;
            }

            return -1;
        }

        public static Task DispatcherAction<T>(this ObservableCollection<T> collection, Action action)
        {
            return App.Current.Dispatcher.BeginInvoke(
                System.Windows.Threading.DispatcherPriority.Background,
                action).Task;
        }

        public static void AddDispatchered<T>(this ObservableCollection<T> collection, T item)
        {
            App.Current.Dispatcher.Invoke(new Action(() => collection.Add(item)));
        }
        public static Task AddDispatcheredAsync<T>(this ObservableCollection<T> collection, T item)
        {
            return App.Current.Dispatcher.InvokeAsync(new Action(() => collection.Add(item))).Task;
        }

        public static void ClearDispatchered<T>(this ObservableCollection<T> collection)
        {
            App.Current.Dispatcher.Invoke(new Action(() => collection.Clear()));
        }

        public static SeparatedAssembly LoadSeparatedAssembly(this SeparatedAppDomain domain, FileInfo fInfo)
        {
            SeparatedAssembly sepAsm = null;
            if (domain == null)
            {
                sepAsm = SeparatedAssembly.Load(fInfo);
            }
            else
                sepAsm = domain.Load(fInfo);

            return sepAsm;
        }
    }
}
