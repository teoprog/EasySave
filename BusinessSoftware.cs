using System.Diagnostics;
using Newtonsoft.Json;

namespace EasySave;

public class BusinessSoftware
{
    public List<string> Name { get; set; }

    public BusinessSoftware()
    {
        this.Name = new List<string>();
        
        // In our Name affect the content of the json file
        using (StreamReader reader = new StreamReader(GeneralTools.DirectoryPath + "/businessSoftware.json"))
        {
            string json = reader.ReadToEnd();
            
            dynamic data = JsonConvert.DeserializeObject(json);
            
            if(data != null) this.Name = data.Name.ToObject<List<string>>();
        }
    }

    public bool Add(string processName)
    {
        if (verifyProcessExistence(processName) && !this.Name.Contains(processName))
        {
            this.Name.Add(processName);
            File.WriteAllText(GeneralTools.DirectoryPath + "/businessSoftware.json", JsonConvert.SerializeObject(this));
            return true;
        }
        return false;
    }

    public bool verifyProcessExistence(string processName)
    {
        bool processExists = false;

        Process[] processes = Process.GetProcesses();
        foreach (Process process in processes)
        {
            if (string.Equals(process.ProcessName, processName, StringComparison.OrdinalIgnoreCase) )
            {
                processExists = true;
                break;
            }
        }

        return processExists;
    }
}