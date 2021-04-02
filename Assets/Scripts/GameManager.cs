using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool gameOver;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI turnsDisplay;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private int totalTurns;
    private int numberOfBricks; // number of bricks left in scene

    private int currentScore;
    private int turns;

    private const string GameScene = "Game";
    private const string StartMenu = "Menu";

    private void Awake()
    {
        turns = totalTurns;
        currentScore = 0;
        numberOfBricks = GameObject.FindGameObjectsWithTag("Brick").Length;
    }

    private void OnEnable()
    {
        BallBehaviour.OnBallHittingFloor += LoseTurn;
        BallBehaviour.OnBallHittingBrick += UpdateNumberOfBricks;
        BallBehaviour.OnBallHittingBrick += UpdateScore;
    }

    private void OnDisable()
    {
        BallBehaviour.OnBallHittingFloor -= LoseTurn;
        BallBehaviour.OnBallHittingBrick -= UpdateNumberOfBricks;
        BallBehaviour.OnBallHittingBrick -= UpdateScore;
    }

    // Start is called before the first frame update
    private void Start()
    {
        scoreDisplay.text = $"Score: {currentScore}";
        turnsDisplay.text = $"Turns: {turns}";
    }

    private void LoseTurn()
    {
        UpdateTurns(-1);
    }

    public void UpdateTurns(int delta)
    {
        turns += delta;

        if (turns <= 0)
        {
            turns = 0;
            EndGame();
        }

        turnsDisplay.text = $"Turns: {turns}";
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

    public void ReturnToStartMenu()
    {
        SceneManager.LoadScene(StartMenu);
    }

    private void EndGame()
    {
        gameOver = true;
        gameOverPanel.SetActive(true);
    }

    private void UpdateNumberOfBricks(int points)
    {
        numberOfBricks--;

        if (numberOfBricks <= 0)
        {
            EndGame();
        }
    }
}
