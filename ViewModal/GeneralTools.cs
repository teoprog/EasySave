namespace EasySave
{
    public static class GeneralTools
    {
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
        
        
        /// <summary>
        /// Change Language when click by using Language Class
        /// </summary>
        /// <param name="language">String, can be Fr or Ang</param>
        /// <returns>string return the changed language</returns>
        public static string ChangeLang(string language)
        {
            return "fr" == language ? "ang" : "fr";
        }
    }
}