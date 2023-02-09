namespace EasySave
{
    public class DiffSave : Save, ISave
    {
        /// <summary>
        /// Constructor who call his parent
        /// </summary>
        public DiffSave(string? appellation, string sourcePath, string targetPath) 
            : base(appellation, sourcePath, targetPath)
        {
        }
        
        /// <summary>
        /// Call parent file for do the Save
        /// </summary>
        public void RepositorySave()
        {
            this.FilesSize = DirectorySize();
            this.TotalFiles = this.FilesToCopy = Directory.GetFiles(this.SourcePath, "*", SearchOption.AllDirectories).Length;
            base.RepositorySave(this.SourcePath, this.TargetPath);
        }
    }
}