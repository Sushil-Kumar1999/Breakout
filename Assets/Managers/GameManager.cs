using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Assets.Data.Models;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public bool gameOver;
    public static bool loadFromSavedGame;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI livesDisplay;
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    [SerializeField] private TMP_InputField labelInputField;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private int totalLives;

    private int numberOfBricks; // number of bricks left in scene
    private int currentScore;
    private int currentLives;
    private int currentHighScore;
    private List<SerializableVector2> hitBrickPositions;

    private SavedGameManager savedGameManager;

    private void Awake()
    {
        ResetTimeToNormal();
        InitalizeGame();
        currentHighScore = PlayerPrefs.GetInt("HIGH_SCORE");
        savedGameManager = SavedGameManager.GetInstance();
        hitBrickPositions = new List<SerializableVector2>();
    }

    private void OnEnable()
    {
        BallBehaviour.OnBallHittingFloor += ProcessOnBallHittingFloor;
        BallBehaviour.OnBallHittingBrick += ProcessOnBallHittingBrick;
    }

    private void OnDisable()
    {
        BallBehaviour.OnBallHittingFloor -= ProcessOnBallHittingFloor;
        BallBehaviour.OnBallHittingBrick -= ProcessOnBallHittingBrick;
    }

    private void Start()
    {
        scoreDisplay.text = $"Score: {currentScore}";
        livesDisplay.text = $"Lives: {currentLives}";
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    private void InitalizeGame()
    {
        if (loadFromSavedGame)
        {
            currentLives = SavedGameManager.SavedGame.livesRemaining;
            currentScore = SavedGameManager.SavedGame.score;

            DestroyBricksFromSavedGame();
        } 
        else
        {
            currentLives = totalLives;
            currentScore = 0;
        }

        numberOfBricks = ComputeNumberOfBricks();
    }

    // Recreating the state of bricks by recording position of each destroyed brick in a SavedGame and
    // destorying those bricks when loading from that SavedGame
    private void DestroyBricksFromSavedGame()
    {
        BrickBehaviour[] brickBehaviours = FindObjectsOfType<BrickBehaviour>();
        List<GameObject> bricksToBeDestroyed = new List<GameObject>();

        foreach (var pos in SavedGameManager.SavedGame.destroyedBrickPositions)
        {
            foreach (var bb in brickBehaviours)
            {
                if (bb.gameObject.transform.position.x == pos.x && bb.gameObject.transform.position.y == pos.y)
                {
                    bricksToBeDestroyed.Add(bb.gameObject);
                }
            }
        }

        foreach (var brick in bricksToBeDestroyed) { DestroyImmediate(brick); }
    }

    private int ComputeNumberOfBricks()
    {
        return FindObjectsOfType<BrickBehaviour>().Length;
    }

    public void UpdateLives(int delta)
    {
        currentLives += delta;

        if (currentLives <= 0)
        {
            currentLives = 0;
            EndGame();
        }

        livesDisplay.text = $"Lives: {currentLives}";
    }

    public void UpdateScore(int delta)
    {
        currentScore += delta;
        scoreDisplay.text = $"Score: {currentScore}";
    }

    public void PlayAgain()
    {
        ResetTimeToNormal();
        SceneManager.LoadScene(Scenes.GameScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(Scenes.MainMenu);
    }

    private void EndGame()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
        FreezeTime();

        if (currentScore > currentHighScore)
        {
            PlayerPrefs.SetInt("HIGH_SCORE", currentScore);
            highScoreDisplay.text = $"New High Score Achieved!\n {currentScore}";
        }
        else
        {
            highScoreDisplay.text = $"High Score: {currentHighScore}\nYour score: {currentScore}";
        }
    }

    private void PauseGame()
    {
        optionsPanel.SetActive(true);
        FreezeTime();
    }

    public void ResumeGame()
    {
        optionsPanel.SetActive(false);
        ResetTimeToNormal();
    }

    private void ProcessOnBallHittingFloor()
    {
        UpdateLives(-1);
    }

    private void ProcessOnBallHittingBrick(BrickBehaviour brick)
    {
        SerializableVector2 hitBrickPosition = 
            new SerializableVector2(brick.gameObject.transform.position.x, brick.gameObject.transform.position.y);
        hitBrickPositions.Add(hitBrickPosition);

        UpdateScore(brick.points);
        UpdateNumberOfBricks(); 
    }

    private void UpdateNumberOfBricks()
    {
        numberOfBricks--;

        if (numberOfBricks <= 0)
        {
            EndGame();
        }
    } 

    public void SaveGame()
    {
        SavedGame savedGame = new SavedGame();
        savedGame.label = labelInputField.text;
        savedGame.score = currentScore;
        savedGame.hasHighScore = currentScore >= currentHighScore;
        savedGame.livesRemaining = currentLives;

        savedGame.destroyedBrickPositions = hitBrickPositions;

        savedGame.saveTime = DateTime.Now;
        SavedGameManager.SavedGame = savedGame;
        savedGameManager.Save();

        string currentSaveFiles = PlayerPrefs.GetString("SAVE_FILES", string.Empty);
        string updatedSaveFiles = string.IsNullOrEmpty(currentSaveFiles)
                                  ? savedGame.label
                                  : currentSaveFiles + "," + savedGame.label;
        PlayerPrefs.SetString("SAVE_FILES", updatedSaveFiles);
        PlayerPrefs.Save();
        Debug.Log("Game saved");
    }

    public static void FreezeTime()
    {
        Time.timeScale = 0.0f;
    }

    public static void ResetTimeToNormal()
    {
        if (Time.timeScale != 1.0f)
        {
            Time.timeScale = 1.0f;
        }
    }
}
