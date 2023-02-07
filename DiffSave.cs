using System;
using System.Diagnostics;
using System.IO;

namespace EasySave
{
    public class DiffSave : Save
    {
        public DiffSave()
        {
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
            string path, path2;
            FileInfo targetInfo, sourceInfo;
            long fileSize;
            long totalFiles;
            
            Stopwatch stopwatch = new Stopwatch();
            this.FilesToCopy = totalFiles = Directory.GetFiles(sourceDirectory, "*", SearchOption.AllDirectories).Length;
            this.FilesSize = DirectorySize(new DirectoryInfo(sourceDirectory));

            UpdateProgressSave(totalFiles, "ACTIVE");
            if (!Directory.Exists(targetDirectory)) 
                Directory.CreateDirectory(targetDirectory);

            foreach (var file in Directory.GetFiles(sourceDirectory))
            {
                path = Path.Combine(targetDirectory, Path.GetFileName(file));
                path2 = Path.Combine(sourceDirectory, Path.GetFileName(file));
                targetInfo = new FileInfo(path);
                sourceInfo = new FileInfo(path2);
                if (sourceInfo.LastWriteTime > targetInfo.LastWriteTime)
                {
                    stopwatch.Start();
                    File.Copy(file, path, true);
                    stopwatch.Stop();
                    
                    fileSize = (new FileInfo(file)).Length;
                    UpdateProgressSave(totalFiles, "ACTIVE");
                    UpdateProgress(file,path, fileSize,  stopwatch.Elapsed.ToString());
                }
            }

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                path = Path.Combine(targetDirectory, Path.GetFileName(directory));
                RepositorySave(directory, path);
            }
            UpdateProgressSave(totalFiles, "END");
        }
    }
}