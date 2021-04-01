using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI scoreDisplay;
    [SerializeField] private TextMeshProUGUI turnsDisplay;
    [SerializeField] private int totalTurns;

    private int currentScore;
    private int currentTurnCount;

    private void Awake()
    {
        currentTurnCount = totalTurns;
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
        turnsDisplay.text = $"Turns: {currentTurnCount}";

        //UpdateTurns();
    }

    private void LoseTurn()
    {
        UpdateTurns(-1);
    }

    public void UpdateTurns(int delta)
    {
        currentTurnCount += delta;

        // check if currentTurnCount < 0
        turnsDisplay.text = $"Turns: {currentTurnCount}";
    }

    public void UpdateScore(int delta)
    {
        currentScore += delta;
        scoreDisplay.text = $"Score: {currentScore}";
    }

}
