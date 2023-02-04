using System;
using System.IO;

namespace EasySave
{
    public class CompleteSave : Save
    {
        public CompleteSave() 
            //: base(Appellation, SourcePath, TargetPath)
        {
            this.Appellation = "";
            this.SourcePath = "";
            this.TargetPath = "";
            this.IsCompleteSave = true;
        }
        
        internal void RepositorySave()
        {
           this.RepositorySave(this.SourcePath, this.TargetPath);
        }
        
        
        /// <summary>
        /// Simple copy of directory
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <param name="targetDirectory"></param>
        private void RepositorySave(string sourceDirectory, string targetDirectory)
        {
            string path;
            
            if (!Directory.Exists(targetDirectory)) 
                Directory.CreateDirectory(targetDirectory);

            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                path = Path.Combine(targetDirectory, Path.GetFileName(file));
                Console.WriteLine(file + "       " + path);
                File.Copy(file, path, true);
            }

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                path = Path.Combine(targetDirectory, Path.GetFileName(directory));
                RepositorySave(directory, path);
            }
        }
    }
}