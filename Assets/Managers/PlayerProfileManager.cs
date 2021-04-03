using Assets.Models;
using System.IO;
using UnityEngine;

public class PlayerProfileManager : MonoBehaviour
{
    public void Save(PlayerProfile playerProfile)
    {
        string json = JsonUtility.ToJson(playerProfile, true);
        
        WriteToFile(GetFileName(playerProfile.playerName), json);
    }

    public PlayerProfile Load(string playerName)
    {
        string json = ReadFromFile(GetFileName(playerName));

        return JsonUtility.FromJson<PlayerProfile>(json);
    }

    private void WriteToFile(string fileName, string json)
    {
        string filePath = GetFilePath(fileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile(string fileName)
    {
        string json = "";
        string filePath = GetFilePath(fileName);

        if (File.Exists(filePath))
        {
            using (StreamReader reader = new StreamReader(filePath))
            {
                json = reader.ReadToEnd();
            }
        }
        else
        {
            Debug.LogError($"File {fileName} at path {filePath} not found");
        }

        return json;
    }

    private string GetFileName(string playerName)
    {
        return playerName + ".json";
    }

    private string GetFilePath(string fileName)
    {
       return Application.persistentDataPath + "/PlayerProfiles/" +  fileName;
    }
}
