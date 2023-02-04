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
        
        internal int RepositorySave()
        {
            var pathCorrect = (dynamic) this.PathCorrect;
            
            Console.WriteLine(pathCorrect.Source + "       " + pathCorrect.Target);
            
            if (!pathCorrect.Source && !pathCorrect.Target)
            {
                return -1;
            } else if (!pathCorrect.Source)
            {
                return -2;
            } else if (!pathCorrect.Target)
            {
                return -3;
            }

            if (System.IO.Directory.Exists(this.SourcePath))
            {
                string fileName;
                string destFilePath;
                string[] files;
            
                Console.WriteLine("marche");
            
                // Get all files names from a directory
                files = System.IO.Directory.GetFiles(this.SourcePath);
            
                // Copy the files and overwrite destination files if they already exist.
                foreach (string file in files)
                {
                    // Extract one filename from the path
                    fileName = System.IO.Path.GetFileName(file);
            
                    // TargetPath with fileName for create the path where is copied
                    destFilePath = System.IO.Path.Combine(this.TargetPath, fileName);
            
                    // Simple copy of a file
                    System.IO.File.Copy(file, destFilePath, true);
                }
            }
            else
            {
                
            }
            
            return 0;
        }
        

    }
}