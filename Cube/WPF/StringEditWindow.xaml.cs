using RTCManifestGenerator.Commands;
using System;
using System.Windows;

namespace RTCManifestGenerator.Windows
{
    public delegate bool StringValidationDelegate(string value);

    /// <summary>
    /// Логика взаимодействия для StringEditWindow.xaml
    /// </summary>
    public partial class StringEditWindow : Window
    {
        string m_startValue;

        readonly Action<string> m_saveDelegate;
        readonly StringValidationDelegate m_validateDelegate;
        readonly Action<string> m_validationFailDelegate;

        public ActionCommand SaveCommand { get { return new ActionCommand(Save); } }

        public bool HasEdited { get { return !EditTextBox.Text.Equals(m_saveDelegate); } }

        public string Value
        {
            get { return EditTextBox.Text; }
            set
            {
                EditTextBox.Text = value;
                m_startValue = value;
            }
        }

        private void Save()
        {
            try
            {
                if (m_validateDelegate != null)
                {
                    bool valid = m_validateDelegate.Invoke(EditTextBox.Text);

                    if (!valid)
                    {
                        if (m_validationFailDelegate != null)
                            m_validationFailDelegate.Invoke(EditTextBox.Text);

                        return; //если не валидно - не закрываем окно раньше времени
                    }
                }

                if (m_saveDelegate != null)
                    m_saveDelegate.Invoke(EditTextBox.Text);

                DialogResult = true;

            }
            catch (Exception ex)
            {
                StaticMethods.LogAndShow("Возникло исключение при сохранении значения", ex);
            }

            this.Close();
        }

        public StringEditWindow(Action<string> saveMethod = null, StringValidationDelegate validateMethod = null, Action<string> validationFailDelegate = null)
        {
            m_saveDelegate = saveMethod;
            m_validateDelegate = validateMethod;
            m_validationFailDelegate = validationFailDelegate;

            InitializeComponent();
        }
    }
}
