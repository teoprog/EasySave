using EasySave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace View.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour HomeView.xaml
    /// </summary>
    public partial class HomeView : UserControl
    {

        public class Jobs
        {
            public  string appellation { get; set; }
            public  string sourcePath { get; set; }
            public  string targetPath { get; set; }

        }

        public static List<ISave> Saves { get; set; } = new List<ISave>();

        public int JobsNumber;
        public static int CompleteSaveNumber = 0;
        public static int DiffSaveNumber = 0;

        public HomeView()
        {
            InitializeComponent();
            JobsGrid.ItemsSource = SecondView.JobsProperties;
            JobsNumber = Saves.Count;
            JobsnumberTextBlock.Text = JobsNumber.ToString();
            CompleteSaveNumberTextBlock.Text = CompleteSaveNumber.ToString();
            DiffSaveNumberTextBlock.Text = DiffSaveNumber.ToString();

        }


        public void JobsButton_Click(object sender, RoutedEventArgs e)
        {
            Thread[] threads = new Thread[Saves.Count];

            for (int i = 0; i < Saves.Count; i++)
            {
                ISave save = Saves[i];
                if (save is CompleteSave)
                {
                    threads[i] = new Thread(() => (save as CompleteSave)?.RepositorySave());
                }
                else if (save is DiffSave)
                {
                    threads[i] = new Thread(() => (save as DiffSave)?.RepositorySave());
                }
                threads[i].Start();
            }
                                
            // clear the List and set the jobs to 0
            Saves = new List<ISave>();
            CompleteSaveNumber = 0;
            DiffSaveNumber = 0;
            JobsNumber = 0;
        }
    }
}
