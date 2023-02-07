using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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


        internal long FilesToCopy;

        internal long FilesSize;
            
        protected Save()
        {
            
        }

        protected void UpdateProgress(string sourceFile, string fileTarget, long directorySize, string stopwatch)
        {
            string filepath = DriveInfo.GetDrives()[0].Name + @"\temp\Sample_log.json";
            if (!File.Exists(filepath)) File.Create(filepath);   
            
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
        
        protected void UpdateProgressSave(long totalFiles, string status)
        {
            string filepath = DriveInfo.GetDrives()[0].Name + @"\temp\Sample_state.json";
            if (!File.Exists(filepath)) File.Create(filepath);   
            
            // Parse the string into a JObject
            List<dynamic> jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText(filepath));
            
            // Create JObject with our values
            JObject  g = new JObject
            (
                new JProperty("Name", this.Appellation),
                new JProperty("FileSource", this.SourcePath),
                new JProperty("FileTarget", this.TargetPath),
                new JProperty("State", status),
                new JProperty("TotalFilesToCopy", totalFiles),
                new JProperty("TotalFilesSize", this.FilesSize),
                new JProperty("Progression", totalFiles - this.FilesToCopy)
            );

            int i = 0;
            if (jsonObjects != null)
            {
                foreach (JObject obj in jsonObjects)
                {
                    // foreach (var property in obj)
                    // {
                    //     if (property.Key == "Name" && property.Value.ToString() == this.Appellation)
                    if (i != 1 && (string)obj["Name"] == this.Appellation)
                    {
                        obj["TotalFilesSize"] = this.FilesSize;
                        obj["NbFilesLeftToDo"] = this.FilesToCopy;
                        obj["State"] = status;
                        
                        i = 1; // occurence fund
                    }
                }

                if (i != 1) jsonObjects.Add(g);

                    string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            } else
            {
                jsonObjects = new List<dynamic>();
                jsonObjects.Add(g);
                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            }
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