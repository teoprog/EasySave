using System.Diagnostics;
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

        internal long TotalFiles;

        internal static readonly string DirectoryPath = System.IO.Directory.GetCurrentDirectory() + @"..\..\..\..\Save_logs";

        protected Save()
        {
            this.TotalFiles = 0;
            CreateLogsFiles();
        }

        protected void UpdateLogs(string sourceFile, string fileTarget, long directorySize, string stopwatch)
        {
            string filepath = DirectoryPath + @"\logs.json";

            // Read the existing JSON file content into a string
            string jsonContent = File.ReadAllText(filepath);

            // Parse the string into a JObject
            List<dynamic>? jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

            // Create a new object
            JsonLog jsonInfoSaveFile = new JsonLog(this.Appellation, 
                sourceFile, 
                fileTarget, 
                this.TargetPath, 
                directorySize.ToString(), 
                stopwatch,
                DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            
            // Nothing in the json file, so it create a List for our Saves and Add the item to the list
            if (jsonObjects == null) jsonObjects = new List<dynamic>();
            jsonObjects.Add(jsonInfoSaveFile);

            // Serialise l'objet créé
            string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
            
            File.WriteAllText(filepath, updatedJson);
        }
        
        protected void UpdateState(string status)
        {
            string filepath = DirectoryPath + @"\state.json";

            // Parse the string into a JObject
            List<dynamic> jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText(filepath));
            
            // Create JObject with our values
            JsonState json = new JsonState(this.Appellation,
                                                this.SourcePath,
                                                this.TargetPath,
                                                status,
                                                this.TotalFiles.ToString(),
                                                this.FilesSize.ToString(),
                                                this.FilesToCopy != 0 && this.TotalFiles != 0 ? (this.FilesToCopy / this.TotalFiles)*100 + "%" : 0 + "%"
            );
            JObject jobject = JObject.FromObject(json);
            
            int i = 0;
            if (jsonObjects != null)
            {
                foreach (JObject obj in jsonObjects)
                {
                    if (i != 1 && (string)obj["Name"] == this.Appellation)
                    {
                        obj["TotalFilesToCopy"] = this.TotalFiles;
                        obj["TotalFilesSize"] = this.FilesSize;
                        obj["NbFilesLeftToDo"] = this.FilesToCopy;
                        obj["State"] = status;
                        
                        i = 1; // occurence fund
                    }
                }

                if (i != 1) jsonObjects.Add(jobject);

                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            } else
            {
                jsonObjects = new List<dynamic>();
                jsonObjects.Add(jobject);
                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            }
        }

        /// <summary>
        /// Calculate the size of a directory
        /// </summary>
        /// <param name="infoDirectory">Calcule la taille d'un répertoire</param>
        /// <param name="targetInfo"></param>
        /// <returns></returns>
        public long DirectorySize(string sourceDirectoryPath, string targetDirectoryPath) 
        {
            long size = 0;

            DirectoryInfo sourceDirectory = new DirectoryInfo(sourceDirectoryPath);
            DirectoryInfo targetDirectory = new DirectoryInfo(targetDirectoryPath);
            
            FileInfo[] sourceFiles = sourceDirectory.GetFiles();
            FileInfo[] targetFiles = targetDirectory.GetFiles();
            
            if (this is CompleteSave)
            {
                foreach (FileInfo sourceFile in sourceFiles)
                {
                    FileInfo targetFile = Array.Find(targetFiles, f => f.Name == sourceFile.Name);
                    if (targetFile != null) size += targetFile.Length;
                }
            }
            else
            {
                foreach (FileInfo sourceFile in sourceFiles)
                {
                    FileInfo targetFile = Array.Find(targetFiles, f => f.Name == sourceFile.Name);
                    if (targetFile != null && sourceFile.LastWriteTime > targetFile.LastWriteTime) size += targetFile.Length;
                }
            }
            return size;
        }

        protected static void CreateLogsFiles()
        {
            if (!System.IO.Directory.Exists(DirectoryPath))
            {
                System.IO.Directory.CreateDirectory(DirectoryPath);
            }
            
            if(!System.IO.File.Exists(DirectoryPath + @"\logs.json"))
                System.IO.File.Create(DirectoryPath + @"\logs.json");
                
            if(!System.IO.File.Exists(DirectoryPath + @"\state.json"))
                System.IO.File.Create(DirectoryPath + @"\state.json");
        }

        public void RepositorySave(string sourceDirectory, string targetDirectory)
        {
            // Global param
            string path;
            long fileSize;

            // Params only use for DiffSave
            string path2;
            FileInfo targetInfo, sourceInfo;

            Stopwatch stopwatch = new Stopwatch();


            UpdateState("ACTIVE");
            if (!Directory.Exists(targetDirectory)) 
                Directory.CreateDirectory(targetDirectory);

            if (this is CompleteSave)
            {
                foreach (var file in Directory.GetFiles(sourceDirectory))
                {
                    path = Path.Combine(targetDirectory, Path.GetFileName(file));
                    stopwatch.Start();
                    File.Copy(file, path, true);
                    stopwatch.Stop();
                    fileSize = (new FileInfo(file)).Length;
                    this.FilesToCopy--;
                    this.FilesSize -= fileSize;
                    
                    UpdateState("ACTIVE");
                    UpdateLogs(file,path, fileSize,  stopwatch.Elapsed.ToString());
                }
            }
            else
            {
                foreach (var file in Directory.GetFiles(sourceDirectory))
                {
                    path = Path.Combine(targetDirectory, Path.GetFileName(file));
                    path2 = Path.Combine(sourceDirectory, Path.GetFileName(file));
                    targetInfo = new FileInfo(path);
                    sourceInfo = new FileInfo(path2);
                    fileSize = 0;
                    stopwatch.Start();
                    if (sourceInfo.LastWriteTime > targetInfo.LastWriteTime)
                    {
                        fileSize = (new FileInfo(file)).Length;
                        
                        File.Copy(file, path, true);
                    }
                    else
                    {
                        this.TotalFiles--;
                    }
                    stopwatch.Stop();
                    
                    UpdateState("ACTIVE");
                    UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString());
                    this.FilesToCopy--;
                    this.FilesSize -= fileSize;
                }
            }

            foreach (var directory in Directory.GetDirectories(sourceDirectory))
            {
                path = Path.Combine(targetDirectory, Path.GetFileName(directory));
                RepositorySave(directory, path);
            }
            UpdateState("END");
        }
    }
}