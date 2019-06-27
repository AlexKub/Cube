using RTCManifestGenerator.Commands;
using System;
using System.Windows;

namespace RTCManifestGenerator.Windows
{
    /// <summary>
    /// Логика взаимодействия для RTCServerView.xaml
    /// </summary>
    public partial class RTCServerView : Window
    {
        static string startupValue;

        public static readonly DependencyProperty dp_CurentDomainProperty = DependencyProperty.Register(nameof(CurentDomain), typeof(string), typeof(RTCServerView), new FrameworkPropertyMetadata()
        {
            DefaultValue = default(string),
            PropertyChangedCallback = (d, e) =>
            {
                var depObj = (d as RTCServerView);

                if (e.OldValue != e.NewValue)
                    depObj.DomainChanged = (startupValue != null) && (!startupValue.Equals((string)e.NewValue, StringComparison.InvariantCultureIgnoreCase));
            }
        });
        public string CurentDomain { get { return (string)GetValue(dp_CurentDomainProperty); } set { SetValue(dp_CurentDomainProperty, value); } }

        public static readonly DependencyProperty dp_DomainChangedProperty = DependencyProperty.Register(nameof(DomainChanged), typeof(bool), typeof(RTCServerView), new FrameworkPropertyMetadata() { DefaultValue = default(bool) });
        public bool DomainChanged { get { return (bool)GetValue(dp_DomainChangedProperty); } set { SetValue(dp_DomainChangedProperty, value); } }

        public RelayCommand<Window> SaveCommand { get { return new RelayCommand<Window>(Save); } }
        private void Save(Window window)
        {
            try
            {
                var config = RTC_Config.LoadActiveConfig();

                if (DomainChanged)
                {
                    config.DomainName = CurentDomain;
                    config.Save();
                }

                if (window != null)
                    window.Close();
            }
            catch (Exception ex)
            {
                StaticMethods.LogAndShow("Возникло исключение при сохранении RTC-сервера", ex);
            }
        }

        public RTCServerView()
        {
            InitializeComponent();

            startupValue = RTC_Config.LoadActiveConfig().DomainName;
            CurentDomain = startupValue;
        }



        MG_Settings GetSettings()
        {
            return MG_Settings.Load();
        }
    }
}
