﻿using System.Diagnostics;

namespace EasySave
{
    public class CompleteSave : Save
    {
        public CompleteSave() {
            this.Appellation = "";
            this.SourcePath = "";
            this.TargetPath = "";
            this.FilesToCopy = 0;
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
            long fileSize;
            long folderSize;
            int numberFiles;
            
            Stopwatch stopwatch = new Stopwatch();
            folderSize = DirectorySize(new DirectoryInfo(sourceDirectory));
            numberFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories).Length;

            UpdateProgressSave(numberFiles, folderSize);
            
            if (!Directory.Exists(targetDirectory)) 
                Directory.CreateDirectory(targetDirectory);

            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                path = Path.Combine(targetDirectory, Path.GetFileName(file));
                
                stopwatch.Start();
                
                File.Copy(file, path, true);
                stopwatch.Stop();

                fileSize = (new FileInfo(file)).Length;
                UpdateProgress(file,path, fileSize,  stopwatch.Elapsed.ToString());
            }

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                path = Path.Combine(targetDirectory, Path.GetFileName(directory));
                RepositorySave(directory, path);
            }
        }
    }
}