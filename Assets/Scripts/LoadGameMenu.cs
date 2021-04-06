using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameMenu : MonoBehaviour
{
    [SerializeField] private Dropdown saveFilesDropdown;
    [SerializeField] private TextMeshProUGUI summaryText;

    private List<string> saveFileNames;
    private SavedGameManager savedGameManager;

    private void Awake()
    {
        saveFileNames = new List<string>(PlayerPrefs.GetString("SAVE_FILES").Split(','));
        savedGameManager = SavedGameManager.GetInstance();
        savedGameManager.Load(saveFileNames[0]);
    }

    private void Start()
    {
        PopulateDropdownList();
        UpdateSummaryText();
    }

    public void Dropdown_IndexChanged(int index)
    {
        savedGameManager.Load(saveFileNames[index]);
        UpdateSummaryText();
    }

    public void RestoreGame()
    {
        GameManager.loadFromSavedGame = true;
        SceneManager.LoadScene(Scenes.GameScene);
    }

    private void PopulateDropdownList()
    {
        saveFilesDropdown.AddOptions(new List<string>(saveFileNames));
    }

    private void UpdateSummaryText()
    {
        string text = $"Label: {SavedGameManager.SavedGame.label}\n\n" +
                      $"Date: {SavedGameManager.SavedGame.saveTime.ToString("d/MM/yyyy")}\n\n" +
                      $"Time: {SavedGameManager.SavedGame.saveTime.ToString("hh:mm:ss tt")}\n\n" +
                      $"Score: {SavedGameManager.SavedGame.score}\n\n" +
                      $"Lives remaining: {SavedGameManager.SavedGame.livesRemaining}\n\n";

        summaryText.text = text;
    }
}
