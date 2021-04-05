using Assets.Data.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavedGameManager
{
    public SavedGame SavedGame { get; set; }

    public void Save(SavedGame savedGame)
    {
        FileStream fileStream = new FileStream(GetFilePath(savedGame.label), FileMode.OpenOrCreate);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, savedGame);
        fileStream.Close();
    }

    public void Load(string label)
    {
        FileStream fileStream = new FileStream(GetFilePath(label), FileMode.Open);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        SavedGame = (SavedGame) binaryFormatter.Deserialize(fileStream);
        fileStream.Close();
    }

    public static SavedGameManager GetInstance()
    {
        return new SavedGameManager();
    }

    private string GetFilePath(string label)
    {
        return Application.persistentDataPath + "/" + label + ".dat";
    }
}
