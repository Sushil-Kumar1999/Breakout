using UnityEngine;

public class TimedTutorial : Tutorial
{
    public float displayDuration = 4.5f;

    public override void CheckIfInProgress()
    {
       
        displayDuration -= Time.deltaTime;

        if (displayDuration < 0)
        {
            TutorialManager.Instance.CompleteCurrentTutorial();
        }
    }
}
