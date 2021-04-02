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
    [SerializeField] private AudioClip ballHitBrickSFX;
    [SerializeField] private AudioClip ballHitPaddleSFX;

    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        BallBehaviour.OnBallHittingFloor += ProcessOnBallHittingFloor;
        BallBehaviour.OnBallHittingBrick += ProcessOnBallHittingBrick;
        BallBehaviour.OnBallHittingPaddle += ProcessOnBallHittingPaddle;
    }

    private void OnDisable()
    {
        BallBehaviour.OnBallHittingFloor -= ProcessOnBallHittingFloor;
        BallBehaviour.OnBallHittingBrick -= ProcessOnBallHittingBrick;
        BallBehaviour.OnBallHittingPaddle -= ProcessOnBallHittingPaddle;
    }

    // Start is called before the first frame update
    private void Start()
    {
        scoreDisplay.text = $"Score: {currentScore}";
        turnsDisplay.text = $"Turns: {turns}";
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

    private void ProcessOnBallHittingFloor()
    {
        UpdateTurns(-1);
    }

    private void ProcessOnBallHittingBrick(BrickBehaviour brick)
    {
        PlayBallHitBrickSFX();
        UpdateScore(brick.points);
        UpdateNumberOfBricks();
    }

    private void ProcessOnBallHittingPaddle()
    {
        PlayBallHitPaddleSFX();
    }

    private void UpdateNumberOfBricks()
    {
        numberOfBricks--;

        if (numberOfBricks <= 0)
        {
            EndGame();
        }
    }

    private void PlayBallHitBrickSFX()
    {
        audioSource.clip = ballHitBrickSFX;
        audioSource.Play();
    }

    private void PlayBallHitPaddleSFX()
    {
        audioSource.clip = ballHitPaddleSFX;
        audioSource.Play();
    }
  
}
