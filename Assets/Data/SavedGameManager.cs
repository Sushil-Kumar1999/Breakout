using Assets.Data.Models;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SavedGameManager
{
    // only one instance should exist
    public static SavedGame SavedGame { get; set; }

    public void Save()
    {
        FileStream fileStream = new FileStream(GetFilePath(SavedGame.label), FileMode.OpenOrCreate);
        BinaryFormatter binaryFormatter = new BinaryFormatter();
        binaryFormatter.Serialize(fileStream, SavedGame);
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
        return Application.persistentDataPath + "/SaveFiles/" + label + ".dat";
    }
}
