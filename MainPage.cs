using System;
using System.IO;
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

                                // Check if directory exist
                                if (!System.IO.Directory.Exists(completeSave.TargetPath))
                                {
                                    if (completeSave.TargetPath.StartsWith(completeSave.SourcePath))
                                    {
                                        Console.WriteLine("ooffoof");
                                        GeneralTools.WriteWarningMessage("You can't make a save inside your source file");
                                    }
                                    else
                                    {
                                        validPath = GeneralTools.GetValidDirectoryPath(completeSave.TargetPath);

                                        invalidPath = completeSave.TargetPath.Substring(validPath.Length,
                                            completeSave.TargetPath.Length - validPath.Length);

                                        GeneralTools.WriteWarningMessage("Target Path does not exist.\n");
                                        Console.WriteLine("Would you like to create : \n"
                                                          + $"\"{invalidPath}\" folders in \"{validPath}\"\n\n yes(y) - no(n)");

                                        do
                                        {
                                            choice = Console.ReadLine();
                                            if (choice != "y" || choice != "n")
                                            {
                                                GeneralTools.WriteWarningMessage("This option does not exist");
                                                Console.WriteLine("Choose between : yes(y) - no(n)");
                                            }

                                        } while (choice != "y" && choice != "n");

                                        if (choice == "y") System.IO.Directory.CreateDirectory(completeSave.TargetPath);
                                    }

                                }
                                else
                                {
                                    if (completeSave.TargetPath.Equals(completeSave.SourcePath) ||
                                        completeSave.TargetPath.StartsWith(completeSave.SourcePath))
                                    {
                                        GeneralTools.WriteWarningMessage(
                                            "You can't make a save inside your source file");
                                        completeSave.TargetPath = "";
                                    }
                                }
                            }

                            completeSave.RepositorySave();
                            throw new Exception("kks");
                            Console.Clear();

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