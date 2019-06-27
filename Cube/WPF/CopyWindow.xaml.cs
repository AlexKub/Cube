using System.Windows;

namespace RTCManifestGenerator.Controls
{
    /// <summary>
    /// Логика взаимодействия для CopyWindow.xaml
    /// </summary>
    public partial class CopyWindow : Window
    {
        public CopyWindow(string title, string value)
        {
            InitializeComponent();

            Title = title;
            tbx_Value.Text = value;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
