﻿namespace EasySave
{
    public static class GeneralTools
    {
        /// <summary>
        /// Var who stock the path to the logs directory
        /// </summary>
        public static readonly string DirectoryPath = "./../../../Save_logs";
        
        /// <summary>
        /// A simple function who write a warning message
        /// </summary>
        /// <param name="word">Message you want to write on Console</param>
        internal static void WriteWarningMessage(string word) {
            ConsoleColor temp = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(word);
            Console.ForegroundColor = temp;
        }
        
        /// <summary>
        /// Create log folder and logs files if they don't exist
        /// </summary>
        public static void CreateLogsFiles()
        {
            if (!Directory.Exists(DirectoryPath))
                Directory.CreateDirectory(DirectoryPath);

            if(!File.Exists(DirectoryPath + "/logs.json"))
                File.Create(DirectoryPath + "/logs.json");
                
            
            if(!File.Exists(DirectoryPath + "/state.json"))
                File.Create(DirectoryPath + "/state.json");
            
            if(!File.Exists(DirectoryPath + "/businessSoftware.json"))
                File.Create(DirectoryPath + "/businessSoftware.json");
        }
        
        /// <summary>
        /// Get the valid directory of a path :
        ///  ex : C:/Bureau/rep --> get C:/Bureau cause rep does not exist
        /// </summary>
        /// <param name="path">Invalid path</param>
        /// <returns>The valid path</returns>
        public static string GetValidDirectoryPath(string? path)
        {
            if (path != null && !path.Contains(@"\") && !path.Contains("/")) return "";
            while (!Directory.Exists(path)) path = Path.GetDirectoryName(path);
            return path;
        }
    }
}