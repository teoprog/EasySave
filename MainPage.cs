using System;
using System.Runtime.InteropServices;

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
            string text;
            string choice;
            string validPath;
            string invalidPath;
            
            
            completeSave = new CompleteSave();
            diffSave = new DiffSave();
            
            Console.WriteLine(completeSave.Appellation + "       " + completeSave.SourcePath + "       " + completeSave.TargetPath + "      " + completeSave.IsCompleteSave
            );
            

            Console.WriteLine("Welcome on EasySave\n");
            do
            {
                WriteHomeMessage();
                option = Console.ReadLine();
                switch (option)
                    {
                        case "1":
                            Console.Clear();
                            Console.WriteLine("Complete Save Creation\n" +
                                              "Name of the backup :");
                            completeSave.Appellation = Console.ReadLine();

                            while (!System.IO.Directory.Exists(completeSave.SourcePath))
                            {
                                Console.WriteLine("\nSource Path :");
                                completeSave.SourcePath = Console.ReadLine();
                                
                                if(!System.IO.Directory.Exists(completeSave.SourcePath)) 
                                    GeneralTools.WriteWarningMessage("Source path does not exist");
                            }

                            while (!System.IO.Directory.Exists(completeSave.TargetPath))
                            {
                                Console.WriteLine("\nTarget Path :");
                                completeSave.TargetPath = Console.ReadLine();
                                
                                if (!System.IO.Directory.Exists(completeSave.TargetPath))
                                {
                                    validPath = GeneralTools.GetValidDirectoryPath(completeSave.TargetPath);
                                    invalidPath = completeSave.TargetPath.Substring(validPath.Length,
                                            completeSave.TargetPath.Length - validPath.Length);

                                    Console.WriteLine("jkfskdkfkf");
                                    GeneralTools.WriteWarningMessage(
                                        "Target Path does not exist : \n would you like to create : \n"
                                        + $"\"{invalidPath}\" folders in \"{validPath}\" \n yes(y) - no(n)");

                                    do
                                    {
                                        choice = Console.ReadLine();
                                        if (choice != "y" || choice != "n")
                                            GeneralTools.WriteWarningMessage("Choose between : yes(y) - no(n)");
                                    } while (choice != "y" || choice != "n");

                                    if (choice == "y") System.IO.Directory.CreateDirectory(completeSave.TargetPath);
                                }
                            }
                            completeSave.RepositorySave();
                            break;

                        case "2":
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