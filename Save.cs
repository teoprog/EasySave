using Newtonsoft.Json;

namespace EasySave
{
    public abstract class Save
    {
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


        internal int FilesToCopy; 
            
        protected Save()
        {
            
        }

        protected Save(string Appellation, string SourcePath, string TargetPath)
        {
         }


        protected void UpdateProgress(string sourceFile, string fileTarget, long directorySize, string stopwatch)
        {
            // C:\Users\emiro\RiderProjects\EasySave\EasySave
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_log.json";
                
            // Read the existing JSON file content into a string
            string jsonContent = File.ReadAllText(filepath);

            // Parse the string into a JObject
            List<dynamic>? jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

            // Create a new dynamic object
            dynamic jsonInfoSaveFile = new
            {
                Name = this.Appellation,
                FileSource = sourceFile,
                FileTarget = fileTarget,
                destPath = this.TargetPath,
                FileSize = directorySize,
                FileTransferTime = stopwatch,
                time = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
            };
            
            if (jsonObjects == null) jsonObjects = new List<dynamic>();
                jsonObjects.Add(jsonInfoSaveFile);

                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
            
            File.WriteAllText(filepath, updatedJson);
        }
        
        protected void UpdateProgressSave(long numberFiles, long folderSize)
        {
            // C:\Users\emiro\RiderProjects\EasySave\EasySave
            string filepath = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\Sample_state.json";
                
            // Read the existing JSON file content into a string
            string jsonContent = File.ReadAllText(filepath);
    
            // Parse the string into a JObject
            List<dynamic>? jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

            // Create a new dynamic object
            dynamic jsonInfoSaveFile = new
            {
                Name = this.Appellation,
                FileSource = this.SourcePath,
                FileTarget = this.TargetPath,
                
                State = "ACTIVE",
                TotalFilesToCopy = numberFiles,
                TotalFilesSize = folderSize,
                NbFilesLeftToDo = "",
                Progression = ""
            };
            if (jsonObjects == null) jsonObjects = new List<dynamic>();
            
            jsonObjects.Add(jsonInfoSaveFile);

            string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                
            File.WriteAllText(filepath, updatedJson);
        }
        
        
        public static long DirectorySize(DirectoryInfo d) 
        {    
            long size = 0;
            
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis) 
            {      
                size += fi.Length;    
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis) 
            {
                size += DirectorySize(di);   
            }
            return size;  
        }
    }
}