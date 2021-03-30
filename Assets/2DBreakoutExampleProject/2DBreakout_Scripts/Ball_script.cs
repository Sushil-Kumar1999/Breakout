/// <summary>
/// 2D BREAKOUT Example Project
/// By Danar Kayfi 
/// Twitter: @DanarKayfi 
/// Personal Blog: https://danarkayfi.wordpress.com
/// Games I developed: https://bug-games.com
/// 
/// Special Thanks to Kenney for the amazing CC0 Graphic Assets: http://kenney.nl
/// 
/// License: (CC0 1.0 Universal) https://creativecommons.org/publicdomain/zero/1.0/
/// You're free to use these game assets in any project, 
/// personal or commercial. There's no need to ask permission before using these. 
/// Giving attribution is not required, but is greatly appreciated
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_script : MonoBehaviour 
{
	//Public Variables will appear in the Inspector 
    public float ballInitialVelocity = 600f;	//The initial velocity of the Ball
    public Text txtScore;	//Connected to the Score text
    public AudioClip sfxBrickHit;	//Connected to SFX of the Brick Hit
	public AudioClip sfxPaddleBorderHit;	//Connected to SFX of the Paddle Border Hit

	//Private Variables
    private Rigidbody2D rb2D;	//Will Connect with the Ball Rigidbody 2D Component
	private AudioSource audioSource; //Will Connect with the Ball Audio Source Component
	private bool ballInPlay;	//Boolean to know if the Ball inPlay (in moving) or not
    private int totalScore;		//Counter for the total Score


	// Use this for initialization
	void Start () 
    {
		rb2D = GetComponent<Rigidbody2D>();		//Connect rb2D with the Ball Rigidbody 2D Component
		audioSource = GetComponent<AudioSource>();		//Connect rb2D with the Ball Audio Source Component
	}
	
	// Update is called once per frame
	void Update () 
    {
		//If The player pressed Space button & The ball is not inPlay (in moving)  & The game can start
        if (Input.GetButtonDown("Jump") && ballInPlay == false && GameManager_script.startGame)
        {
            transform.parent = null;	//Disconnect the Ball from the Paddle
			ballInPlay = true;		//The Ball is inPlay (is moving)
			rb2D.isKinematic = false;		//Uncheck the isKinematic in the Rigidbody 2D

            if(Input.GetAxis ("Horizontal") == 0f)		//Checking if the Paddle is standing still
				rb2D.AddForce(new Vector2(1f, ballInitialVelocity));	//Adding force to the Balle
			else if(Input.GetAxis ("Horizontal") > 0f)		//Checking if the Paddle is moving right
				rb2D.AddForce(new Vector2(ballInitialVelocity, ballInitialVelocity));		//Adding force to the Balle
			else if(Input.GetAxis ("Horizontal") < 0f)		//Checking if the Paddle is moving left
				rb2D.AddForce(new Vector2(-ballInitialVelocity, ballInitialVelocity));		//Adding force to the Balle
        }


	}

	//Will be called when a Collision Enter accure 
    void OnCollisionEnter2D(Collision2D coll)
    {
		//If a collision caused by a Brick 
        if (coll.gameObject.tag == "Brick")
        {
            audioSource.clip = sfxBrickHit;		//Load Brick Hit SFX to the Audio Source component
			audioSource.Play();		//Play Brick Hit SFX
            totalScore += coll.gameObject.GetComponent<Brick_script>().score;		//Add this Brick score to the total score 
            txtScore.text = totalScore.ToString("000000");		//Show the total score as a Text containing 6 digits
            Destroy(coll.gameObject);		//Destroy the brick
        }

		//If a collision caused by a Paddle or a  Border
        if (coll.gameObject.tag == "Paddle" || coll.gameObject.tag == "Border")
        {
			audioSource.clip = sfxPaddleBorderHit;		//Load Paddle/Border Hit SFX to the Audio Source component
			audioSource.Play();		//Play Brick Hit SFX
        }
    }
}
