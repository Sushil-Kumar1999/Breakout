public class ExtraLife : SpecialEffect
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public override void Activate()
    {
        gameManager.UpdateLives(1);   
    }
}
