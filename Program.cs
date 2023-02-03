using System;
using BackupApplication;

namespace ConsoleBackupApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Formulaire de sauvegarde :");
            Console.Write("Appellation : ");
            string appellation = Console.ReadLine();
            Console.Write("fichier Source : ");
            string sourceDirectory = Console.ReadLine();
            Console.Write("Fichier cible : ");
            string targetDirectory = Console.ReadLine();
            Console.Write("Type de Sauvegarde (Complete or différentielle) : ");
            string type = Console.ReadLine();

            BackupJob job = new BackupJob
            {
                Appellation = appellation,
                SourceDirectory = sourceDirectory,
                TargetDirectory = targetDirectory,

                // Type = type == "Complete" ? BackupJob.BackupType.Complete : BackupJob.BackupType.Differential
            };

            job.Save();

            Console.WriteLine("La sauvegarde de type " + type + "du fichier " + appellation + " a bien été réalisée avec succès");
        }
    }
}
