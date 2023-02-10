using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Easysavegraphique2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void parameter_of_cryptage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void blacklist_software_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void langue_Checked(object sender, RoutedEventArgs e)
        {
        }
        private void langue_Click(object sender, RoutedEventArgs e)
        {
            if (sender is ToggleButton toggleButton)
            {
                bool isChecked = toggleButton.IsChecked ?? false;

                if (isChecked)
                {
                    toggleButton.Content = "Anglais";
                    parameter_of_cryptage.Content = "Paramètre de cryptage";
                    blacklist_software.Content = "Logiciels à bannir";
                    exit.Content = "FERMER";
                    save_Name.Content = "Nom de la sauvegarde:";
                    source_Folder_Path.Content = "Chemin du dossier source:";
                    target_Folder_Path.Content = "Chemin du dossier cible:";
                    mirror_Folder_Path.Content = "Chemin de la sauvegarde complète:";
                    mirror_Backup.Content = "Sauvegarde Complète";
                    differential_Backup.Content = "Sauvegarde différentielle";
                    add_Save.Content = "Ajouter une sauvegarde";
                    backup_Button.Content = "Démarrer la sauvegarde";
                }
                else
                {
                    toggleButton.Content = "Français";
                    parameter_of_cryptage.Content = "Parameter of cryptage";
                    blacklist_software.Content = "Software Blacklist";
                    exit.Content = "EXIT";
                    save_Name.Content = "Save Name:";
                    source_Folder_Path.Content = "Source Folder Path:";
                    target_Folder_Path.Content = "Target Folder Path:";
                    mirror_Folder_Path.Content = "Mirror Folder Path:";
                    mirror_Backup.Content = "Mirror Backup";
                    differential_Backup.Content = "Differential Backup";
                    add_Save.Content = "Add a save";
                    backup_Button.Content = "Start the backup";


                }
            }
        }

        private void source_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                source_Folder_Path_Tb.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void target_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                target_Folder_Path_Tb.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void mirror_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                mirror_Folder_Path_Tb.Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
