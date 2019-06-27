using RTCManifestGenerator.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace RTCManifestGenerator.Windows
{
    /// <summary>
    /// Interaction logic for ArgsHelpWindow.xaml
    /// </summary>
    public partial class ArgsHelpWindow : Window
    {
        /// <summary>
        /// Инфо о входных аргументах приложения
        /// </summary>
        public IEnumerable<ArgumentDataMadel> ArgumentsCollection { get { return StartupArgs.GetAttributes().Select(attr => new ArgumentDataMadel(attr)); } }

        public ArgsHelpWindow()
        {
            InitializeComponent();
        }

        
    }
}
