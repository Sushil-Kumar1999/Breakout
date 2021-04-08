using UnityEngine;

public class SpeedChange : SpecialEffect
{
    [SerializeField] private float speedFactor;

    private PaddleBehaviour paddle;

    private void Start()
    {
        paddle = FindObjectOfType<PaddleBehaviour>();
    }

    public override void Activate()
    {
        paddle.UpdateSpeed(speedFactor);
    }
}
