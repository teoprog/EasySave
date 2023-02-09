namespace EasySave
{
    internal static class MainPage
    {
        private static void Main()
        {
            // The option user have to choose
            string? option;
            ISave?[] saves = new ISave?[5];
            string lang = "fr"; // language fr by default

            
            Console.WriteLine(Language.Welcome.Get("ang"));
            do
            {
                Console.WriteLine(Language.Menu.Choice.Get(lang));
                Console.WriteLine(Language.Menu.CompleteSave.Get(lang));
                Console.WriteLine(Language.Menu.DiffSave.Get(lang));
                Console.WriteLine(Language.Menu.LaunchSaves.Get(lang));
                Console.WriteLine(Language.Menu.ChangeLanguage.Get(lang));
                Console.WriteLine(Language.Menu.Exit.Get(lang));

                option = Console.ReadLine();
                switch (option)
                    {
                        case "1":
                            saves = Champ(saves, option, lang);
                            break;

                        case "2":
                            saves = Champ(saves, option, lang);
                            break;
                        
                        case "3":
                            Task.Run(() =>
                            {
                                for(int i = 0; i < saves.Length - 1; i++) 
                                {
                                    if (!Object.Equals(saves[i], null))
                                    {
                                        try {
                                            if (saves[i] is CompleteSave)
                                            {
                                                (saves[i] as CompleteSave)?.RepositorySave();
                                            } else if (saves[i] is DiffSave)
                                            {
                                                (saves[i] as DiffSave).RepositorySave();
                                            }
                                        } catch (Exception e)
                                        {
                                            Console.WriteLine(e);
                                            (saves[i] as DiffSave)?.UpdateState("ERROR");
                                            GeneralTools.WriteWarningMessage((saves[i] as CompleteSave)?.Appellation + Language.Error.Access.Get(lang));
                                        }
                                    }
                                    saves[i] = null;
                                }
                            });
                            Console.Clear();
                            Console.WriteLine("Save progress... Enter for continue");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        
                        case "4":
                            lang = ChangeLang(lang);
                            Console.Clear();
                            break;
            
                        default:
                            Console.Clear();
                            GeneralTools.WriteWarningMessage(Language.Error.Option.Get(lang));
                            break;
                    }
            } while ((option != "e"));
            Console.WriteLine(Language.GoodBye.Get(lang));
        }


        /// <summary>
        /// Ask user the entree of some values and create the object associated at the option selected
        /// </summary>
        /// <param name="iSaves">The ISave arrays</param>
        /// <param name="option">The option selected</param>
        /// <param name="lang">The actual language</param>
        /// <returns></returns>
        private static ISave?[] Champ(ISave?[] iSaves, string? option, string lang)
        {
            Console.Clear();
            var saveError = PositionAvailable(iSaves);

            Console.Write(Language.Selected.CompleteSave.Get(lang));
            if (saveError != -1)
            {
                Console.WriteLine(Language.BackupName.Get(lang));
                var appellation = Console.ReadLine();
                string? sourcePath;
                do
                {
                    Console.WriteLine(Language.SourcePath.Get(lang));
                    sourcePath = Console.ReadLine();
                    if (!Directory.Exists(sourcePath)) GeneralTools.WriteWarningMessage(Language.Error.SourceExistence.Get(lang));
                } while (!Directory.Exists(sourcePath));

                string? targetPath;
                switch (option)
                {
                    case "1":
                    {
                        targetPath = "";
                        while ((!Directory.Exists(targetPath)))
                        {
                            Console.WriteLine(Language.TargetPath.Get(lang));
                            targetPath = Console.ReadLine();

                            // Check if directory exist
                            if (!Directory.Exists(targetPath))
                            {
                                string validPath = GeneralTools.GetValidDirectoryPath(targetPath);
                                if (targetPath != "" && validPath != "")
                                {
                                    if (validPath != "")
                                    {
                                        var invalidPath = targetPath?.Substring(validPath.Length,
                                            targetPath.Length - validPath.Length);

                                        GeneralTools.WriteWarningMessage(Language.Error.ErrTargetPath.Get(lang));
                                        Console.WriteLine(Language.CreationDirectory.Get(lang, validPath, invalidPath));
                                    }
                                    else
                                    {
                                        GeneralTools.WriteWarningMessage(Language.Error.ErrTargetPath.Get(lang));
                                        Console.WriteLine(Language.YesNo.Get(lang));
                                    }

                                    string? choice;
                                    do
                                    {
                                        choice = Console.ReadLine();
                                        if (choice != "y" && choice != "n")
                                        {
                                            GeneralTools.WriteWarningMessage(Language.Error.Option.Get(lang));
                                            Console.WriteLine(Language.YesNo.Get(lang));
                                        }
                                    } while (choice != "y" && choice != "n");

                                    if (choice == "y")
                                        if (targetPath != null)
                                            Directory.CreateDirectory(targetPath);
                                }

                                Console.Clear();
                            }
                        }

                        iSaves[saveError] = new CompleteSave(appellation, sourcePath, targetPath);
                        break;
                    }
                    case "2":
                    {
                        do
                        {
                            Console.WriteLine(Language.TargetPath.Get(lang));
                            targetPath = Console.ReadLine();

                            // Check if directory exist
                            if (!Directory.Exists(targetPath))
                                GeneralTools.WriteWarningMessage(Language.Error.TargetExistence.Get(lang));
                        } while (!Directory.Exists(targetPath));

                        iSaves[saveError] = new DiffSave(appellation, sourcePath, targetPath);
                        break;
                    }
                }

                Console.Clear();
            } else {
                    Console.Clear();
                    GeneralTools.WriteWarningMessage(Language.Error.SavesOverflow.Get(lang));
            }

            return iSaves;
        }

        /// <summary>
        /// Check if a position in save array is null
        /// </summary>
        /// <param name="saves">List of ISave (can be CompleteSave or DiffSave)</param>
        /// <returns>Int the first free position in the array. If full, return -1.</returns>
        private static int PositionAvailable(IReadOnlyList<ISave?> saves)
        {
            for(int i = 0; i < saves.Count - 1; i++) 
            {
                if (Equals(saves[i], null)) return i;
            }
            return -1;
        }
        
        /// <summary>
        /// Change Language when click by using Language Class
        /// </summary>
        /// <param name="language">String, can be Fr or Ang</param>
        /// <returns>string return the changed language</returns>
        private static string ChangeLang(string language)
        {
            return "fr" == language ? "ang" : "fr";
        }
    }
}