using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace EasySave
{
    interface ISave {}
    
    public abstract class Save
    {
        /// <summary>
        /// Name of the save
        /// </summary>
        internal string? Appellation;

        /// <summary>
        /// What you want to save
        /// </summary>
        internal string SourcePath;

        /// <summary>
        /// Where you want to save
        /// </summary>
        internal string TargetPath;


        /// <summary>
        /// Files need to be copied (decrement when a file is copied)
        /// </summary>
        internal long FilesToCopy;

        /// <summary>
        /// Size of path directory
        /// </summary>
        internal long FilesSize;

        /// <summary>
        /// Files total to be copied
        /// </summary>
        internal long TotalFiles;

        /// <summary>
        /// Var who stock the path to the logs directory
        /// </summary>
        internal static readonly string DirectoryPath = System.IO.Directory.GetCurrentDirectory() + @"..\..\..\..\Save_logs";
        
        protected Save(string? appellation, string sourcePath, string targetPath)
        {
            Appellation = appellation;
            SourcePath = sourcePath;
            TargetPath = targetPath;
            this.TotalFiles = 0;
            CreateLogsFiles();
        }
        
        /// <summary>
        /// Function who update dynamically the log file
        /// </summary>
        /// <param name="sourceFile">File source to be copied</param>
        /// <param name="fileTarget">File target, location to where we want to copy sourceFile</param>
        /// <param name="fileSize">Size of the file</param>
        /// <param name="stopwatch">Time of the copy</param>
        protected void UpdateLogs(string sourceFile, string fileTarget, long fileSize, string stopwatch)
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
                                                        fileSize.ToString(), 
                                                        stopwatch,
                                                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            
            // Nothing in the json file, so it create a List for our Saves and Add the item to the list
            if (jsonObjects == null) jsonObjects = new List<dynamic>();
            jsonObjects.Add(jsonInfoSaveFile);

            // Serialize created object
            string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
            
            File.WriteAllText(filepath, updatedJson);
        }
        
        /// <summary>
        /// Function who update dynamically the state file
        /// </summary>
        /// <param name="status"></param>
        public void UpdateState(string status)
        {
            string filepath = DirectoryPath + @"\state.json";

            // Parse the string into a JObject
            List<dynamic>? jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText(filepath));
            
            // Create JObject with our values
            JsonState json = new JsonState(this.Appellation,
                                                this.SourcePath,
                                                this.TargetPath,
                                                status,
                                                this.TotalFiles.ToString(),
                                                this.FilesSize.ToString(),
                                                this.FilesToCopy != 0 && this.TotalFiles != 0 ? (this.FilesToCopy / this.TotalFiles)*100 + "%" : 0 + "%"
            );
            
            // Transform our object into a JObject
            JObject jobject = JObject.FromObject(json);
            
            int i = 0;
            if (jsonObjects != null)
            {
                foreach (JObject obj in jsonObjects)
                {
                    if (i != 1 && obj["Name"].ToString() == this.Appellation)
                    {
                        // Permit to update our file values if occurence with same appellation found
                        obj["State"] = status;
                        obj["TotalFilesToCopy"] = this.TotalFiles;
                        obj["TotalFilesSize"] = this.FilesSize;
                        obj["NbFilesLeftToDo"] = this.FilesToCopy;
                        obj["Progression"] = this.TotalFiles == 0 || this.FilesToCopy == 0 ? 100 + "%" : (100 - ((double)this.FilesToCopy / this.TotalFiles) * 100).ToString("0.00") + "%";
                        
                        i = 1; // occurence found
                    }
                }

                if (i != 1) jsonObjects.Add(jobject);

                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            } else
            {
                // Initialize list and add objects to list
                jsonObjects = new List<dynamic>();
                jsonObjects.Add(jobject);
                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            }
        }

        /// <summary>
        /// Calculate the size of a directory
        /// </summary>+
        /// <returns>The size of the directory</returns>
        public long DirectorySize() 
        {
            long size = 0;

            DirectoryInfo sourceDirectory = new DirectoryInfo(this.SourcePath);
            DirectoryInfo targetDirectory = new DirectoryInfo(this.TargetPath);
            
            if (this is CompleteSave)
            {
                foreach (FileInfo file in sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
                    size += file.Length;
            }
            else
            {
                foreach (FileSystemInfo info in sourceDirectory.GetFileSystemInfos())
                {
                    foreach (FileInfo file in sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
                    {
                        FileInfo? targetFile = targetDirectory.EnumerateFiles("*", SearchOption.AllDirectories)
                            .FirstOrDefault(f => f.Name == file.Name);
                        
                        if (targetFile != null && file.LastWriteTime > targetFile.LastWriteTime) size += file.Length;
                    }
                }
            }
            return size;
        }

        /// <summary>
        /// Create log folder and logs files if they don't exist
        /// </summary>
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

        /// <summary>
        /// Function who save a repository (completeSave and diffSave)
        /// </summary>
        /// <param name="sourceDirectory">The source directory we actually browse</param>
        /// <param name="targetDirectory">The target directory we want to add modifications</param>
        public void RepositorySave(string sourceDirectory, string targetDirectory)
        {
            // Global param
            string path;
            long fileSize;
            Stopwatch stopwatch = new Stopwatch();
            
            // Params only used for DiffSave
            string path2;
            FileInfo targetInfo, sourceInfo;

            UpdateState("ACTIVE");

            if (this is CompleteSave)
            {
                foreach (var file in Directory.GetFiles(sourceDirectory))
                {
                    path = Path.Combine(targetDirectory, Path.GetFileName(file));
                    stopwatch.Start();
                    File.Copy(file, path, true);
                    stopwatch.Stop();
                    
                    // Different values we need for json
                    fileSize = (new FileInfo(file)).Length;
                    this.FilesToCopy--;
                    this.FilesSize -= fileSize;
                    
                    // Update our logs
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
                        File.Copy(file, path, true);
                        
                        //Different values we need for our json
                        fileSize = (new FileInfo(file)).Length;
                        this.FilesSize -= fileSize;
                    }
                    else
                    {
                        this.TotalFiles--;
                    }
                    stopwatch.Stop();
                    
                    //Different values we need for our json
                    this.FilesToCopy--;
                    
                    // Update our logs
                    UpdateState("ACTIVE");
                    UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString());
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