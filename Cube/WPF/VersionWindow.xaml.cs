using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;

namespace RTCManifestGenerator.Windows
{
    /// <summary>
    /// Interaction logic for VersionWindow.xaml
    /// </summary>
    public partial class VersionWindow : Window
    {
        string m_newVersionDir;

        public FileVersionInfo Version { get; private set; }

        public VersionWindow()
        {
            InitializeComponent();

            Height = 160;
            grd_NewVersion.Visibility = Visibility.Collapsed;

            SetVersion();

            CheckForUpdates();
        }

        void SetVersion()
        {
            try
            {
                Version = FileVersionInfo.GetVersionInfo(App.File.FullName);

                tbx_CurentVersion.Text = Version.FileVersion;
                tbx_CurentFrom.Text = App.File.LastWriteTime.ToString();
            }
            catch (Exception ex)
            {
                StaticMethods.LogAndShow("Возникло исключение при получении версии приложения", ex);
            }
        }

        public void CheckForUpdates()
        {
            System.Threading.Tasks.Task.Run(() =>
            {
                try
                {
                    //папка с версиями приложения
                    var distributePath = @"\\Fs\dfs$\IT\Общее\\Kubasov\ManifestGenerator";

                    //если кто-то запускает прилу из дистрибутива - версию не чекаем
                    //иначе при запуске старой версии она перезатрётся новой, 
                    //что, возможно, будет ненужно - хотели запустить старую версию
                    if (App.File.DirectoryName.Contains(distributePath))
                        return;

                    var dirs = Directory.EnumerateDirectories(distributePath);

                    if (dirs != null && dirs.Count() > 0)
                    {
                        //получаем последнюю папку в дистрибутиве
                        //по идее, там должны лежать только папки с программой
                        //с датами в прямом порядке (выбираем последнюю дату)
                        var lastDir = new DirectoryInfo(dirs.Last());

                        //выбираем файл с тем же именем, что и запущенный exe-шник
                        var newFile = lastDir.GetFiles().FirstOrDefault(f => f.Name.Equals(App.File.Name, StringComparison.CurrentCultureIgnoreCase));

                        if (newFile != null)
                        {
                            var newFileVersion = FileVersionInfo.GetVersionInfo(newFile.FullName);

                            //сравнение версий файлов
                            var newVersion =
                                newFileVersion.FileMajorPart > Version.FileMajorPart ||
                                (newFileVersion.FileMinorPart > Version.FileMinorPart && newFileVersion.FileMajorPart == Version.FileMajorPart) ||
                                (newFileVersion.FilePrivatePart > Version.FilePrivatePart && (newFileVersion.FileMinorPart == Version.FileMinorPart && newFileVersion.FileMajorPart == Version.FileMajorPart)) ||
                                (newFileVersion.FileBuildPart > Version.FileBuildPart && (newFileVersion.FilePrivatePart == Version.FilePrivatePart && newFileVersion.FileMinorPart == Version.FileMinorPart && newFileVersion.FileMajorPart == Version.FileMajorPart));

                            if (newVersion)
                            {
                                Dispatcher.Invoke(() =>
                                {
                                    grd_NewVersion.Visibility = Visibility.Visible;
                                    Height = 320;
                                    tbx_NewVersion.Text = newFileVersion.FileVersion;
                                    tbx_VersionDate.Text = newFile.LastWriteTime.ToString();
                                    m_newVersionDir = lastDir.FullName;
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    StaticMethods.LogAndShow("Возникло искюлчени при проверке версии приложения", ex);
                }
            });
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            StaticMethods.OpenFolder(m_newVersionDir);
        }
    }
}
