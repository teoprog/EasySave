using Microsoft.Extensions.Configuration;

namespace EasySave
{
    public static class GeneralTools
    {
        /// <summary>
        /// Var who stock the path to the logs directory
        /// </summary>
        public static readonly string? LogPath = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build()["Log_Path"];
        
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
            if (!Directory.Exists(LogPath))
                Directory.CreateDirectory(LogPath);

            
            if(!File.Exists(LogPath + "/logs.json"))
                File.Create(LogPath + "/logs.json");
            
            if(!File.Exists(LogPath + "/state.json"))
                File.Create(LogPath + "/state.json");
            
            
            if(!File.Exists(LogPath + "/logs.xml"))
                File.Create(LogPath + "/logs.xml");
        
            if(!File.Exists(LogPath + "/state.xml"))
                File.Create(LogPath + "/state.xml");
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