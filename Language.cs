namespace EasySave;

public abstract class Language
{
    public abstract class Welcome
    {
        private const string Fr = "Bienvenue sur EasySave !\n";
        private const string Ang = "Welcome on EasySave !\n";

        public static string Get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public abstract class GoodBye
    {
        private const string Ang = "Thanks for using our software !";
        private const string Fr = "Merci d'avoir utilisé notre logiciel !";

        public static string Get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public abstract class Selected
    {
        public abstract class CompleteSave
        {
            private const string Ang = "Selected : Complete Save";
            private const string Fr = "Sélectionné : Sauvegarde";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public class DiffSave
        {
            private const string Ang = "Selected : Diffferantial Save";
            private const string Fr = "Choisi : Sauvegarde Différentielle";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
    }

    public abstract class BackupName
    {
        private const string Ang = "\nName of the Backup :";
        private const string Fr = "\nNom de la Sauvegarde :";

        public static string Get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public abstract class SourcePath
    {
        private const string Ang = "\nSource Path :";
        private const string Fr = "\nChemin Source :";

        public static string Get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public abstract class TargetPath
    {
        private const string Ang = "\nTarget Path :";
        private const string Fr = "\nChemin Cible :";

        public static string Get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public abstract class Menu
    {
        public abstract class Choice
        {
            private const string Ang = "To get starting, choose a save mode :";
            private const string Fr = "Pour commencer, choisis un mode de sauvegarde :";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public class CompleteSave
        {
            private const string Ang = "1- Complete Save";
            private const string Fr = "1- Sauvegarde Complète";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public abstract class DiffSave
        {
            private const string Ang = "2- Differential Save (save only modified files)";
            private const string Fr = "2- Sauvegarde Diférentielle (sauvegarde seulement les fichiers modifiés)";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        
        public abstract class LaunchSaves
        {
            private const string Ang = "3- Launch the saves in memory (max 5)";
            private const string Fr = "3- Lance les sauvegardes en mémoire (5 max)";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        
        public abstract class ChangeLanguage
        {
            private const string Ang = "4- Change Language (Languages available : French, English)";
            private const string Fr = "4- Changement de langue (Langues disponibles : Français, Anglais)";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }

        public abstract class Exit
        {
            private const string Ang = "e- For close the Application";
            private const string Fr = "e- Pour fermer l'application";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
    }
    
    public abstract class YesNo
    {
        private const string Ang = "Choose between : yes(y) - no(n)";
        private const string Fr = "Choisis entre : oui(y) - no(n)";

        public static string Get(string word)
        {
            return word == "fr" ? Fr : Ang;
        }
    }

    public abstract class CreationDirectory
    {
        public static string Get(string word, string? validPath, string? invalidPath)
        {
            return word == "fr" ? "Souhaitez-vous créer le dossier :          \n"
                                  + $"\"{invalidPath}\" au chemin :\n\"{validPath}\"\n\n oui(y) - non(n)"
                                  : "Would you like to create the directory : \n"
                                    + $"\"{invalidPath}\" at the path :          \n\"{validPath}\"\n\n yes(y) - no(n)";
        }
    }

    public abstract class Error
    {
        public abstract class SavesOverflow
        {
            private const string Ang = "You already have 5 saves in memory. Unable to add. Please run current saves for have the ability to create new ones.";
            private const string Fr = "Vous avez déjà 5 sauvegardes en mémoire. Impossible d'en ajouter. Veuillez lancer les sauvegardes actuelles pour avoir la possibilité d'en créer des nouvelles.";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        
        public class Option
        {
            private const string Ang = "Retry ! Chose a valid option";
            private const string Fr = "Rééssaie ! Choisis une option Valide";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        
        public class Access
        {
            private const string Ang = " save : problem in the backup, access to a directory has been denied. It is read-only so the backup cannot be performed entirely.";
            private const string Fr = " sauvegarde : problème dans la sauvegarde, l'accès à un répertoire à été refusé. Il est en lecture seule la sauvegarde ne peut donc s'effectuer entièrement";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        public abstract class SourceExistence
        {
            private const string Ang = "You can't do a Differential Save without existing source folder. Retry !";
            private const string Fr = "Tu ne peux pas faire une Sauvegarde Différentielle d'un dossier source qui n'existe pas.";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        public abstract class TargetExistence
        {
            private const string Ang = "You can't do a Differential Save without existing target folder. Retry !";
            private const string Fr = "Tu ne peux pas faire une Sauvegarde Différentielle d'un dossier cible qui n'existe pas.";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
        
        public abstract class ErrTargetPath
        {
            private const string Ang = "Target path of the directory does not exist.\n";
            private const string Fr = "Le lien vers le répertoire n'existe pas.";

            public static string Get(string word)
            {
                return word == "fr" ? Fr : Ang;
            }
        }
    }
}