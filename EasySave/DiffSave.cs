using Microsoft.Extensions.Configuration;

namespace EasySave
{
    public class DiffSave : Save, ISave
    {
        /// <summary>
        /// Constructor who call his parent
        /// </summary>
        public DiffSave(string? appellation, string? sourcePath, string? targetPath) 
            : base(appellation, sourcePath, targetPath)
        {
        }
        
        /// <summary>
        /// Call parent file for do the Save
        /// </summary>
        public void RepositorySave()
        {
            this.FilesSize = DirectorySize();
            if (this.SourcePath != null)
            {
                this.TotalFiles = this.FilesToCopy = Directory.GetFiles(this.SourcePath, "*", SearchOption.AllDirectories).Length;
                this.CreateDirectoriesOfADirectory();
                GeneralTools.CreateLogsFiles();
                IConfiguration conf = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
                CreateState(conf);
                base.RepositorySave(this.SourcePath, this.TargetPath);
            }
        }
    }
}