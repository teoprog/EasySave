namespace EasySave;

public class Language
{
    public class Welcome
    {
        public static string Fr { get;} = "Bienvenue sur EasySave !\n";
        public static string Ang { get;} = "Welcome on EasySave !\n";
        
        public static string get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public class GoodBye
    {
        public static string Ang = "Thanks for using our software !";
        public static string Fr = "Merci d'avoir utilisé notre logiciel !";
        
        public static string get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public class Selected
    {
        public class CompleteSave
        {
            public static string Ang = "Selected : Complete Save";
            public static string Fr = "Sélectionné : Sauvegarde";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public class DiffSave
        {
            public static string Ang = "Selected : Diffferantial Save";
            public static string Fr = "Choisi : Sauvegarde Différentielle";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
    }

    public class BackupName
    {
        public static string Ang = "\nName of the Backup :";
        public static string Fr = "\nNom de la Sauvegarde :";
        
        public static string get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public class SourcePath
    {
        public static string Ang = "\nSource Path :";
        public static string Fr = "\nChemin Source :";
        
        public static string get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public class TargetPath
    {
        public static string Ang = "\nTarget Path :";
        public static string Fr = "\nChemin Cible :";
        
        public static string get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public class Menu
    {
        public class Choice
        {
            public static string Ang = "To get starting, choose a save mode :";
            public static string Fr = "Pour commencer, choisis un mode de sauvegarde :";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public class CompleteSave
        {
            public static string Ang = "1- Complete Save";
            public static string Fr = "1- Sauvegarde Complète";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public class DiffSave
        {
            public static string Ang = "2- Differential Save (save only modified files)";
            public static string Fr = "2- Sauvegarde Diférentielle (sauvegarde seulement les fichiers modifiés)";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        
        public class ChangeLanguage
        {
            public static string Ang = "3- Change Language (Languages available : French, English)";
            public static string Fr = "3- Changement de langue (Langues disponibles : Français, Anglais)";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public class Exit
        {
            public static string Ang = "e- For close the Application";
            public static string Fr = "e- Pour fermer l'application";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
    }
    
    public class YesNo
    {
        public static string Ang = "Choose between : yes(y) - no(n)";
        public static string Fr = "Choisis entre : oui(y) - no(n)";
        
        public static string get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public class CreationDirectory
    {
        public static string get(string word, string validPath, string invalidPath)
        {
            return word == "fr" ? "Would you like to create le dossier : \n"
                                  + $"\"{invalidPath}\" at the path :\n\"{validPath}\"\n\n yes(y) - no(n)"
                                  : "Voudriez-vous créer : \n"
                                    + $"\"{invalidPath}\" au chemin :\n\"{validPath}\"\n\n yes(y) - no(n)";
        }
    }

    public class Error
    {
        public class Option
        {
            public static string Ang = "Retry ! Chose a valid option";
            public static string Fr = "Rééssaie ! Choisis une option Valide";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        
        public class Access
        {
            public static string Ang = "Problem in save, some file access have been denied.";
            public static string Fr = "Problème dans la sauvegarde, l'accès a été refusé.";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        public class SourceExistence
        {
            public static string Ang = "You can't do a Differential Save without existing source folder. Retry !";
            public static string Fr = "Tu ne peux pas faire une Sauvegarde Différentielle d'un dossier source qui n'existe pas.";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        public class TargetExistence
        {
            public static string Ang = "You can't do a Differential Save without existing target folder. Retry !";
            public static string Fr = "Tu ne peux pas faire une Sauvegarde Différentielle d'un dossier cible qui n'existe pas.";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        public class TargetPath
        {
            public static string Ang = "Target path of the directory does not exist.\n";
            public static string Fr = "Le lien vers le répertoire n'existe pas.";
            
            public static string get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
    }
}