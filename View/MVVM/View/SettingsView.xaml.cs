using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View;

namespace View.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SettingsView.xaml
    /// </summary>
    public partial class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();
        }
        
        

        private void ChekBox1_Click(object sender, RoutedEventArgs e)
        {

            if (MainWindow.option == false)
            {
                /*HomeButton.Text = "Accueil";
                HomeCompleteBackupButton.Text = "Sauvegarde complete";
                HomeDifferentialBackupButton.Text = "Sauvegarde Sequentielle";
                HomeBlackListSoftwareButton.Text = "Logiciels Metier";
                HomeSettingsButton.Text = "Parametres";
                HomeAboutUsButton.Text = "A propos";
                HomeDashBoardCompleteSaves.Text = "Sauvegarde Complete";
                HomeDashBoardDifferentialSaves.Text = "Sauvegarde Sequentielle";
                HomeTableTitleName.Text = "Nom";
                HomeTableTitleSource.Text = "Source";
                HomeTableTitleTarget.Text = "Destination";
                Homejobsbutton.Text = "Envoyer";
                SaveTitleName.Text = "Nom de la Sauvegarde";
                SaveTitleSource.Text = "Repertoire Source";
                SaveTitleTarget.Text = "Repertoire de Destination";
                SaveSendButton.Text = "Envoyer";*/
                Settingstitle.Text = "Parametres";
                SettingsLanguage.Text = "Langue";
                SettingsLogFiles.Text = "Extension des fichier Logs";
                SettingsEncryptingTitle.Text = "Chiffrement";
                SettingsSizeSave.Text = "Taille Maximale d'une Save";

                MainWindow.option = true;
            }
            else if (MainWindow.option == true)
            {
               /*HomeButton = "Home";
               HomeCompleteBackupButton = "Complete Saves";
               HomeDifferentialBackupButton = "Differential Saves";
               HomeBlackListSoftwareButton = "BlackList Software";
               HomeSettingsButton = "Settings";
               HomeAboutUsButton = "About Us";
               HomeDashBoardCompleteSaves = "Complete Saves";
               HomeDashBoardDifferentialSaves = "Differential Saves";
               HomeTableTitleName = "Name";
               HomeTableTitleTarget = "Target";
               Homejobsbutton = "Send";
               SaveTitleName = "BackUp Name";
               SaveTitleSource = "Source Path";
               SaveTitleTarget = "Target Path";
               SaveSendButton = "Send";*/
               Settingstitle.Text = "Settings";
               SettingsLanguage.Text = "Language";
               SettingsLogFiles.Text = "Extension of Logs files";
               SettingsEncryptingTitle.Text = "Encrypting Settings";
               SettingsSizeSave.Text = "Size Max of a Save";

                MainWindow.option = false;
            }
        }
    }
}
