using System.Diagnostics;
using System.Security.Cryptography;
using CryptSharp;
using Newtonsoft.Json;

namespace EasySave;

public class CryptoSoft
{
    public List<string> Name { get; set; }

    public CryptoSoft()
    {
        this.Name = new List<string>();
        
        GeneralTools.CreateLogsFiles();
        
        // In our Name affect the content of the json file
        using (StreamReader reader = new StreamReader(GeneralTools.DirectoryPath + "/cryptoSoft.json"))
        {
            string json = reader.ReadToEnd();
            
            dynamic data = JsonConvert.DeserializeObject(json);
            
            if(data != null) this.Name = data.Name.ToObject<List<string>>();
        }
    }

    public bool Add(string extensionName)
    {
        if (extensionName.Substring(0,1) != ".") extensionName = "." + extensionName;
        if (!this.Name.Contains(extensionName))
        {
            this.Name.Add(extensionName);
            File.WriteAllText(GeneralTools.DirectoryPath + "/cryptoSoft.json", JsonConvert.SerializeObject(this));
            return true;
        }
        return false;
    }

    public void Cryp()
    { 
        string inputFile = @"C:\Users\bvict\OneDrive\Bureau\Nouveau dossier\azer.txt";
        string outputFile = @"C:\Users\bvict\OneDrive\Bureau\Nouveau dossier\ok.txt";

        string password = "mysecretpassword";
        byte[] salt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

        using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(password, salt, 1000).GetBytes(aes.KeySize / 8);
            aes.Key = key;
            aes.IV = new byte[aes.BlockSize / 8];

            using (FileStream input = new FileStream(inputFile, FileMode.Open))
            using (FileStream output = new FileStream(outputFile, FileMode.Create))
            using (CryptoStream encryptor = new CryptoStream(output, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                input.CopyTo(encryptor);
            }
        }
    }

    public void Decrypt()
    {
        string inputFile = @"C:\Users\bvict\OneDrive\Bureau\Nouveau dossier\ok.txt";
        string outputFile = @"C:\Users\bvict\OneDrive\Bureau\Nouveau dossier\ab.txt";

        string password = "mysecretpassword";
        byte[] salt = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08 };

            using (Aes aes = Aes.Create())
        {
            aes.KeySize = 256;
            aes.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(password, salt, 1000).GetBytes(aes.KeySize / 8);
            aes.Key = key;
            aes.IV = new byte[aes.BlockSize / 8];

            using (FileStream input = new FileStream(inputFile, FileMode.Open))
            using (FileStream output = new FileStream(outputFile, FileMode.Create))
            using (CryptoStream decryptor = new CryptoStream(input, aes.CreateDecryptor(), CryptoStreamMode.Read))
            {
                decryptor.CopyTo(output);
            }
        }
    }
}