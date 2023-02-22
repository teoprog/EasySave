using EasySave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace View.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour HomeView.xaml
    /// </summary>
    public class Jobs
    {
        public string appellation { get; set; }
        public string sourcePath { get; set; }
        public string targetPath { get; set; }

    }

    public partial class HomeView : UserControl, INotifyPropertyChanged
    {
        private static Mutex Complete = new Mutex();
        private static Mutex Diff = new Mutex();

        public event PropertyChangedEventHandler? PropertyChanged;
        public static List<ISave> Saves { get; set; } = new List<ISave>();

        public static readonly DependencyProperty JobsNumberProperty = DependencyProperty.Register(
        "JobsNumber",
        typeof(int),
        typeof(HomeView),
        new PropertyMetadata(0)
        );

        public static readonly DependencyProperty CompleteSaveProperty = DependencyProperty.Register(
        "CompleteSaveNumber",
        typeof(int),
        typeof(HomeView),
        new PropertyMetadata(0)
        );

        public static readonly DependencyProperty DiffSaveProperty = DependencyProperty.Register(
        "DiffSaveNumber",
        typeof(int),
        typeof(HomeView),
        new PropertyMetadata(0)
        );

        private int _currentProgress;

        public int CurrentProgress
        {
            get { return _currentProgress; }
            set
            {
                _currentProgress = value;
                OnPropertyChanged(nameof(CurrentProgress)); // déclenche l'événement de notification de changement de propriété
            }
        }
        public int JobsNumber
        {
            get { return (int)GetValue(JobsNumberProperty); }
            set { SetValue(JobsNumberProperty, value); }
        }
        

        public int CompleteSaveNumber
        {
            get { return (int)GetValue(CompleteSaveProperty); }
            set { SetValue(CompleteSaveProperty, value); }
        }
        public int DiffSaveNumber
        {
            get { return (int)GetValue(DiffSaveProperty); }
            set { SetValue(DiffSaveProperty, value); }
        }
        private static bool isStopped = false;

        public HomeView()
        {
            InitializeComponent();
            DataContext = this;
            JobsGrid.ItemsSource = SecondView.JobsProperties;
            JobsNumber = Saves.Count;
            CompleteSaveNumber = 0;
            DiffSaveNumber = 0;

        }




        public void JobsButton_Click(object sender, RoutedEventArgs e)
        {
            Thread[] threads = new Thread[Saves.Count];
            HomeView.isStopped = false;

            for (int i = 0; i < JobsNumber; i++)
            {
                ISave save = Saves[i];
                if (save is CompleteSave)
                {
                    threads[i] = new Thread(() =>
                    {
                        (save as CompleteSave)?.RepositorySave();

                        Dispatcher.Invoke(() =>
                        {
                            CompleteSaveNumber = CompleteSaveNumber + 1;
                        });

                        // Mettez à jour la valeur de la ProgressBar
                        Dispatcher.Invoke(() =>
                        {
                            if (JobsNumber > 0)
                                ProgressBar.Value = ((double)i / JobsNumber) * 100;
                        });
                    });
                }
                else if (save is DiffSave)
                {
                    threads[i] = new Thread(() =>
                    {
                        (save as DiffSave)?.RepositorySave();

                        Dispatcher.Invoke(() =>
                        {
                            DiffSaveNumber = DiffSaveNumber + 1;
                        });

                        // Mettez à jour la valeur de la ProgressBar
                        Dispatcher.Invoke(() =>
                        {
                            if (JobsNumber > 0)
                                ProgressBar.Value = ((double)i / JobsNumber) * 100;
                        });
                    });
                }
                threads[i].Start();
            }

            Dispatcher.Invoke(() =>
            {
                ProgressBar.Value = 0;
            });

            // clear the List and set the jobs to 0
            Saves = new List<ISave>();
            CompleteSaveNumber = 0;
            DiffSaveNumber = 0;
            JobsNumber = 0;



            for (int i = 0; i < JobsNumber; i++)
            {
                while (isStopped)
                { 
                }

            }
        }

        public void StopButton_Click(object sender, RoutedEventArgs e)
        {
            isStopped = true;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            isStopped = false;
        }

        private void BreakButton_Click(object sender, RoutedEventArgs e)
        {
            return;
        }

        
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        
    }


}
