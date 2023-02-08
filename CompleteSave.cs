namespace EasySave
{
    public class CompleteSave : Save
    {
        
        /// <summary>
        /// Constructor who call his parent
        /// </summary>
        public CompleteSave() : base() {
            this.Appellation = "";
            this.SourcePath = "";
            this.TargetPath = "";
            this.FilesToCopy = 0;
        }
        
        /// <summary>
        /// Call parent file for do the Save
        /// </summary>
        internal void RepositorySave()
        {
            this.FilesSize = DirectorySize(this.SourcePath, this.TargetPath);
            this.TotalFiles = this.FilesToCopy = Directory.GetFiles(this.SourcePath, "*", SearchOption.AllDirectories).Length;
            base.RepositorySave(this.SourcePath, this.TargetPath);
        }
    }
}