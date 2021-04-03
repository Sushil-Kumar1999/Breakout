using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip ballHitBrickSFX;
    [SerializeField] private AudioClip ballHitPaddleSFX;
    [SerializeField] private AudioClip ballHitFloorSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        BallBehaviour.OnBallHittingPaddle += PlayBallHitPaddleSFX;
        BallBehaviour.OnBallHittingBrick += PlayBallHitBrickSFX;
        BallBehaviour.OnBallHittingFloor += PlayBallHitFloorSFX;
    }

    private void OnDisable()
    {
        BallBehaviour.OnBallHittingPaddle -= PlayBallHitPaddleSFX;
        BallBehaviour.OnBallHittingBrick -= PlayBallHitBrickSFX;
        BallBehaviour.OnBallHittingFloor -= PlayBallHitFloorSFX;
    }

    private void PlayBallHitBrickSFX(BrickBehaviour brick)
    {
        audioSource.clip = ballHitBrickSFX;
        audioSource.Play();
    }

    private void PlayBallHitPaddleSFX()
    {
        audioSource.clip = ballHitPaddleSFX;
        audioSource.Play();
    }

    private void PlayBallHitFloorSFX()
    {
        audioSource.clip = ballHitFloorSFX;
        audioSource.Play();
    }
}
