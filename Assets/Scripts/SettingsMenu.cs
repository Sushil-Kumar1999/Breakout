using Assets.Data.Models;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    private DataManager<Settings> settingsManager;
    private Settings settings;

    [SerializeField] private Toggle backgroundMusicToggle;
    [SerializeField] private Toggle paddleSfxToggle;
    [SerializeField] private Toggle brickSfxToggle;

    private void Awake()
    {
        settingsManager = new DataManager<Settings>("Settings.json");
        settings = settingsManager.Load();
    }

    private void Start()
    {
        backgroundMusicToggle.isOn = settings.backgroundMusic;
        paddleSfxToggle.isOn = settings.paddleSfx;
        brickSfxToggle.isOn = settings.brickSfx;
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
