using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI livesDisplay;
    [SerializeField] private TextMeshProUGUI highScoreDisplay;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject saveGamePanel;
    [SerializeField] private int totalLives;

    private int numberOfBricks; // number of bricks left in scene
    private int currentScore;
    private int currentLives;
    private int currentHighScore;

    private const string GameScene = "Game";
    private const string MainMenu = "MainMenu";

    private void Awake()
    {
        currentLives = totalLives;
        currentScore = 0;
        numberOfBricks = ComputeNumberOfBricks();
        currentHighScore = PlayerPrefs.GetInt("HIGH_SCORE");
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

    // Start is called before the first frame update
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

    private int ComputeNumberOfBricks()
    {
        return GameObject.FindGameObjectsWithTag("RedBrick").Length +
               GameObject.FindGameObjectsWithTag("OrangeBrick").Length +
               GameObject.FindGameObjectsWithTag("GreenBrick").Length +
               GameObject.FindGameObjectsWithTag("YellowBrick").Length;
    }

    public void UpdateTurns(int delta)
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
        SceneManager.LoadScene(GameScene);
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenu);
    }

    private void EndGame()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);

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
        saveGamePanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ResumeGame()
    {
        saveGamePanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    private void ProcessOnBallHittingFloor()
    {
        UpdateTurns(-1);
    }

    private void ProcessOnBallHittingBrick(BrickBehaviour brick)
    {
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
        Debug.Log("Saving game");
    }
}
