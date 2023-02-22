using EasySaveApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace View.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public static bool option = false;

        public Brush Red { get; private set; }

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

        private bool IsNumber(string text)
        {
            return int.TryParse(text, out int result);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsNumber(e.Text);
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(SizeSaveTextBox.Text))
            {
                SuccesLabel6.Visibility = Visibility.Collapsed;
                SuccesLabel8.Visibility = Visibility.Collapsed;
                SuccesLabel3.Visibility = Visibility.Collapsed;
                SuccesLabel6bis.Visibility = Visibility.Collapsed;
                SuccesLabel8bis.Visibility = Visibility.Collapsed;

                SuccesLabel6bis.Content = "Veuillez entrer une valeur Non Nulle";
                SuccesLabel6bis.Visibility = Visibility.Visible;

                
                return;
            }
            else
            {
                MaxParallelSize maxsizeparallel = new MaxParallelSize();
                maxsizeparallel.Modify(long.Parse(SizeSaveTextBox.Text));

                SuccesLabel6.Visibility = Visibility.Collapsed;
                SuccesLabel8.Visibility = Visibility.Collapsed;
                SuccesLabel3.Visibility = Visibility.Collapsed;
                SuccesLabel6bis.Visibility = Visibility.Collapsed;
                SuccesLabel8bis.Visibility = Visibility.Collapsed;

                SuccesLabel6.Content = "La taille maximale de sauvegarde a bien été modifiée";
                SuccesLabel6.Visibility = Visibility.Visible;

                SizeSaveTextBox.Text = "";
            }
            
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(NetworkLoadBox.Text))
            {
                SuccesLabel6.Visibility = Visibility.Collapsed;
                SuccesLabel8.Visibility = Visibility.Collapsed;
                SuccesLabel3.Visibility = Visibility.Collapsed;
                SuccesLabel6bis.Visibility = Visibility.Collapsed;
                SuccesLabel8bis.Visibility = Visibility.Collapsed;


                SuccesLabel8bis.Content = "Veuillez entrer une valeur Non Nulle";
                SuccesLabel8bis.Visibility = Visibility.Visible;


                return;
            }
            else
            {
                NetworkLoad networkload = new NetworkLoad();
                networkload.Modify(long.Parse(NetworkLoadBox.Text));

                SuccesLabel6.Visibility = Visibility.Collapsed;
                SuccesLabel8.Visibility = Visibility.Collapsed;
                SuccesLabel3.Visibility = Visibility.Collapsed;
                SuccesLabel6bis.Visibility = Visibility.Collapsed;
                SuccesLabel8bis.Visibility = Visibility.Collapsed;

                SuccesLabel8.Content = "Les Modification de surcharge Reseau on bien été pris en compte";
                SuccesLabel8.Visibility = Visibility.Visible;

                NetworkLoadBox.Text = "";
            }

            

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            LogJsonOrXml xml = new LogJsonOrXml();
            xml.ToXml();

            SuccesLabel6.Visibility = Visibility.Collapsed;
            SuccesLabel8.Visibility = Visibility.Collapsed;
            SuccesLabel3.Visibility = Visibility.Collapsed;
            SuccesLabel6bis.Visibility = Visibility.Collapsed;
            SuccesLabel8bis.Visibility = Visibility.Collapsed;

            SuccesLabel3.Content = "Les log files ont bien ete changer en type XML";
            SuccesLabel3.Visibility = Visibility.Visible;
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            LogJsonOrXml json = new LogJsonOrXml();
            json.ToJson();

            SuccesLabel6.Visibility = Visibility.Collapsed;
            SuccesLabel8.Visibility = Visibility.Collapsed;
            SuccesLabel3.Visibility = Visibility.Collapsed;
            SuccesLabel6bis.Visibility = Visibility.Collapsed;
            SuccesLabel8bis.Visibility = Visibility.Collapsed;

            SuccesLabel3.Content = "Les log files ont bien ete changer en type JSON";
            SuccesLabel3.Visibility = Visibility.Visible;


        }
    }
}
