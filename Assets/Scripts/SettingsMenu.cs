using Assets.Data.Models;
using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    private DataManager<Settings> settingsManager;
    private Settings settings;

    private void Awake()
    {
        settingsManager = new DataManager<Settings>("Settings.json");
        settings = settingsManager.Load();
    }

    public void ToggleBallHitPaddleSfx(bool isEnabled)
    {
        settings.paddleSfx = isEnabled;
    }

    public void ToggleBallHitBrickSfx(bool isEnabled)
    {
        settings.brickSfx = isEnabled;
    }

    public void ToggleBackgroundMusic(bool isEnabled)
    {
        settings.backgroundMusic = isEnabled;
    }

    public void SaveSettings()
    {
        settingsManager.Save(settings);
    }
}
