using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public List<Tutorial> tutorials = new List<Tutorial>();
    public TextMeshProUGUI explanationText;
    public GameObject tutorialPanel;
    public float waitTime = 2.5f;

    private static TutorialManager instance;
    public static TutorialManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<TutorialManager>();
            }

            if (instance == null)
            {
                Debug.LogWarning("There is no TutorialManager");
            }

            return instance;
        }
    }

    private Tutorial currentTutorial;

    private void Start()
    {
        if (GameManager.loadFromSavedGame)
        {
            Destroy(gameObject);
        }
        else
        {
            SetNextTutorial(0);
        }
    }

    private void Update()
    {
        if (currentTutorial)
        {
            currentTutorial.CheckIfInProgress();
        }
        else
        {
            waitTime -= Time.deltaTime;
            if (waitTime < 0)
            {
                tutorialPanel.SetActive(false);
            }
        }
    }

    public void CompleteCurrentTutorial()
    {
        SetNextTutorial(currentTutorial.order + 1);
    }

    public void SetNextTutorial(int nextOrder)
    {
        currentTutorial = GetTutorialByOrder(nextOrder);

        if (!currentTutorial)
        {
            CompletedAllTutorials();
            return;
        }

        explanationText.text = currentTutorial.explanation;
    }

    public void CompletedAllTutorials()
    {
        explanationText.text = "Get the highest score possible of 480 to win the game.";
    }

    public Tutorial GetTutorialByOrder(int order)
    {
       return tutorials.Find(t => t.order == order);
    }
}
