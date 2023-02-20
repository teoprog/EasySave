using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Microsoft.Extensions.Configuration;
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
        internal readonly string? Appellation;

        /// <summary>
        /// What you want to save
        /// </summary>
        internal readonly string? SourcePath;

        /// <summary>
        /// Where you want to save
        /// </summary>
        internal readonly string? TargetPath;

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
        
        private static object fileLock = new object();

        protected Save(string? appellation, string? sourcePath, string? targetPath)
        { 
            Appellation = appellation;
            SourcePath = sourcePath;
            TargetPath = targetPath;
            this.TotalFiles = 0;
        }

        /// <summary>
        /// Function who update dynamically the log file
        /// </summary>
        /// <param name="sourceFile">File source to be copied</param>
        /// <param name="fileTarget">File target, location to where we want to copy sourceFile</param>
        /// <param name="fileSize">Size of the file</param>
        /// <param name="stopwatch">Time of the copy</param>
        private void UpdateLogs(string sourceFile, string? fileTarget, long fileSize, string stopwatch,
            string cryptTime, IConfiguration conf)
        {
            string xmlorjson = conf["JsonOrXml"];
            
            if (xmlorjson == "json")
            { 
                string filepath = GeneralTools.LogPath + "\\logs.json";
            
                // Read the existing JSON file content into a string
                string jsonContent = File.ReadAllText(filepath);

                // Parse the string into a JObject
                List<dynamic> jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

                // Create a new object
                LogFile jsonInfoSaveFile = new LogFile(this.Appellation,
                    sourceFile,
                    fileTarget,
                    this.TargetPath,
                    fileSize.ToString(),
                    stopwatch,
                    DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                    cryptTime);

                if(jsonObjects == null) jsonObjects = new List<dynamic>();
                jsonObjects.Add(jsonInfoSaveFile);

                // Serialize created object
                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
            
                File.WriteAllText(filepath, updatedJson);
            }
            else
            {
                string filepath = GeneralTools.LogPath + "\\logs.xml";

                List<LogFile> xmlObjects;
                XmlSerializer serializer = new XmlSerializer(typeof(List<LogFile>));

                // Try to open the file in a shared mode
                using (var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    if (fileStream.Length > 0)
                    {
                        // Read the existing XML file content into a List of LogFile objects
                        xmlObjects = (List<LogFile>)serializer.Deserialize(fileStream);
                    }
                    else
                    {
                        xmlObjects = new List<LogFile>();
                    }

                    // Create a new LogFile object
                    LogFile xmlInfoSaveFile = new LogFile(this.Appellation,
                        sourceFile,
                        fileTarget,
                        this.TargetPath,
                        fileSize.ToString(),
                        stopwatch,
                        DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        cryptTime);

                    xmlObjects.Add(xmlInfoSaveFile);

                    // Serialize and write the updated List of LogFile objects to the XML file
                    fileStream.Position = 0;
                    serializer.Serialize(fileStream, xmlObjects);
                }
            }
        }

        /// <summary>
        /// Function who update dynamically the state file
        /// </summary>
        /// <param name="status"></param>
        public void UpdateState(IConfiguration conf)
        {
            string xmlorjson = conf["JsonOrXml"];

            if (xmlorjson == "json")
            {
                string filepath = GeneralTools.LogPath + "\\state.json";

                // Parse the string into a JObject
                List<StateFile>? jsonObjects = JsonConvert.DeserializeObject<List<StateFile>>(File.ReadAllText(filepath));

                if (this.FilesToCopy == 0) jsonObjects[^1].State = "END";
                // jsonObjects[^1].TotalFilesToCopy = this.TotalFiles.ToString();
                // jsonObjects[^1].TotalFilesSize = this.FilesSize.ToString();
                // jsonObjects[^1].NbFilesLeftToDo = this.FilesToCopy.ToString();
                // jsonObjects[^1].Progression = 
                //     this.TotalFiles == 0 || this.FilesToCopy == 0
                //     ? 100 + "%"
                //     : (100 - ((double)this.FilesToCopy / this.TotalFiles) * 100).ToString("0.00") + "%";
                
                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            }
            else
            {
                string filepath = GeneralTools.LogPath + "\\state.xml";
                XmlSerializer serializer = new XmlSerializer(typeof(List<StateFile>));
                List<StateFile> xmlObjects;

                using (var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    xmlObjects = (List<StateFile>)serializer.Deserialize(fileStream);

                    if (this.FilesToCopy == 0)
                    {
                        xmlObjects[^1].State = "END";
                    }

                    xmlObjects[^1].TotalFilesToCopy = this.TotalFiles.ToString();
                    xmlObjects[^1].TotalFilesSize = this.FilesSize.ToString();
                    xmlObjects[^1].NbFilesLeftToDo = this.FilesToCopy.ToString();
                    xmlObjects[^1].Progression = this.TotalFiles == 0 || this.FilesToCopy == 0
                        ? "100%"
                        : ((100 - ((double)this.FilesToCopy / this.TotalFiles) * 100)).ToString("0.00") + "%";

                    fileStream.SetLength(0); // Truncate the file before writing
                }

                using (var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.ReadWrite))
                {
                    serializer.Serialize(fileStream, xmlObjects);
                }
            }
        }

        /// <summary>
        /// Function who update dynamically the state file
        /// </summary>
        /// <param name="status"></param>
        public void CreateState(IConfiguration conf)
        {
            string xmlorjson = conf["JsonOrXml"];

            if (xmlorjson == "json")
            {
                string filepath = GeneralTools.LogPath + "\\state.json";

                // Parse the string into a JObject
                List<dynamic>? jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(File.ReadAllText(filepath));

                // Create JObject with our values
                StateFile json = new StateFile(this.Appellation,
                    this.SourcePath,
                    this.TargetPath,
                    "ACTIVE",
                    this.TotalFiles.ToString(),
                    this.FilesSize.ToString(),
                    0 + "%",
                    this.FilesToCopy.ToString()
                );

                // Transform our object into a JObject
                JObject jobject = JObject.FromObject(json);
                
                if (jsonObjects != null)
                {
                    jsonObjects.Add(jobject);
                }
                else
                {
                    // Initialize list and add objects to list
                    jsonObjects = new List<dynamic> { jobject };
                }
                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
            }
            else
            {
                string filepath = GeneralTools.LogPath + "\\state.xml";
                
                // Create a new serializer for our object
                XmlSerializer serializer = new XmlSerializer(typeof(List<StateFile>));
                
                // Try to open the file in a shared mode
                using (var fileStream = new FileStream(filepath, FileMode.OpenOrCreate, FileAccess.ReadWrite,
                           FileShare.ReadWrite))
                {
                    List<StateFile>? xmlObjects;
                    if (fileStream.Length > 0)
                    {
                        // Read the existing XML file content into a List of StateFile objects
                        xmlObjects = (List<StateFile>)serializer.Deserialize(fileStream);
                    }
                    else
                    {
                        xmlObjects = new List<StateFile>();
                    }
                    
                    // Create a new StateFile object
                    StateFile xmlStateFile = new StateFile(this.Appellation,
                        this.SourcePath,
                        this.TargetPath,
                        "ACTIVE",
                        this.TotalFiles.ToString(),
                        this.FilesSize.ToString(),
                        0 + "%",
                        this.FilesToCopy.ToString()
                    );
                
                    xmlObjects.Add(xmlStateFile);
                
                    // Serialize and write the updated List of StateFile objects to the XML file
                    fileStream.Position = 0;
                    serializer.Serialize(fileStream, xmlObjects);
                }
            }
        }

        /// <summary>
        /// Calculate the size of a directory
        /// </summary>+
        /// <returns>The size of the directory</returns>
        protected long DirectorySize() 
        {
            long size = 0;

            if (SourcePath != null && TargetPath != null)
            {
                DirectoryInfo sourceDirectory = new DirectoryInfo(SourcePath);
                DirectoryInfo targetDirectory = new DirectoryInfo(TargetPath);
                
                foreach (FileInfo file in sourceDirectory.EnumerateFiles("*", SearchOption.AllDirectories))
                    size += file.Length;
            }

            return size;
        }

        /// <summary>
        /// Function who save a repository (completeSave and diffSave)
        /// </summary>
        /// <param name="sourceDirectory">The source directory we actually browse</param>
        /// <param name="targetDirectory">The target directory we want to add modifications</param>
        protected void RepositorySave(string? sourceDirectory, string? targetDirectory)
        {

            // Global param
            string? path = null;
            Stopwatch stopwatch = new Stopwatch();
            List<Process> pro = new List<Process>();
            
            IConfiguration conf = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            var businessList = conf.GetSection("Crypto_Ext").Get<List<string>>();
            
            if (sourceDirectory != null && targetDirectory != null ){
                long fileSize;
                if (this is CompleteSave)
                {

                    foreach (var file in Directory.GetFiles(sourceDirectory))
                    {
                        if (targetDirectory != null) path = Path.Combine(targetDirectory, Path.GetFileName(file));
                        stopwatch.Start();
                        if (path != null)
                        {
                            FileInfo fi = new FileInfo(file);

                            // Console.WriteLine(businessList.Contains(fi.Extension));
                            File.Copy(file, path, true);
                            stopwatch.Stop();
                            fileSize = fi.Length;
                            if (businessList != null && businessList.Contains(fi.Extension))
                            {
                                pro.Add(new Process());
                                
                                pro[pro.Count-1].StartInfo = new ProcessStartInfo
                                {
                                    FileName = conf["Crypto_Path"],
                                    Arguments = path + " " + conf["Crypto_Path"] +" encrypt"
                                };
                                pro[pro.Count - 1].Start();

                                // Create a Task for stop the stopwatch cause it need to wait the end of the process
                                pro[pro.Count - 1].WaitForExit();
            
                                // Different values we need for json
                                UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString(),
                                    pro[pro.Count - 1].ExitCode == 0 ? (pro[pro.Count - 1].ExitTime - pro[pro.Count - 1].StartTime).ToString(@"hh\:mm\:ss\.fffffff")
                                        : "Error in encryption", conf);
                            }
                            else
                            {
                                UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString(), "Not encrypted", conf);
                            }
                            this.FilesSize -= fileSize;
                            this.FilesToCopy--;
                        }
                        UpdateState(conf);
                    }
                }
                else
                {
                    foreach (var file in Directory.GetFiles(sourceDirectory))
                    {
                        path = Path.Combine(targetDirectory, Path.GetFileName(file));
                        var path2 = Path.Combine(sourceDirectory, Path.GetFileName(file));
                        var targetInfo = new FileInfo(path);
                        var sourceInfo = new FileInfo(path2);
                        fileSize = 0;
                        stopwatch.Start();
                        FileInfo fi = new FileInfo(file);

                        if (sourceInfo.LastWriteTime > targetInfo.LastWriteTime)
                        {
                            File.Copy(file, path, true);

                            // Recup list of ext in the config file
                            if (businessList != null && businessList.Contains(fi.Extension))
                            {
                                pro.Add(new Process());
                                
                                pro[pro.Count-1].StartInfo = new ProcessStartInfo
                                {
                                    FileName = conf["Crypto_Path"],
                                    Arguments = path + " " + conf["Crypto_Path"] +" encrypt"
                                };
                                pro[pro.Count - 1].Start();

                                // Create a Task for stop the stopwatch cause it need to wait the end of the process
                                pro[pro.Count - 1].WaitForExit();
            
                                // Different values we need for json
                                stopwatch.Stop();
                                UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString(),
                                    pro[pro.Count - 1].ExitCode == 0 ? (pro[pro.Count - 1].ExitTime - pro[pro.Count - 1].StartTime).ToString(@"hh\:mm\:ss\.fffffff")
                                        : "Error in encryption", conf);
                            }
                            else
                            {
                                stopwatch.Stop();
                                UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString(), "Not encrypted", conf);
                            }  
                        }
                        else
                        {
                            this.TotalFiles--;
                        }

                        fileSize = fi.Length;
                        this.FilesSize -= fileSize;
                        this.FilesToCopy--;
                        UpdateState(conf);
                    }
                }
                stopwatch.Reset();


                foreach (var directory in Directory.GetDirectories(sourceDirectory))
                {
                    if (targetDirectory != null) path = Path.Combine(targetDirectory, Path.GetFileName(directory));
                    RepositorySave(directory, path);
                }
            }
        }

        protected void CreateDirectoriesOfADirectory()
        {
            if (SourcePath != null && TargetPath != null)
            {
                foreach (string subDirectory in Directory.GetDirectories(SourcePath, "*", SearchOption.AllDirectories))
                {
                    string relativePath = subDirectory.Replace(SourcePath, "");
                    string targetSubDirectory = Path.Combine(TargetPath, relativePath);
                    Directory.CreateDirectory(TargetPath + targetSubDirectory);
                }
            }
        }
    }
}