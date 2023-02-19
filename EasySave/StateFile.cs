using System.Xml.Serialization;
using Newtonsoft.Json;

namespace EasySave
{
    public class StateFile
    {
        [JsonProperty("Name"), XmlElement("Name")]
        private string? Name  {get; set; }

        [JsonProperty("FileSource"), XmlElement("FileSource")]
        private string? FileSource  {get; set; }

        [JsonProperty("FileTarget"), XmlElement("FileTarget")]
        private string? FileTarget  {get; set; }

        [JsonProperty("State"), XmlElement("State")]
        private string State  {get; set; }

        [JsonProperty("TotalFilesToCopy"), XmlElement("TotalFilesToCopy")]
        private string TotalFilesToCopy  {get; set; }

        [JsonProperty("TotalFilesSize"), XmlElement("TotalFilesSize")]
        private string TotalFilesSize  {get; set; }

        [JsonProperty("Progression"), XmlElement("Progression")]
        private string Progression  {get; set; }

        public StateFile(string? name, string? fileSource, string? fileTarget, string state, string totalFilesToCopy, string totalFilesSize, string progression)
        {
            Name = name;
            FileSource = fileSource;
            FileTarget = fileTarget;
            State = state;
            TotalFilesToCopy = totalFilesToCopy;
            TotalFilesSize = totalFilesSize;
            Progression = progression;
        }
    }
}