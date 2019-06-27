using Merlion.ECR.Update.Core;
using Merlion.ECR.Update.Core.Log;
using Merlion.ECR.Update.Core.Manifest;
using Merlion.ECR.Update.Core.SeparatedAssemblyLoad;
using Merlion.ECR.Update.Core.Win32_API;
using Microsoft.Win32;
using RTCManifestGenerator.Commands;
using RTCManifestGenerator.Controls;
using RTCManifestGenerator.Models;
using RTCManifestGenerator.Views;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace RTCManifestGenerator
{
    /// <summary>
    /// Общие методы и комманды
    /// </summary>
    public static class StaticMethods
    {
        static LogSet m_loger = GetFileLogSet(nameof(StaticMethods));

        #region DependencyProperties

        #region EnumValues

        /// <summary>
        /// Имена перечисления Merlion.ECR.Update.Core.Manifest.RegistrationActions
        /// </summary>
        public static IEnumerable<string> RegistrationActionsNames { get { return Enum.GetNames(typeof(RegistrationActions)); } }

        public static IEnumerable<string> GetEnumNames(Type type)
        {
            if (type == null)
            {
                m_loger.Log("На полчуение имён перечисления передана пустая ссылка на тип", MessageType.Error);
                return Enumerable.Empty<string>();
            }
            if (!type.IsEnum)
            {
                m_loger.Log("На полчуение имён перечисления передан тип отличный от Enum", MessageType.Error
                    , new LogParameter("Переданный Тип", type.FullName));
                return Enumerable.Empty<string>();
            }

            return Enum.GetNames(type);
        }

        #endregion

        #endregion

        #region RelayCommand<ManifestFileGridItem>

        #region Регистрация/разрегистрация

        public static RelayCommand<ManifestFileGridItem> Reg_GACCommand { get { return new RelayCommand<ManifestFileGridItem>(Reg_GAC); } }
        public static RelayCommand<ManifestFileGridItem> Reg_AsmCommand { get { return new RelayCommand<ManifestFileGridItem>(Reg_Asm); } }
        public static RelayCommand<ManifestFileGridItem> Reg_Svr32Command { get { return new RelayCommand<ManifestFileGridItem>(Reg_Svr32); } }

        public static RelayCommand<ManifestFileGridItem> UnReg_GACCommand { get { return new RelayCommand<ManifestFileGridItem>(UnReg_GAC); } }
        public static RelayCommand<ManifestFileGridItem> UnReg_AsmCommand { get { return new RelayCommand<ManifestFileGridItem>(UnReg_Asm); } }
        public static RelayCommand<ManifestFileGridItem> UnReg_Svr32Command { get { return new RelayCommand<ManifestFileGridItem>(UnReg_Svr32); } }

        private static void Reg_GAC(ManifestFileGridItem item)
        {
            try
            {
                var result = Merlion.ECR.Update.Core.Environment.GAC.ReInstallAssembly(item.FileInfo.FullName);

                MessageBox.Show("Результат операции: " + result.ToString());
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при регистрации библиотеки через GAC", ex);
            }
        }

        private static void Reg_Asm(ManifestFileGridItem item)
        {
            try
            {
                bool codebase = MessageBox.Show("Использовать /codebase?", "Использование параметра", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
                bool tlb = MessageBox.Show("Выгрузить tlb в каталог библиотеки?", "Использование параметра", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

                var result = Merlion.ECR.Update.Core.Environment.AssemblyManager.Install_ByRegAsm(item.FileInfo, codebase, tlb);

                if (result == Merlion.ECR.Update.Core.Environment.RegistrationResult.Installed)
                    Merlion.ECR.Update.Core.Environment.RegAsm.InstallTypeLib(item.FileInfo);

                MessageBox.Show("Результат операции: " + result.ToString());
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при регистрации библиотеки через RegAsm", ex);
            }
        }

        private static void Reg_Svr32(ManifestFileGridItem item)
        {
            try
            {
                var result = Merlion.ECR.Update.Core.Environment.AssemblyManager.Install_ByRegSvr32(item.FileInfo);
                //var result = Merlion.ECR.Update.Core.Environment.AssemblyManager.ResultFromExitcode(exitcode);

                MessageBox.Show("Результат операции: " + result.ToString());
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при регистрации библиотеки через Regsvr32", ex);
            }
        }

        private static void UnReg_GAC(ManifestFileGridItem item)
        {
            try
            {
                var result = Merlion.ECR.Update.Core.Environment.GAC.UnInstallAssembly(System.IO.Path.GetFileNameWithoutExtension(item.FileInfo.Name));

                MessageBox.Show("Результат операции: " + result.ToString());
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при разрегистрации библиотеки через GAC", ex);
            }
        }

        private static void UnReg_Asm(ManifestFileGridItem item)
        {
            try
            {
                var result = Merlion.ECR.Update.Core.Environment.AssemblyManager.UnInstall_ByRegAsm(item.FileInfo);

                MessageBox.Show("Результат операции: " + result.ToString());
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при разрегистрации библиотеки через RegAsm", ex);
            }
        }

        private static void UnReg_Svr32(ManifestFileGridItem item)
        {
            try
            {
                var result = Merlion.ECR.Update.Core.Environment.AssemblyManager.UnInstall_ByRegSvr32(item.FileInfo);
                //var result = Merlion.ECR.Update.Core.Environment.AssemblyManager.ResultFromExitcode(exitcode);

                MessageBox.Show("Результат операции: " + result.ToString());
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при разрегистрации библиотеки через Regsvr32", ex);
            }
        }

        #endregion

        public static RelayCommand<ManifestFileGridItem> OpenItemFolderCommand { get { return new RelayCommand<ManifestFileGridItem>(OpenItemFolder); } }

        public static RelayCommand<ManifestFileGridItem> OpenItemFileCommand { get { return new RelayCommand<ManifestFileGridItem>(OpenItemFile); } }

        public static RelayCommand<ManifestFileGridItem> ShowPublicKeyTokenCommand { get { return new RelayCommand<ManifestFileGridItem>(ShowPublicKeyToken); } }

        public static RelayCommand<ManifestFileGridItem> ShowAddinNameCommand { get { return new RelayCommand<ManifestFileGridItem>(ShowAddinName); } }

        public static RelayCommand<ManifestFileGridItem> ShowFileHashCommand { get { return new RelayCommand<ManifestFileGridItem>(ShowFileHash); } }

        private static void OpenItemFolder(ManifestFileGridItem item)
        {
            try
            {
                if (item.FileInfo != null && item.FileInfo.Exists)
                    item.FileInfo.Directory.OpenExplorer();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии каталога файла", ex);
            }
        }

        private static void OpenItemFile(ManifestFileGridItem item)
        {
            try
            {
                if (item.FileInfo != null && item.FileInfo.Exists)
                    item.FileInfo.OpenExplorer();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии файла", ex);
            }
        }

        private static void ShowPublicKeyToken(ManifestFileGridItem item)
        {
            try
            {
                var pkt = Merlion.ECR.Update.Core_2_0.Utilites.GetPublicKeyToken(item.FileInfo.FullName);

                var window = new CopyWindow("Токен публичного ключа", pkt);

                window.ShowDialog();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при показе Токена первичного ключа", ex);
            }
        }

        private static void ShowAddinName(ManifestFileGridItem item)
        {
            try
            {
                string name = string.Empty;
                using (var asm = SeparatedAssembly.Load(item.FileInfo))
                {
                    name = asm.GetControlAddinName();

                    if (asm.HasException)
                        throw new InvalidOperationException("Возникло исключение при получении имени Add-In'а", asm.LastEx);
                }

                var window = new CopyWindow("Имя Add-In'a", name);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при показе Имени AddIn'a", ex);
            }
        }

        private static void ShowFileHash(ManifestFileGridItem item)
        {
            try
            {
                var crc = Merlion.ECR.Update.Core_2_0.Utilites.GetFileHash(item.FileInfo.FullName);

                var window = new CopyWindow("CRC", crc);
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при выводе хеша файла", ex);
            }
        }

        public static RelayCommand<ManifestFileGridItem> AddToDefaultTemplateCommand { get { return new RelayCommand<ManifestFileGridItem>(AddToDefaultTemplate); } }
        private static void AddToDefaultTemplate(ManifestFileGridItem value)
        {
            try
            {
                var defTemplate = TemplatesSet.Load();

                if (defTemplate.Contains(value))
                    defTemplate.UpdateItemBy(value);
                else
                    defTemplate.AddItem(value);

                defTemplate.Save();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при добавлении информации о файле в текущий шаблон", ex);
            }
        }

        public static RelayCommand<ManifestFileGridItem> SignFileCommand { get { return new RelayCommand<ManifestFileGridItem>(SignFile); } }
        private static void SignFile(ManifestFileGridItem value)
        {
            SigFile(value.FileInfo);
        }

        /// <summary>
        /// Подписание файла
        /// </summary>
        /// <param name="info">Информация о файле</param>
        /// <returns>Возвращает флаг успешной подписи</returns>
        public static bool SigFile(FileInfo info, bool wait = true)
        {
            if (info == null)
            {
                MessageBox.Show("Отсутствует информация о файле для подписи");
                return false;
            }

            try
            {
                var proc = new Process();
                var pInfo = proc.StartInfo;
                pInfo.FileName = @"C:\VS 2015\Projects\ECR\Code\SignTool\bin\Debug\SignTool.exe";
                pInfo.Arguments = "show \"" + info.FullName + "\"";
                pInfo.Verb = "runas";

                proc.Start();
                if (wait)
                {
                    proc.WaitForExit();
                    proc.Dispose();
                }

                return true;
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при попытке подписи файла", ex, new LogParameter("Имя файла", info.FullName));
                return false;
            }
        }

        /// <summary>
        /// Открывает окно редактирования сервера RTC
        /// </summary>
        public static RelayCommand<System.Windows.Window> OpenRTCServerWindowCommand { get { return new RelayCommand<System.Windows.Window>(OpenRTCServerWindow); } }
        private static void OpenRTCServerWindow(System.Windows.Window mainWindow)
        {
            try
            {
                var window = new Windows.RTCServerView();
                window.Owner = mainWindow;
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии окна просмотра Сервера RTC", ex);
            }
        }

        /// <summary>
        /// Открывает модальное окно просмотра Файла состояния
        /// </summary>
        public static RelayCommand<System.Windows.Window> OpenFileStateWindowCommand { get { return new RelayCommand<System.Windows.Window>(OpenFileStateWindow); } }
        private static void OpenFileStateWindow(System.Windows.Window mainWindow)
        {
            try
            {
                var window = new UpdateStateWindow();
                window.Owner = mainWindow;
                window.ShowDialog();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при попытке открыть окно просмотра Файла состояния", ex);
            }
        }

        public static RelayCommand<string> OpenLogSettingsCommand { get { return new RelayCommand<string>(OpenLogSettings); } }
        private static void OpenLogSettings(string fileKey)
        {
            try
            {
                if (ViewModels.LogSettingsViewModel.CustomFileKey.Equals(fileKey))
                {
                    var file = Coockies.SelectFile(fileKey, (path) => SelectFilesDialog(path, "Выберите файл с настройками логирования", false, "XML | *.xml").FirstOrDefault());

                    if (file == null)
                        return;
                }

                var window = new LogSettingsView(fileKey);

                window.ShowDialog();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при просмотре настроек логирования", ex
                    , new LogParameter("Ключ в настойках", fileKey));
            }
        }

        public static RelayCommand<IEnumerable<ManifestFileGridItem>> RestartAsAdminCommand { get { return new RelayCommand<IEnumerable<ManifestFileGridItem>>(RestartAsAdmin); } }
        private static void RestartAsAdmin(IEnumerable<ManifestFileGridItem> value)
        {
            //выбор загруженных файлов среди переданных (только те, у которых есть пути)
            var filesArray = value == null ? new string[0] : value.Where(i => i != null && !string.IsNullOrEmpty(i.FullPatName)).Select(i => i.FullPatName).ToArray();

            StartupArgs args = new StartupArgs()
            {
                LoadFiles = filesArray
            };

            App.RunAs(args);
        }

        public static RelayCommand<string> OpenFolderCommand { get { return new RelayCommand<string>(OpenFolder); } }

        public static RelayCommand<System.Windows.Window> CloseWindowCommand { get { return new RelayCommand<System.Windows.Window>(CloseWindow); } }

        #endregion

        #region ActionCommand

        public static ActionCommand OpenInstructionCommand { get { return new ActionCommand(OpenInstruction); } }

        public static ActionCommand GetProgramVersionCommand { get { return new ActionCommand(GetProgramVersion); } }

        public static ActionCommand OpenProgramFolderCommand { get { return new ActionCommand(OpenProgramFolder); } }

        public static ActionCommand OpenLogFolderCommand { get { return new ActionCommand(OpenLogFolder); } }

        public static ActionCommand OpenRootFolderCommand { get { return new ActionCommand(OpenRootFolder); } }

        public static ActionCommand RemoveActiveManifestCommand { get { return new ActionCommand(RemoveActiveManifest); } }

        public static ActionCommand CheckFileLockCommand { get { return new ActionCommand(CheckFileLock); } }

        public static ActionCommand ActiveManifestVersionCommand { get { return new ActionCommand(ActiveManifestVersion); } }

        public static ActionCommand ActiveManifestOpenCommand { get { return new ActionCommand(ActiveManifestOpen); } }

        public static ActionCommand ActiveManifestDownVersionCommand { get { return new ActionCommand(ActiveManifestDownVersion); } }

        public static ActionCommand TestCommand { get { return new ActionCommand(() => { MessageBox.Show("Test Command called!"); }); } }

        #endregion

        /// <summary>
        /// Удаляет из набора файлы манифеста
        /// </summary>
        /// <param name="files">Начальный набор файлов</param>
        /// <returns>Файлы, не содержащие имён манифеста</returns>
        internal static IEnumerable<FileInfo> RemoveManifestFiles(IEnumerable<FileInfo> files)
        {
            if (files == null || files.Count() == 0)
                return files;

            var rtcName = Path.GetFileNameWithoutExtension(RTCManifest.DefaultFileName).ToLower();
            var ecrName = Path.GetFileNameWithoutExtension(ECRManifest.DefaultFileName).ToLower();
            //var settingsName = Path.GetFileNameWithoutExtension(UpdateSettings.DefaultFileName).ToLower();

            return files.Where(f =>
            {
                var name = f.Name.ToLower();

                //все файлы, без разрешения xml
                return (!f.Extension.Equals(".xml", StringComparison.InvariantCultureIgnoreCase))
                //или те (среди xml) в которых отсутствуют имена файлов манифестов по умолчанию
                || ((!name.Contains(rtcName))
                && (!name.Contains(ecrName)));
            });
        }

        internal static void UnLoadInstruction()
        {
            var filePathName = Constants.InstructionFileName;

            try
            {
                if (!File.Exists(filePathName))
                {
                    File.WriteAllBytes(filePathName, Properties.Resources.Instruction);
                }
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при проверке/выгрузке инструкии пользователя", ex
                    , new LogParameter("Путь к файлу", LogMessageBuilder.GetStringLogVal(filePathName)));
            }
        }

        private static void OpenInstruction()
        {
            var filePathName = Constants.InstructionFileName;

            if (!File.Exists(filePathName))
            {
                UnLoadInstruction();

                if (!File.Exists(filePathName))
                {
                    LogAndShow("Инструкция не найдена", null, new LogParameter("Адрес файла", LogMessageBuilder.GetStringLogVal(filePathName)));
                    return;
                }
            }

            Process p = new Process();
            var pi = p.StartInfo;
            pi.FileName = filePathName;

            p.Start();
        }

        private static void GetProgramVersion()
        {
            try
            {
                //MessageBox.Show("Версия программы: " + App.Version == null ? "NOT INITIALIZED" : App.Version.FileVersion);
                var versionWindow = new Windows.VersionWindow();
                versionWindow.Owner = App.Current.MainWindow;
                versionWindow.ShowDialog();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при проверке версии приложения", ex);
            }
        }

        private static void OpenProgramFolder()
        {
            try
            {
                var dirName = AppDomain.CurrentDomain.BaseDirectory;

                new DirectoryInfo(dirName).OpenExplorer();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии каталога с программой", ex);
            }
        }

        private static void OpenLogFolder()
        {
            if (m_loger == null)
            {
                MessageBox.Show("Логирование не инициализировано!");
                return;
            }

            string logFolder = string.Empty;
            try
            {
                if (m_loger.Settings.FileLogingFolder != null)
                {
                    var firstFileLoger = m_loger?.LogerPacks?.FirstOrDefault(lp => lp.Loger is FileLoger)?.Loger as FileLoger;

                    if (firstFileLoger != null)
                        logFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, firstFileLoger.LogFolder);
                    else
                    {
                        MessageBox.Show("Логирование в файл не задано.");
                        return;
                    }
                }
                else
                {
                    logFolder = AppDomain.CurrentDomain.BaseDirectory;
                }


                new DirectoryInfo(logFolder).OpenExplorer();
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии каталога логирования", ex
                    , new LogParameter("Имя каталога", LogMessageBuilder.GetStringLogVal(logFolder)));
            }
        }

        private static void OpenRootFolder()
        {
            string rootFolder = "<notInited>";
            try
            {
                rootFolder = Merlion.ECR.Update.Core.Constants.RootFolder;

                var dir = new DirectoryInfo(rootFolder);
                if (dir.Exists)
                    dir.OpenExplorer();
                else
                    MessageBox.Show("Папка не найдена: " + dir.FullName);
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии корневого каталога", ex
                    , new LogParameter("Имя каталога", LogMessageBuilder.GetStringLogVal(rootFolder)));
            }
        }

        /// <summary>
        /// Окно выбора файла манфиеста
        /// </summary>
        /// <param name="defaultPath">Путь по умочланию</param>
        /// <param name="pathKey">Ключ для поиска/сохранения пути по умолчанию в Файле настроек</param>
        /// <returns>Возвращает выбранный файл манифеста или null</returns>
        public static RTCManifest SelectManifest(string defaultPath = null, string pathKey = null)
        {
            try
            {
                OpenFileDialog ofd = new OpenFileDialog();

                ofd.Multiselect = false;
                ofd.Title = "Выберите файл Манифеста";
                ofd.CheckFileExists = true;
                ofd.Filter = "XML | *.xml";

                MG_Settings settings = null;
                if (!string.IsNullOrEmpty(pathKey))
                {
                    settings = MG_Settings.Load();

                    if (string.IsNullOrWhiteSpace(defaultPath))
                        if (settings.Directories.Contains(pathKey)) //загрузка из файла настроек по ключу каталога по умолчанию
                            defaultPath = settings.Directories[pathKey];
                }

                if (!string.IsNullOrWhiteSpace(defaultPath))
                    if (Directory.Exists(defaultPath))
                        ofd.InitialDirectory = defaultPath;

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

                if (!string.IsNullOrEmpty(pathKey))
                    //сохраняем каталог выбранного манифеста в файле настроек
                    settings.Directories.Save(new NamedSettingsValue() { Key = pathKey, Value = manifest.FileInfo.DirectoryName });

                return manifest;
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при загрузке файла манифеста", ex);
                return null;
            }
        }

        public static IEnumerable<FileInfo> SelectFilesDialog(string defaultPath = "", string description = "", bool multiselect = true, string filter = "")
        {
            try
            {
                var ofd = new OpenFileDialog();
                ofd.Multiselect = multiselect;
                ofd.Filter = filter;

                if (string.IsNullOrEmpty(defaultPath))
                {
                    var settingsPath = Properties.Settings.Default.LoadFilesPath;
                    if (!string.IsNullOrEmpty(settingsPath))
                        if (Directory.Exists(settingsPath))
                            ofd.InitialDirectory = settingsPath;
                }
                else
                {
                    ofd.InitialDirectory = defaultPath;
                }

                if (string.IsNullOrWhiteSpace(description))
                    ofd.Title = multiselect ? "Выбор файлов" : "Выбор файла";
                else
                    ofd.Title = description;

                var result = ofd.ShowDialog();

                if (result == true)
                {
                    var files = ofd.FileNames;

                    if (files != null && files.Length > 0)
                    {
                        Properties.Settings.Default.LoadFilesPath = Path.GetDirectoryName(files.First());
                        Properties.Settings.Default.Save();

                        return files.Select(f => new FileInfo(f));
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при выборе файлов", ex);
            }

            return Enumerable.Empty<FileInfo>();
        }

        /// <summary>
        /// Диалог выбора папки и загрузка всех файлов из выбранной
        /// </summary>
        /// <param name="defaultPath">Путь по умолчанию</param>
        /// <param name="description">Описание для окна выбора папки</param>
        /// <param name="excludeManifests">Флаг исключения из выборки файлов манифеста</param>
        /// <param name="searchPattern">Шаблон поиска</param>
        /// <returns>Возвращает список файлов из выбранной пользователем папки или пустой набор</returns>
        public async static Task<IEnumerable<FileInfo>> SelectFilesFromFolderDialog(string defaultPath = "", string description = "", bool excludeManifests = true, string searchPattern = "*.*")
        {
            using (var dialog = new System.Windows.Forms.FolderBrowserDialog())
            {
                try
                {
                    dialog.Description = string.IsNullOrWhiteSpace(description) ? "Выберите папку с файлами" : description;

                    if (!string.IsNullOrWhiteSpace(defaultPath))
                        dialog.SelectedPath = defaultPath;

                    System.Windows.Forms.DialogResult result = dialog.ShowDialog();
                    if (result == System.Windows.Forms.DialogResult.OK)
                    {
                        DirectoryInfo dInfo = new DirectoryInfo(dialog.SelectedPath);

                        var recursive = MessageBox.Show("Включая вложенные папки?", "Определите вложенность выбора файлов", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

                        IEnumerable<FileInfo> files = Enumerable.Empty<FileInfo>();

                        files = await Task.Run(() =>
                        {
                            files = recursive ? dInfo.GetFiles(searchPattern, SearchOption.AllDirectories) : dInfo.GetFiles(searchPattern);

                            return excludeManifests ? RemoveManifestFiles(files) : files;
                        });

                        return files;
                    }
                }
                catch (Exception ex)
                {
                    LogAndShow("Возникло исключение при загрузке папки", ex
                        , new LogParameter("Каталог", LogMessageBuilder.GetStringLogVal(defaultPath))
                        , new LogParameter("Заголовок окна", LogMessageBuilder.GetStringLogVal(description))
                        , new LogParameter("Исключая манифесты", excludeManifests.ToString())
                        , new LogParameter("Паттерн поиска", LogMessageBuilder.GetStringLogVal(searchPattern)));
                }
            }

            return Enumerable.Empty<FileInfo>();
        }

        private static void RemoveActiveManifest()
        {
            try
            {
                var fileName = GetActiveManifestFileName();

                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    MessageBox.Show("Файл удалён\r\n" + fileName);
                }
                else
                {
                    MessageBox.Show("Файл не найден\r\n" + fileName);
                }
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при удалении активного манифеста", ex);
            }
        }

        private static void CheckFileLock()
        {
            try
            {
                var file = SelectFilesDialog(multiselect: false).FirstOrDefault();

                if (file == null)
                    return;

                var lockers = FileLockChecker.WhoIsLocking(file);

                var sb = new System.Text.StringBuilder();
                foreach (var proc in lockers)
                {
                    using (proc)
                        sb.Append("ID: ").Append(proc.Id.ToString()).Append(" | ").Append("Name: ").AppendLine(proc.ProcessName);
                }

                MessageBox.Show(sb.Length == 0 ? "Файл не занят" : sb.ToString(), "Блокирующие процессы");
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при проверке блокировки файла", ex);
            }
        }

        private static void ActiveManifestVersion()
        {
            try
            {
                var fileName = GetActiveManifestFileName();

                if (File.Exists(fileName))
                {
                    var manifest = RTCManifest.Load(fileName);
                    MessageBox.Show("Версия манифеста: " + manifest.Version.ToString());
                }
                else
                {
                    MessageBox.Show("Файл не найден\r\n" + fileName);
                }
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при получении версии активного манифеста", ex);
            }
        }

        private static void ActiveManifestOpen()
        {
            try
            {
                var fileName = GetActiveManifestFileName();

                ShowFile(fileName);
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии активного манифеста", ex);
            }
        }

        public static void ShowFile(string fileName)
        {
            if (File.Exists(fileName))
            {
                new FileInfo(fileName).OpenExplorer();
            }
            else
            {
                MessageBox.Show("Файл не найден\r\n" + fileName);
            }
        }

        private static void ActiveManifestDownVersion()
        {
            try
            {
                var fileName = GetActiveManifestFileName();

                if (File.Exists(fileName))
                {
                    var manifest = RTCManifest.Load(fileName);
                    int oldVersion = manifest.Version;
                    manifest.Version--;

                    manifest.Save();

                    MessageBox.Show("Версия манифеста понижена: " + oldVersion.ToString() + " > " + manifest.Version.ToString());
                }
                else
                {
                    MessageBox.Show("Файл не найден\r\n" + fileName);
                }
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии активного манифеста", ex);
            }
        }

        internal static void OpenFolder(string folder)
        {
            try
            {
                bool remoteFolder = folder.StartsWith("\\");

                if (remoteFolder)
                {
                    Process command = new Process();

                    command.StartInfo.FileName = @"explorer";

                    command.StartInfo.Arguments = folder;

                    command.StartInfo.CreateNoWindow = false;

                    command.StartInfo.Verb = "open";

                    command.StartInfo.UseShellExecute = false;

                    command.Start();

                }
                else
                {
                    //Directory.Exists не работает для удалённой папки
                    var dir = new DirectoryInfo(folder);
                    dir.OpenExplorer();
                }
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при открытии каталога", ex);
            }
        }

        internal static void CloseWindow(System.Windows.Window window)
        {
            if (window != null)
            {
                try
                {
                    if (window.DataContext != null)
                    {
                        var disposableContext = window.DataContext as IDisposable;

                        if (disposableContext != null)
                            disposableContext.Dispose();
                    }

                    var disposableWindow = window as IDisposable;

                    if (disposableWindow != null)
                        disposableWindow.Dispose();

                    window.Close();

                }
                catch (Exception ex)
                {
                    LogAndShow("Возникло исключение при закрытии окна", ex
                        , new LogParameter("Тип окна", window.GetType().FullName));
                }
            }
            else
                MessageBox.Show("Не передана ссылка на окно при закрытии");
        }

        internal static string GetActiveManifestFileName()
        {
            return Path.Combine(Merlion.ECR.Update.Core.Constants.RootFolder, RTCManifest.DefaultFileName);
        }

        /// <summary>
        /// Загрузка активного манифеста.
        /// </summary>
        /// <returns>Возвращает активный манфиест или null, если не найден файл/не удалось загрузить</returns>
        internal static RTCManifest LoadActiveManifest()
        {
            var fileName = string.Empty;

            try
            {
                fileName = GetActiveManifestFileName();

                if (File.Exists(fileName))
                    return RTCManifest.Load(fileName);
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при загрузке активного манифеста", ex
                    , new LogParameter("Путь к файлу", fileName));
            }

            return null;
        }

        internal static void LogAndShow(string message, Exception ex, params ILogParameter[] parameters)
        {
            if (ex == null)
                m_loger.Log(message, parameters);
            else
                m_loger.Log(message, ex, parameters);

            System.Windows.MessageBox.Show(message + (ex == null ? "" : (": \r\n" + ex.Message)));
        }

        /// <summary>
        /// Вызов диалога выбора папки
        /// </summary>
        /// <param name="selectedPath">Вуть, выбранный при открытии диалога</param>
        /// <returns>Возвращает выбранный пользователем путь или null</returns>
        public static string SelectFolderDialog(string selectedPath)
        {
            try
            {
                var fbd = new System.Windows.Forms.FolderBrowserDialog();

                fbd.Description = "Выберите папку";
                fbd.SelectedPath = selectedPath;
                fbd.ShowNewFolderButton = true;

                if (string.IsNullOrWhiteSpace(selectedPath))
                    if (!string.IsNullOrEmpty(Properties.Settings.Default.LoadManifestPath))
                        fbd.SelectedPath = Properties.Settings.Default.LoadManifestPath;

                if (fbd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return null;

                return fbd.SelectedPath;
            }
            catch (Exception ex)
            {
                m_loger.Log("Возникло исключение при вызове диалога выбора папки", ex
                    , new LogParameter("Путь по умолчанию", LogMessageBuilder.GetStringLogVal(selectedPath)));

                return null;
            }
        }

        /// <summary>
        /// Открывает окно проверки основного манифеста
        /// </summary>
        /// <returns>Возвращает задачу с открытием окна</returns>
        internal static Task<ManifestChangeWindow> CheckActiveManifest()
        {
            //MessageBox.Show("В разработке...");
            //return Task.FromResult<ManifestChangeWindow>(null);

            var aMon = GetActiveManifest();

            if (aMon == null || aMon.IsEmpty())
            {
                MessageBox.Show("Не удалось загрузить активный манифест");

                return Task.FromResult<ManifestChangeWindow>(null);
            }

            return CheckManifest(aMon, true);
        }

        internal static RTCManifest GetActiveManifest()
        {
            string aMonFileName = string.Empty;

            try
            {
                aMonFileName = GetActiveManifestFileName();
                return RTCManifest.Load(aMonFileName);
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при загрузке активного манифеста", ex, new LogParameter("Путь к файлу", aMonFileName));
                return null;
            }
        }

        /// <summary>
        /// Получить файлы из активного каталога
        /// </summary>
        /// <returns>Возвращает набор файлов по активному манифесту или пустой набор</returns>
        internal static IEnumerable<FileInfo> GetActiveManifestFiles()
        {
            List<FileInfo> files = new List<FileInfo>();

            var rootPath = Merlion.ECR.Update.Core.Constants.RootFolder;
            var root = RTCManifest.RootDirectoryFlag;
            string pathName = string.Empty;

            try
            {
                var mon = GetActiveManifest();

                if (mon == null || mon.IsEmpty())
                    return files;


                foreach (var file in mon.Files)
                {
                    foreach (var dir in file.Folders)
                    {
                        pathName = root.Equals(dir)
                            ? Path.Combine(rootPath, file.FileName)
                            : Path.Combine(Path.Combine(rootPath, dir), file.FileName);

                        files.Add(new FileInfo(pathName));
                    }
                }
            }
            catch (Exception ex)
            {
                LogAndShow("Возникло исключение при генерации набора файлов активного манифеста", ex, new LogParameter("Последний путь к файлу", pathName));
            }

            return files;
        }

        internal static Task<ManifestChangeWindow> CheckManifest(RTCManifest manifest, bool active = false)
        {
            return Task.Run(() =>
            {
                var baseFiles = manifest.Files.OfType<IManifestFileItem>();

                var manifestFileName = Path.GetFileNameWithoutExtension(manifest.FileInfo.Name);

                var manifestFiles = active
                                        ? GetActiveManifestFiles()//для активного манифеста собираем файлы по путям, указанным в манифесте
                                        : manifest.FileInfo.Directory.GetFiles(); //для прочих манифестов берём файлы из текущего каталога

                var rawFiles = StaticMethods.RemoveManifestFiles(manifestFiles);

                if (rawFiles.Count() == 0)
                {
                    MessageBox.Show("В папке манифеста не найдено файлов для сравнения", "Файлы не найдены");
                }

                List<ManifestFileGridItem> rawFileItems = null;
                RTCManifest tempManifest = null;


                //генерируем временный манифест из файлов, что лежат рядом с текущим
                var root = manifest.FileInfo.Directory;
                using (var domain = SeparatedAppDomain.Create())
                    rawFileItems = rawFiles.Select(fi => new ManifestFileGridItem(fi, root, domain)).ToList();

                if (active) //при проверке активного манифеста
                {
                    //выбираем файлы, для которых указано несколько папок
                    var multipleFolderFiles = manifest.Files.Where(fi => fi.Folders.Length > 1);

                    foreach (var fileItem in multipleFolderFiles)
                    {
                        //для каждого находим соответствующую пачку в активных файлах
                        var activeFiles = rawFileItems.Where(rf => rf.FileName.Equals(fileItem.FileName)).ToList();
                        ManifestFileGridItem tempFile = null;

                        /*
                         * Для одинаковых файлов (по CRC) просто добавляем папку к первому попавшемуся
                         * если в какой-то из папок файл отличается, то его просто пропускаем - на форме высветится красным
                         */
                        foreach (var aFile in activeFiles)
                        {
                            if (aFile.CRC.Equals(fileItem.CRC))
                            {
                                if (tempFile == null)
                                    tempFile = aFile;
                                else
                                {
                                    tempFile.AddFolder(aFile.Folders);

                                    rawFileItems.Remove(aFile);
                                }
                            }
                            else
                                aFile.FileName = aFile.FileName + "_" + aFile.CRC; //добавляем CRC-шку для видимости
                        }
                        tempFile = null;
                    }
                }

                tempManifest = RTCManifest.Create(files: rawFileItems);
                ManifestFileItem baseItem = null;
                //копируем не значимые параметры из основного манифеста, чтобы не мозолили глаза при сравнении
                tempManifest.Version = manifest.Version;
                foreach (var mFile in tempManifest.Files)
                {
                    baseItem = manifest.GetItemByFileName(mFile.FileName);

                    if (baseItem == null)
                        continue;

                    if (baseItem.IsAssembly)
                    {
                        mFile.GAC = baseItem.GAC;
                        mFile.REGASM = baseItem.REGASM;
                        mFile.REGSRV32 = baseItem.REGSRV32;
                    }

                    mFile.Type = baseItem.Type;

                    if (!active) //для активного манифеста важны папки
                        mFile.Folders = baseItem.Folders;
                    mFile.Delete = baseItem.Delete;
                }

                ManifestChangeWindow window = null;
                App.Current.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.ApplicationIdle, new Action(() =>
               {
                   var updateWindow = new ManifestChangeWindow(tempManifest, manifest);
                   updateWindow.Title = active ? "Проверка рабочего манифеста" : "Обновление манифеста файлами из того же каталога";
                   updateWindow.ButtonText = "Обновить манифест";
                   updateWindow.UpdateEnable = !active; //запрещаем обновление для активного манифеста
                   updateWindow.LeftLoadable = false;
                   updateWindow.RightLoadable = false;
                   window = updateWindow;
               })).Wait();

                return window;
            });
        }

        internal static LogSet GetFileLogSet<TType>()
        {
            return LogManager.GetLogSet<TType>(LogManager.LogSetFlags.File);
        }

        internal static LogSet GetFileLogSet(string typeName)
        {
            return LogManager.GetLogSet(typeName, LogManager.LogSetFlags.File);
        }
    }
}
