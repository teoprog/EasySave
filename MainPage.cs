namespace EasySave
{
    internal class MainPage
    {

        static void Main(string[] args)
        {
            // The option user have to choose
            string option;
            int code;
            CompleteSave completeSave;
            DiffSave diffSave;
            string choice;
            string validPath;
            string invalidPath;
            

            Console.WriteLine("Welcome on EasySave\n");
            do
            {
                WriteHomeMessage();
                option = Console.ReadLine();
                switch (option)
                    {
                        case "1":
                            Console.Clear();
                            completeSave = new CompleteSave();

                            Console.WriteLine("Complete Save Creation\n" +
                                              "Name of the backup :");
                            completeSave.Appellation = Console.ReadLine();

                            while (!System.IO.Directory.Exists(completeSave.SourcePath))
                            {
                                Console.WriteLine("\nSource Path :");
                                completeSave.SourcePath = Console.ReadLine();
                                if (!System.IO.Directory.Exists(completeSave.SourcePath))
                                    GeneralTools.WriteWarningMessage("Source path does not exist");
                            }

                            while (!System.IO.Directory.Exists(completeSave.TargetPath))
                            {
                                Console.WriteLine("\nTarget Path :");
                                completeSave.TargetPath = Console.ReadLine();

                                // Check if directory exist
                                if (!System.IO.Directory.Exists(completeSave.TargetPath))
                                {

                                    validPath = GeneralTools.GetValidDirectoryPath(completeSave.TargetPath);

                                    invalidPath = completeSave.TargetPath.Substring(validPath.Length,
                                        completeSave.TargetPath.Length - validPath.Length);

                                    GeneralTools.WriteWarningMessage("Target Path does not exist.\n");
                                    Console.WriteLine("Would you like to create : \n"
                                                      + $"\"{invalidPath}\" folders in :\n\"{validPath}\"\n\n yes(y) - no(n)");

                                    do
                                    {
                                        choice = Console.ReadLine();
                                        if (choice != "y" && choice != "n")
                                        {
                                            GeneralTools.WriteWarningMessage("This option does not exist");
                                            Console.WriteLine("Choose between : yes(y) - no(n)");
                                        }

                                    } while (choice != "y" && choice != "n");

                                    if (choice == "y") System.IO.Directory.CreateDirectory(completeSave.TargetPath);
                                }
                            }

                            Task.Run(() =>
                            {
                                try
                                {
                                    completeSave.RepositorySave();
                                } catch (System.UnauthorizedAccessException e)
                                {
                                    GeneralTools.WriteWarningMessage("Problem in save, some file access have been refused.");
                                } catch (System.IO.IOException i)
                                {
                                    GeneralTools.WriteWarningMessage("Problem in save, some file access have been refused.");
                                }
                            });
                            break;

                        case "2":
                            Console.Clear();
                            diffSave = new DiffSave();
                            
                            Console.WriteLine("Complete Save Creation\n" +
                                              "Name of the backup :");
                            diffSave.Appellation = Console.ReadLine();

                            while (!System.IO.Directory.Exists(diffSave.SourcePath))
                            {
                                Console.WriteLine("\nSource Path :");
                                diffSave.SourcePath = Console.ReadLine();
                                if(!System.IO.Directory.Exists(diffSave.SourcePath)) 
                                    GeneralTools.WriteWarningMessage("Source path does not exist");
                            }

                            while (!System.IO.Directory.Exists(diffSave.TargetPath))
                            {
                                Console.WriteLine("\nTarget Path :");
                                diffSave.TargetPath = Console.ReadLine();

                                // Check if directory exist
                                if (!System.IO.Directory.Exists(diffSave.TargetPath))
                                    GeneralTools.WriteWarningMessage("You can't do a differential save without target folder. Retry !");
                            }

                            Task.Run(() =>
                            {
                                try
                                {
                                    diffSave.RepositorySave();
                                } catch (System.UnauthorizedAccessException e)
                                {
                                    GeneralTools.WriteWarningMessage("Problem in save, some file access have been refused.");
                                } catch (System.IO.IOException i)
                                {
                                    GeneralTools.WriteWarningMessage("Problem in save, some file access have been refused.");
                                }
                            });
                            Console.Clear();
                            Console.WriteLine("Sauvegarde diff bien effectuée");
                            
                            break;
                            
                            break;
            
                        default:
                            Console.Clear();
                                GeneralTools.WriteWarningMessage("Retry ! Please select a valid option");
                            break;
                    }
            } while ((option != "e"));
            Console.WriteLine("Thanks for using our software");
        }

        internal static void WriteHomeMessage()
        {
            var option_names = new
            {
                CompleteSave = "1- Complete Save",
                DiffSave = "2- Differential Save (save only modified files)",
                Exit = "e- For exit"
            };
            
            Console.WriteLine("To get starting choose a save you want to do :\n"
                              + $"{option_names.CompleteSave}\n{option_names.DiffSave}"
                              + $"\n{option_names.Exit}\n");
        }
    }
}