using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadGameMenu : MonoBehaviour
{
    [SerializeField] private Dropdown saveFilesDropdown;
    [SerializeField] private TextMeshProUGUI summaryText;
    [SerializeField] private Button continueButton;

    private List<string> saveFileNames;
    private SavedGameManager savedGameManager;
    private bool saveFilesAbsent;

    private void Awake()
    {
        string saveFilesValue = PlayerPrefs.GetString("SAVE_FILES", string.Empty);
        saveFilesAbsent = string.IsNullOrEmpty(saveFilesValue);

        if (saveFilesAbsent)
        {
            continueButton.gameObject.SetActive(false);
        } 
        else
        {
            saveFileNames = new List<string>(saveFilesValue.Split(','));
            savedGameManager = SavedGameManager.GetInstance();
            savedGameManager.Load(saveFileNames[0]);
        }
    }

    private void Start()
    {
        PopulateDropdownList();
        UpdateSummaryText();
    }

    public void Dropdown_IndexChanged(int index)
    {
        if (!saveFilesAbsent)
        {
            savedGameManager.Load(saveFileNames[index]);
            UpdateSummaryText();
        }
    }

    public void RestoreGame()
    {
        GameManager.loadFromSavedGame = true;
        SceneManager.LoadScene(Scenes.GameScene);
    }

    private void PopulateDropdownList()
    {
        if (saveFilesAbsent)
        {
            saveFilesDropdown.AddOptions(new List<string> { "No save file available" });
        }
        else
        {
            saveFilesDropdown.AddOptions(saveFileNames);
        }
    }

    private void UpdateSummaryText()
    {
        if (!saveFilesAbsent)
        {
            string text = $"Label: {SavedGameManager.SavedGame.label}\n\n" +
                          $"Date: {SavedGameManager.SavedGame.saveTime.ToString("d/MM/yyyy")}\n\n" +
                          $"Time: {SavedGameManager.SavedGame.saveTime.ToString("hh:mm:ss tt")}\n\n" +
                          $"Score: {SavedGameManager.SavedGame.score}\n\n" +
                          $"Lives remaining: {SavedGameManager.SavedGame.livesRemaining}\n\n";

            summaryText.text = text;
        }
        else
        {
            summaryText.text = "No save files available.\n\n" +
                               "Start a new game and save your progress first.";
        }
    }
}
