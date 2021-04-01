using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public bool gameOver;

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI turnsDisplay;
    [SerializeField] private int totalTurns;

    private int currentScore;
    private int turns;

    private void Awake()
    {
        turns = totalTurns;
        currentScore = 0;
    }

    private void OnEnable()
    {
        BallBehaviour.OnBallHittingFloor += LoseTurn;
        BallBehaviour.OnBallHittingBrick += UpdateScore;
    }

    private void OnDisable()
    {
        BallBehaviour.OnBallHittingFloor -= LoseTurn;
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

    private void EndGame()
    {
        gameOver = true;
    }
}
