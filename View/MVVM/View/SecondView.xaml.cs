using EasySave;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using View.MVVM.View;
using static View.MVVM.View.HomeView;
using static View.MVVM.View.SecondView;

namespace View.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour SecondView.xaml
    /// </summary>
    public partial class SecondView : UserControl
    {
        private static List<Jobs> jobsProperties  = new List<Jobs>();
        public static List<Jobs> JobsProperties { get => jobsProperties; set => jobsProperties = value; }

        public SecondView()
        {
            InitializeComponent();
        }

       

        private void CompleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(BackUpNameBox.Text))
            {
                ErrorNameLabel.Content = "Le champ BackUp Name est requis.";
                ErrorNameLabel.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrEmpty(SoucePathBox.Text))
            {
                ErrorSourceLabel.Content = "Le champ Source Path est requis.";
                ErrorSourceLabel.Visibility = Visibility.Visible;
                return;
            }
            else if (!Directory.Exists(SoucePathBox.Text))
            {
                ErrorSourceLabel.Content = "Le répertoire Source Path n'existe pas.";
                ErrorSourceLabel.Visibility = Visibility.Visible;
                return;
            }

            if (string.IsNullOrEmpty(TargetPathBox.Text))
            {
                ErrorTargetLabel.Content = "Le champ Target Path est requis.";
                ErrorTargetLabel.Visibility = Visibility.Visible;
                return;
            }
            else if (!Directory.Exists(TargetPathBox.Text))
            {
                ErrorTargetLabel.Content = "Le répertoire Target Path n'existe pas.";
                ErrorTargetLabel.Visibility = Visibility.Visible;
                return;
            }

            HomeView.Jobs job = new HomeView.Jobs
            {
                appellation = BackUpNameBox.Text,
                sourcePath = SoucePathBox.Text,
                targetPath = TargetPathBox.Text
            };

            SuccesLabel.Content = "Les données ont été sauvegardées avec succès.";
            SuccesLabel.Visibility = Visibility.Visible;


            jobsProperties.Add(job);

            HomeView.Saves.Add(new CompleteSave(job.appellation, job.sourcePath, job.targetPath));
            HomeView.CompleteSaveNumber = HomeView.CompleteSaveNumber + 1;

        }

        private void BackUpNameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorNameLabel.Visibility = Visibility.Collapsed;
            SuccesLabel.Visibility = Visibility.Collapsed;

        }

        private void SoucePathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorSourceLabel.Visibility = Visibility.Collapsed;
            SuccesLabel.Visibility = Visibility.Collapsed;


        }

        private void TargetPathBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ErrorTargetLabel.Visibility = Visibility.Collapsed;
            SuccesLabel.Visibility = Visibility.Collapsed;


        }

       private void source_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SoucePathBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

        private void target_Folder_Button_Click(object sender, RoutedEventArgs e)
        {
            var folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TargetPathBox.Text = folderBrowserDialog.SelectedPath;
            }
        }

    }
}

