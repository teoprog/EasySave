using Newtonsoft.Json;

namespace EasySave;

public class JsonLog
{
    [JsonProperty("Name")]
    private string? Name  {get; set; }

    [JsonProperty("FileSource")]
    private string FileSource {get; set; }
    
    [JsonProperty("FileTarget")]
    private string? FileTarget {get; set; }

    [JsonProperty("DestPath")]
    private string? DestPath {get; set; }

    [JsonProperty("FileSize")]
    private string FileSize {get; set; }

    [JsonProperty("FileTransferTime")]
    private string FileTransferTime {get; set; }

    [JsonProperty("Time")]
    private string Time {get; set; }

    public JsonLog(string? name, string fileSource, string? fileTarget, string? destPath, string fileSize, string fileTransferTime, string time)
    {
        Name = name;
        FileSource = fileSource;
        FileTarget = fileTarget;
        DestPath = destPath;
        FileSize = fileSize;
        FileTransferTime = fileTransferTime;
        Time = time;
    }
}