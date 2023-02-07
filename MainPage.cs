namespace EasySave
{
    internal class MainPage
    {
        static void Main(string[] args)
        {
            // The option user have to choose
            string option;
            CompleteSave completeSave;
            DiffSave diffSave;
            string choice;
            string validPath;
            string invalidPath;
            string lang = "fr"; // language fr by default

            Console.WriteLine(Language.Welcome.get("ang"));
            do
            {
                Console.WriteLine(Language.Menu.Choice.get(lang));
                Console.WriteLine(Language.Menu.CompleteSave.get(lang));
                Console.WriteLine(Language.Menu.DiffSave.get(lang));
                Console.WriteLine(Language.Menu.ChangeLanguage.get(lang));
                Console.WriteLine(Language.Menu.Exit.get(lang));

                option = Console.ReadLine();
                switch (option)
                    {
                        case "1":
                            Console.Clear();
                            completeSave = new CompleteSave();

                            Console.Write(Language.Selected.CompleteSave.get(lang)); 
                            Console.WriteLine(Language.BackupName.get(lang));
                            completeSave.Appellation = Console.ReadLine();

                            while (!System.IO.Directory.Exists(completeSave.SourcePath))
                            {
                                Console.WriteLine(Language.SourcePath.get(lang));
                                completeSave.SourcePath = Console.ReadLine();
                                if (!System.IO.Directory.Exists(completeSave.SourcePath))
                                    GeneralTools.WriteWarningMessage(Language.Error.SourceExistence.get(lang));
                            }

                            while (!System.IO.Directory.Exists(completeSave.TargetPath))
                            {
                                Console.WriteLine(Language.TargetPath.get(lang));
                                completeSave.TargetPath = Console.ReadLine();

                                // Check if directory exist
                                if (!System.IO.Directory.Exists(completeSave.TargetPath))
                                {

                                    validPath = GeneralTools.GetValidDirectoryPath(completeSave.TargetPath);

                                    invalidPath = completeSave.TargetPath.Substring(validPath.Length,
                                        completeSave.TargetPath.Length - validPath.Length);

                                    GeneralTools.WriteWarningMessage(Language.Error.TargetPath.get(lang));
                                    Console.WriteLine(Language.CreationDirectory.get(lang, validPath, invalidPath));

                                    do
                                    {
                                        choice = Console.ReadLine();
                                        if (choice != "y" && choice != "n")
                                        {
                                            GeneralTools.WriteWarningMessage(Language.Error.Option.get(lang));
                                            Console.WriteLine(Language.YesNo.get(lang));
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
                                    GeneralTools.WriteWarningMessage(Language.Error.Access.get(lang));
                                } catch (System.IO.IOException i)
                                {
                                    GeneralTools.WriteWarningMessage(Language.Error.Access.get(lang));
                                }
                            });
                            break;

                        case "2":
                            Console.Clear();
                            diffSave = new DiffSave();

                            Console.Write(Language.Selected.CompleteSave.get(lang));
                            Console.WriteLine(Language.BackupName.get(lang));
                            diffSave.Appellation = Console.ReadLine();

                            while (!System.IO.Directory.Exists(diffSave.SourcePath))
                            {
                                Console.WriteLine(Language.SourcePath.get(lang));
                                diffSave.SourcePath = Console.ReadLine();
                                if(!System.IO.Directory.Exists(diffSave.SourcePath)) 
                                    GeneralTools.WriteWarningMessage(Language.Error.SourceExistence.get(lang));
                            }

                            while (!System.IO.Directory.Exists(diffSave.TargetPath))
                            {
                                Console.WriteLine(Language.TargetPath.get(lang));
                                diffSave.TargetPath = Console.ReadLine();

                                // Check if directory exist
                                if (!System.IO.Directory.Exists(diffSave.TargetPath))
                                    GeneralTools.WriteWarningMessage(Language.Error.TargetExistence.get(lang));
                            }

                            Task.Run(() =>
                            {
                                try
                                {
                                    diffSave.RepositorySave();
                                } catch (System.UnauthorizedAccessException e)
                                {
                                    GeneralTools.WriteWarningMessage(Language.Error.Access.get(lang));
                                } catch (System.IO.IOException i)
                                {
                                    GeneralTools.WriteWarningMessage(Language.Error.Access.get(lang));
                                }
                            });
                            Console.Clear();
                            break;
                        
                        case "3":
                            lang = changeLang(lang);
                            break;
            
                        default:
                            Console.Clear();
                                GeneralTools.WriteWarningMessage(Language.Error.Option.get(lang));
                            break;
                    }
            } while ((option != "e"));
            Console.WriteLine(Language.GoodBye.get(lang));
        }

        internal static string changeLang(string language)
        {
            return "fr" == language ? "ang" : "fr";
        }
    }
}