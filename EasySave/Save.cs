﻿using System.Diagnostics;
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
            string cryptTime)
        {
            string filepath = GeneralTools.LogPath + "\\logs.json";
            
            // Read the existing JSON file content into a string
            string jsonContent = File.ReadAllText(filepath);

            // Parse the string into a JObject
            List<dynamic> jsonObjects = JsonConvert.DeserializeObject<List<dynamic>>(jsonContent);

            // Create a new object
            JsonLog jsonInfoSaveFile = new JsonLog(this.Appellation,
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

        /// <summary>
        /// Function who update dynamically the state file
        /// </summary>
        /// <param name="status"></param>
        public void UpdateState(string status)
        {
            string filepath = GeneralTools.LogPath + "\\state.json";

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
                    if (i != 1 && obj["Name"]?.ToString() == this.Appellation)
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
                jsonObjects = new List<dynamic> { jobject };
                string updatedJson = JsonConvert.SerializeObject(jsonObjects, Formatting.Indented);
                File.WriteAllText(filepath, updatedJson);
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
            GeneralTools.CreateLogsFiles();
            
            // Global param
            string? path = null;
            Stopwatch stopwatch = new Stopwatch();
            
            List<Process> pro = new List<Process>();
            
            IConfiguration conf = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();
            var businessList = conf.GetSection("Crypto_Ext").Get<List<string>>();

            // Params only used for DiffSave
            UpdateState("ACTIVE");
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
                                        : "Error in encryption");
                            }
                            else
                            {
                                UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString(), "Not encrypted");
                            }
                            this.FilesSize -= fileSize;
                            this.FilesToCopy--;
                            UpdateState("ACTIVE");
                            stopwatch.Reset();
                        }
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
                                        : "Error in encryption");
                            }
                            else
                            {
                                stopwatch.Stop();
                                UpdateLogs(file, path, fileSize, stopwatch.Elapsed.ToString(), "Not encrypted");
                            }  
                        }
                        else
                        {
                            this.TotalFiles--;
                        }

                        fileSize = fi.Length;
                        this.FilesSize -= fileSize;
                        this.FilesToCopy--;

                        // Update our logs
                        UpdateState("ACTIVE");
                        stopwatch.Reset();
                    }
                }


                foreach (var directory in Directory.GetDirectories(sourceDirectory))
                {
                    if (targetDirectory != null) path = Path.Combine(targetDirectory, Path.GetFileName(directory));
                    RepositorySave(directory, path);
                }
            }
            UpdateState("END");
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