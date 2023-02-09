namespace EasySave
{
    internal class MainPage
    {
        static void Main(string[] args)
        {
            // The option user have to choose
            string option;
            ISave?[] saves = new ISave?[5];
            int saveError;
            string choice;
            string? appellation;
            string sourcePath,
                   targetPath;
            string validPath;
            string invalidPath;
            string lang = "fr"; // language fr by default

            
            Console.WriteLine(Language.Welcome.get("ang"));
            do
            {
                Console.WriteLine(Language.Menu.Choice.get(lang));
                Console.WriteLine(Language.Menu.CompleteSave.get(lang));
                Console.WriteLine(Language.Menu.DiffSave.get(lang));
                Console.WriteLine(Language.Menu.LaunchSaves.get(lang));
                Console.WriteLine(Language.Menu.ChangeLanguage.get(lang));
                Console.WriteLine(Language.Menu.Exit.get(lang));

                option = Console.ReadLine();
                switch (option)
                    {
                        case "1":
                            Console.Clear();

                            saveError = -1;
                            for(int i = 0; i < saves.Length - 1; i++) 
                            {
                                if (Object.Equals(saves[i], null))
                                {
                                    saveError = i;
                                    i = saves.Length;
                                }
                            }

                            Console.Write(Language.Selected.CompleteSave.get(lang)); 
                            if (saveError != -1)
                            {
                                Console.WriteLine(Language.BackupName.get(lang));
                                appellation = Console.ReadLine();
                                do
                                {
                                    Console.WriteLine(Language.SourcePath.get(lang));
                                    sourcePath = Console.ReadLine();
                                    if (!System.IO.Directory.Exists(sourcePath)) GeneralTools.WriteWarningMessage(Language.Error.SourceExistence.get(lang));
                                } while (!System.IO.Directory.Exists(sourcePath));

                                do
                                {
                                    Console.WriteLine(Language.TargetPath.get(lang));
                                    targetPath = Console.ReadLine();

                                    // Check if directory exist
                                    if (!System.IO.Directory.Exists(targetPath))
                                    {
                                        validPath = GeneralTools.GetValidDirectoryPath(targetPath);

                                        invalidPath = targetPath.Substring(validPath.Length,
                                            targetPath.Length - validPath.Length);

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

                                        if (choice == "y") System.IO.Directory.CreateDirectory(targetPath);
                                        Console.Clear();
                                    }
                                } while ((!System.IO.Directory.Exists(targetPath)));
                                Console.Clear();
                                saves[saveError] = new CompleteSave(appellation, sourcePath, targetPath);
                            }
                            else
                            {
                                Console.Clear();
                                GeneralTools.WriteWarningMessage(Language.Error.SavesOverflow.get(lang));
                            }
                            
                            break;

                        case "2":
                            Console.Clear();
                            saveError = -1;
                            for(int i = 0; i < saves.Length - 1; i++) 
                            {
                                if (Object.Equals(saves[i], null))
                                {
                                    saveError = i;
                                    i = saves.Length;
                                }
                            }

                            Console.Write(Language.Selected.CompleteSave.get(lang));

                            if (saveError != -1)
                            {
                                Console.WriteLine(Language.BackupName.get(lang));
                                appellation = Console.ReadLine();

                                do
                                {
                                    Console.WriteLine(Language.SourcePath.get(lang));
                                    sourcePath = Console.ReadLine();
                                    if (!System.IO.Directory.Exists(sourcePath))
                                        GeneralTools.WriteWarningMessage(Language.Error.SourceExistence.get(lang));
                                } while (!System.IO.Directory.Exists(sourcePath));

                                do
                                {
                                    Console.WriteLine(Language.TargetPath.get(lang));
                                    targetPath = Console.ReadLine();

                                    // Check if directory exist
                                    if (!System.IO.Directory.Exists(targetPath))
                                        GeneralTools.WriteWarningMessage(Language.Error.TargetExistence.get(lang));
                                } while (!System.IO.Directory.Exists(targetPath));

                                saves[saveError] = new DiffSave(appellation, sourcePath, targetPath);
                                Console.Clear();
                            }
                            else
                            {
                                Console.Clear();
                                GeneralTools.WriteWarningMessage(Language.Error.SavesOverflow.get(lang));
                            }
                            break;
                        
                        case "3":
                            Task.Run(() =>
                            {
                                for(int i = 0; i < saves.Length - 1; i++) 
                                {
                                    if (!Object.Equals(saves[i], null))
                                    {
                                            if (saves[i] is CompleteSave)
                                            {
                                                try
                                                {
                                                    (saves[i] as CompleteSave).RepositorySave();
                                                } catch (System.UnauthorizedAccessException e)
                                                {
                                                    (saves[i] as CompleteSave).UpdateState("ERROR");
                                                    GeneralTools.WriteWarningMessage( (saves[i] as CompleteSave).Appellation + Language.Error.Access.get(lang));
                                                } catch (System.IO.IOException r)
                                                {
                                                    (saves[i] as CompleteSave).UpdateState("ERROR");
                                                    GeneralTools.WriteWarningMessage((saves[i] as CompleteSave).Appellation + Language.Error.Access.get(lang));
                                                } 
                                            } else if (saves[i] is DiffSave)
                                            {
                                                try
                                                {
                                                    (saves[i] as DiffSave).RepositorySave();
                                                } catch (System.UnauthorizedAccessException e)
                                                {
                                                    (saves[i] as DiffSave).UpdateState("ERROR");
                                                    GeneralTools.WriteWarningMessage((saves[i] as CompleteSave).Appellation + Language.Error.Access.get(lang));
                                                } catch (System.IO.IOException r)
                                                {
                                                    (saves[i] as DiffSave).UpdateState("ERROR");
                                                    GeneralTools.WriteWarningMessage((saves[i] as CompleteSave).Appellation + Language.Error.Access.get(lang));
                                                }
                                            }
                                    }
                                    saves[i] = null;
                                }
                            });
                            ;
                            Console.Clear();
                            Console.WriteLine("Save progress... Enter for continue");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        
                        case "4":
                            lang = changeLang(lang);
                            Console.Clear();
                            break;
            
                        default:
                            Console.Clear();
                            GeneralTools.WriteWarningMessage(Language.Error.Option.get(lang));
                            break;
                    }
            } while ((option != "e"));
            Console.WriteLine(Language.GoodBye.get(lang));
        }

        private static string changeLang(string language)
        {
            return "fr" == language ? "ang" : "fr";
        }
    }
}