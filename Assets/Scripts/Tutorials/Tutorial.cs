using UnityEngine;

public abstract class Tutorial : MonoBehaviour
{
    public int order;

    [TextArea(3, 10)]
    public string explanation;

    private void Awake()
    {
        TutorialManager.Instance.tutorials.Add(this);
    }

    /// <summary>
    /// Called every frame by TutorialManager
    /// </summary>
    public abstract void CheckIfInProgress();
}
