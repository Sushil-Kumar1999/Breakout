using Assets.Data.Models;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource audioSource;
    private DataManager<Settings> settingsManager;
    private Settings settings;

    [SerializeField] private AudioClip ballHitBrickSFX;
    [SerializeField] private AudioClip ballHitPaddleSFX;
    [SerializeField] private AudioClip ballHitFloorSFX;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        settingsManager = new DataManager<Settings>("Settings.json");
        settings = settingsManager.Load();

        // if background music setting is set to false stop background music
        if (!settings.backgroundMusic)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
        }
    }

    private void OnEnable()
    {
        if (settings.paddleSfx)
        {
            BallBehaviour.OnBallHittingPaddle += PlayBallHitPaddleSFX;
        }

        if (settings.brickSfx)
        {
            BallBehaviour.OnBallHittingBrick += PlayBallHitBrickSFX;
        }
        
        BallBehaviour.OnBallHittingFloor += PlayBallHitFloorSFX;
    }

    private void OnDisable()
    {
        if (settings.paddleSfx)
        {
            BallBehaviour.OnBallHittingPaddle -= PlayBallHitPaddleSFX;
        }

        if (settings.brickSfx)
        {
            BallBehaviour.OnBallHittingBrick -= PlayBallHitBrickSFX;
        }

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
