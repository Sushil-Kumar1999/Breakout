public class InstantDeath : SpecialEffect
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void Activate()
    {
        gameManager.InvokeInstantDeath();
    }
}
