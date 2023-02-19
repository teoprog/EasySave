using EasySave;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public string teotest;
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

            for (int i = 0; i < Saves.Count; i++)
            {
                if (!Equals(Saves[i], null))
                {
                    try
                    {

                        if (Saves[i] is CompleteSave)
                        {
                            (Saves[i] as CompleteSave)?.RepositorySave();
                        }
                        else if (Saves[i] is DiffSave)
                        {
                            (Saves[i] as DiffSave).RepositorySave();
                        }
                    }
                    catch (Exception h)
                    {
                        Console.WriteLine(h);

                    }
                }
                Saves[i] = null;
                CompleteSaveNumber = 0;
                DiffSaveNumber = 0;
                JobsNumber = 0;


            };
        }
    }
}
