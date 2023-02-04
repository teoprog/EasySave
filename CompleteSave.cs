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
            if (System.IO.Directory.Exists(this.SourcePath))
            {
                string fileName;
                string destFilePath;
                string[] files;
                this.CopyDirectory(this.SourcePath, this.TargetPath);
            }
        }
        
        /// <summary>
        /// Simple copy of a directory
        /// </summary>
        private void CopyDirectory(string sourceDirectory, string targetDirectory)
        {
            string targetPath;
            foreach (var file in Directory.GetFiles(this.SourcePath))
            {
                targetPath = Path.Combine(this.TargetPath, Path.GetFileName(file));
                File.Copy(file, targetPath, true);
            }

            foreach (var directory in Directory.GetDirectories(this.SourcePath))
            {
                targetPath = Path.Combine(this.TargetPath, Path.GetFileName(directory));
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                CopyDirectory(directory, targetPath);
            }
        }
    }
}