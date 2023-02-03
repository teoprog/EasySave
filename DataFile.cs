using System;
using Newtonsoft.Json;

namespace BackupApplication
{
    public class BackupJob
    {
        public string Appellation { get; set; }
        public string SourceDirectory { get; set; }
        public string TargetDirectory { get; set; }
        public BackupType Type { get; set; }

        public enum BackupType
        {
            Complete,
            Differential
        }

        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            System.IO.File.WriteAllText(Appellation + ".json", json);
        }

        public static BackupJob Load(string fileName)
        {
            string json = System.IO.File.ReadAllText(fileName);
            return JsonConvert.DeserializeObject<BackupJob>(json);
        }
    }
}
