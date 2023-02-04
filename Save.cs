namespace EasySave
{
    public abstract class Save
    {
        /// <summary>
        /// Anonymous type whitch contain if SourcePath or TargetPath have valid path
        /// </summary>
        public object PathCorrect;
        
        /// <summary>
        /// Name of the save
        /// </summary>
        internal string Appellation;

        /// <summary>
        /// What you want to save
        /// </summary>
        internal string SourcePath;

        /// <summary>
        /// Where you want to save
        /// </summary>
        internal string TargetPath;

        /// <summary>
        /// DiffSave or CompleteSave
        /// </summary>
        internal bool IsCompleteSave;

        protected Save()
        {
            
        }
        
        protected Save(string Appellation, string SourcePath, string TargetPath)
        {
        //     this.Appellation = Appellation;
        //     this.SourcePath = SourcePath;
        //     this.TargetPath = TargetPath;
        //     this.PathCorrect = new
        //     {
        //         Source = System.IO.Directory.Exists(this.SourcePath),
        //         Target = System.IO.Directory.Exists(this.TargetPath)
        //     };
         }
    }
}