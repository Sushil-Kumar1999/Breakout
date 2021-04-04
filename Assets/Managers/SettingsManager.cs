using Assets.Models;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public string fileName = "Settings.json";

    public void SaveSettings(Settings settings)
    {
        string json = JsonUtility.ToJson(settings, true);

        FileUtilities.WriteToFile(fileName, json);
    }

    public Settings LoadSettings()
    {
        string json = FileUtilities.ReadFromFile(fileName);

        return JsonUtility.FromJson<Settings>(json);
    }
}
