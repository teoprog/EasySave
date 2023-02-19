using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EasySave
{

    public class LogFile
    {
        [JsonProperty("Name"), XmlElement("Name")]
        private string? Name { get; set; }

        [JsonProperty("FileSource"), XmlElement("FileSource")]
        private string FileSource { get; set; }

        [JsonProperty("FileTarget"), XmlElement("FileTarget")]
        private string? FileTarget { get; set; }

        [JsonProperty("DestPath"), XmlElement("DestPath")]
        private string? DestPath { get; set; }

        [JsonProperty("FileSize"), XmlElement("FileSize")]
        private string FileSize { get; set; }

        [JsonProperty("FileTransferTime"), XmlElement("FileTransfertTime")]
        private string FileTransferTime { get; set; }

        [JsonProperty("Time"), XmlElement("Time")]
        private string Time { get; set; }

        [JsonProperty("EncryptTime"), XmlElement("EncryptTime")]
        private string EncryptTime { get; set; }

        public LogFile(string? name, string fileSource, string? fileTarget, string? destPath, string fileSize, string fileTransferTime, string time, string encryptTime)
        {
            Name = name;
            FileSource = fileSource;
            FileTarget = fileTarget;
            DestPath = destPath;
            FileSize = fileSize;
            FileTransferTime = fileTransferTime;
            Time = time;
            EncryptTime = encryptTime;
        }
    }
}