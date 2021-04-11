using Assets.Data;
using Assets.Data.Models;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private SettingsManager settingsManager;
    private Settings settings;

    [SerializeField] private AudioClip ballHitBrickSFX;
    [SerializeField] private AudioClip ballHitPaddleSFX;
    [SerializeField] private AudioClip ballHitFloorSFX;
    [SerializeField] private GameObject mainCamera;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        settingsManager = SettingsManager.GetInstance();
        settings = settingsManager.Load();

        // if background music setting is set to false stop background music
        if (!settings.backgroundMusic)
        {
            mainCamera.GetComponent<AudioSource>().Stop();
        }
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
        if (settings.brickSfx)
        {
            audioSource.clip = ballHitBrickSFX;
            audioSource.Play();
        }
    }

    private void PlayBallHitPaddleSFX()
    {
        if (settings.paddleSfx)
        {
            audioSource.clip = ballHitPaddleSFX;
            audioSource.Play();
        }
    }

    private void PlayBallHitFloorSFX()
    {
        audioSource.clip = ballHitFloorSFX;
        audioSource.Play();
    }    
}
