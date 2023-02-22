using System.Windows;
using System.Windows.Controls;

namespace View.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {

        public static bool option = false;
        public SettingsView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            EasySaveApp.Properties.Settings.Default.languageCode = "fr-FR";
            EasySaveApp.Properties.Settings.Default.Save();
            MessageBox.Show("Reload application for changes to take effect");
            
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            EasySaveApp.Properties.Settings.Default.languageCode = "en-US";
            EasySaveApp.Properties.Settings.Default.Save();
            MessageBox.Show("Redémarrer l'application pour que les changements prennent effet");
        }
    }
}
