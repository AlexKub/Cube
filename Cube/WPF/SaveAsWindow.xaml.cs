using System.IO;
using System.Windows;

namespace RTCManifestGenerator.Windows
{
    /// <summary>
    /// Логика взаимодействия для SaveAsWindow.xaml
    /// </summary>
    public partial class SaveAsWindow : Window
    {
        readonly string m_defaultPath;
        readonly string m_defaultFileName;

        public string SelectedFilePathName { get; private set; }

        public SaveAsWindow(string defaultPath, string defaultFileName)
        {
            InitializeComponent();

            m_defaultPath = defaultPath;
            m_defaultFileName = defaultFileName;

            tbl_Folder.Text = defaultPath;
            tbx_Name.Text = Path.GetFileNameWithoutExtension(defaultFileName);
            tbl_Extension.Text = Path.GetExtension(defaultFileName);
        }

        private void Button_SelectFolder_Click(object sender, RoutedEventArgs e)
        {
            var folder = StaticMethods.SelectFolderDialog(tbl_Folder.Text);

            if (!string.IsNullOrEmpty(folder))
                tbl_Folder.Text = folder;
        }

        private void Button_Save_Click(object sender, RoutedEventArgs e)
        {
            var fileName = tbx_Name.Text + tbl_Extension.Text;

            var filePathName = Path.Combine(m_defaultPath, m_defaultFileName);

            SelectedFilePathName = filePathName;

            DialogResult = true;

            Close();
        }

        private void tbx_Name_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            var text = tbx_Name.Text;

            if (string.IsNullOrWhiteSpace(text))
                tbx_Name.Text = Path.GetFileNameWithoutExtension(m_defaultFileName);
        }

        public static string ShowDialog(string defaultPath, string defaultFileName)
        {
            var window = new SaveAsWindow(defaultPath, defaultFileName);
            window.Owner = App.Current.MainWindow;
            if (window.ShowDialog() == true)
                return window.SelectedFilePathName;
            else
                return null;

        }
    }
}
