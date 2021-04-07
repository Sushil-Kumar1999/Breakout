using System.Collections.Generic;
using UnityEngine;

public class KeysTutorial : Tutorial
{
    public List<KeyCode> keys = new List<KeyCode>();

    public override void CheckIfInProgress()
    {
        for (int i = 0; i < keys.Count; i++)
        {
            if (Input.GetKeyDown(keys[i]))
            {
                keys.RemoveAt(i);
                break;
            }
        }

        if (keys.Count == 0)
        {
            TutorialManager.Instance.CompleteCurrentTutorial();
        }
    }
}
