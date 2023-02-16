using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace EasySave;

public class BusinessSoftware
{
    public List<string>? Name { get; set; }
    
    public BusinessSoftware()
    {
        IConfiguration business = new ConfigurationBuilder().AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).Build();

        var businessList = business.GetSection("Business_Software").Get<List<string>>();
        
        this.Name = businessList ?? new List<string>();
        
        GeneralTools.CreateLogsFiles();
    }

    public bool Add(string processName)
    {
        if (verifyProcessExistence(processName) && !this.Name.Contains(processName))
        {
            this.Name.Add(processName);
            var business = JsonConvert.DeserializeObject<Dictionary<string, object>>(File.ReadAllText("appsettings.json"));
            business["Business_Software"] = this.Name;
            var json = JsonConvert.SerializeObject(business, Formatting.Indented);

            File.WriteAllText("appsettings.json", json);
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