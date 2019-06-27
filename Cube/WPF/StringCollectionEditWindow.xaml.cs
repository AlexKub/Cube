using RTCManifestGenerator.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace RTCManifestGenerator.Windows
{
    /// <summary>
    /// Логика взаимодействия для StringCollectionWindow.xaml
    /// </summary>
    public partial class StringCollectionEditWindow : Window
    {

        public ActionCommand AcceptChangesCommand { get { return new ActionCommand(AcceptChanges); } }
        private void AcceptChanges()
        {
            DialogResult = true;

            this.Close();
        }

        public ActionCommand AddItemCommand { get { return new ActionCommand(AddItem); } }
        private void AddItem()
        {
            var window = new StringEditWindow();
            window.Value = Items.LastOrDefault();

            var add = window.ShowDialog() == true;

            if(add)
            {
                MainListView.Items.Add(window.Value.Trim());
                Changed = true;
            }
        }

        public IEnumerable<string> Items { get { return MainListView.Items.Cast<string>(); } }

        public bool Changed { get; private set; }

        public StringCollectionEditWindow(IEnumerable<string> values)
        {
            InitializeComponent();

            if (values != null)
                foreach (var val in values)
                    MainListView.Items.Add(val);
        }

        private void RemoveItem_Click(object sender, RoutedEventArgs e)
        {
            string item = ((sender as Button)?.DataContext as string);

            if (item == null)
                return;

            var yes = MessageBox.Show("Удалить '" + item + "' ?", "Удаление домена", MessageBoxButton.YesNo) == MessageBoxResult.Yes;

            if(yes)
            {
                MainListView.Items.Remove(item);

                Changed = true;
            }
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            string item = ((sender as Button)?.DataContext as string);

            if (item == null)
                return;

            var window = new StringEditWindow();
            window.Value = item;

            var result = window.ShowDialog();
            

            if(result == true && window.HasEdited)
            {
                MainListView.Items.Remove(item);
                MainListView.Items.Add(window.Value);

                Changed = true;
            }
        }
    }
}
