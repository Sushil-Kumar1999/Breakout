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

public class GameOverDetector_script : MonoBehaviour 
{
	//Public Variables will appear in the Inspector 
	public GameObject gameOverPanel;	//Connected to Game Over Panel

	//Will be called when a Collision Enter accur
    void OnCollisionEnter2D(Collision2D coll)
    {
		//If a collision caused by the Ball
        if (coll.gameObject.tag == "Ball")
        {
            Destroy(coll.gameObject);		//Destroy the ball
			GameManager_script.gameOver = true;		//Turn Game Over boolean to true
			gameOverPanel.SetActive(true);		//Turn on the Game over Panel
        }
    }
}
