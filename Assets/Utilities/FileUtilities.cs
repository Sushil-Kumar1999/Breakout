using System.IO;
using UnityEngine;

public class FileUtilities : MonoBehaviour
{
    public static void WriteToFile(string fileName, string json)
    {
        string filePath = GetFilePath(fileName);
        FileStream fileStream = new FileStream(filePath, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    public static string ReadFromFile(string fileName)
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

    public static string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
