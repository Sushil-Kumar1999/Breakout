using Assets.Data.Models;
using UnityEngine;

namespace Assets.Data
{
    public class SettingsManager : IDataManager<Settings>
    {
        private readonly string fileName = "Settings.json";

        public void Save(Settings settings)
        {
            string json = JsonUtility.ToJson(settings, true);

            JsonFiles.WriteToFile(fileName, json);
        }

        public Settings Load()
        {
            string json = JsonFiles.ReadFromFile(fileName);

            return JsonUtility.FromJson<Settings>(json);
        }

        public static SettingsManager GetInstance()
        {
            return new SettingsManager();
        }
    }
}
