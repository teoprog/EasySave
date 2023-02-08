using Newtonsoft.Json;

namespace EasySave;

public class JsonState
{
    [JsonProperty("Name")]
    private string Name  {get; set; }

    [JsonProperty("FileSource")]
    private string FileSource  {get; set; }

    [JsonProperty("FileTarget")]
    private string FileTarget  {get; set; }

    [JsonProperty("State")]
    private string State  {get; set; }

    [JsonProperty("TotalFilesToCopy")]
    private string TotalFilesToCopy  {get; set; }

    [JsonProperty("TotalFilesSize")]
    private string TotalFilesSize  {get; set; }

    [JsonProperty("Progression")]
    private string Progression  {get; set; }

    public JsonState(string name, string fileSource, string fileTarget, string state, string totalFilesToCopy, string totalFilesSize, string progression)
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