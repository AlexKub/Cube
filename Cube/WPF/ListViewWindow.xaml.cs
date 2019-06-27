using System;
using System.Text;
using System.Windows;

namespace RTCManifestGenerator.Controls
{
    /// <summary>
    /// Логика взаимодействия для ListViewWindow.xaml
    /// </summary>
    public partial class ListViewWindow : Window
    {
        public ListViewWindow(StringBuilder sb)
        {
            InitializeComponent();

            if (sb != null)
            {
                var strings = sb.ToString().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var str in strings)
                    MainLisView.Items.Add(str);
            }
        }

        public static void Show(StringBuilder sb)
        {
            var window = new ListViewWindow(sb);

            window.Show();
        }

        public static void ShowDialog(StringBuilder sb)
        {
            var window = new ListViewWindow(sb);

            window.ShowDialog();
        }
    }
}
