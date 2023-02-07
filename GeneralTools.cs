namespace EasySave
{
    public class GeneralTools
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
        ///  ex : C:/Bureau/iufdfgoqug --> get C:/Bureau cause iufdfgoqug does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetValidDirectoryPath(string path)
        {
            while (!Directory.Exists(path)) path = Path.GetDirectoryName(path);
            return path;
        }
    }
}