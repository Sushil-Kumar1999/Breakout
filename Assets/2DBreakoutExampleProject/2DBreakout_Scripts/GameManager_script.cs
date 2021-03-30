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
using UnityEngine.SceneManagement;

public class GameManager_script : MonoBehaviour 
{
	//Public Variables will appear in the Inspector 
	public GameObject mainMenuPanel;	//Connected to the Main Menu Panel
	public GameObject gameOverPanel;	//Connected to the Game Over Panel
	public static bool startGame = false;	//Boolean to know if the game started or not
	public static bool gameOver = false;	//Boolean to know if the game is over or not

	// Update is called once per frame
	void Update () 
    {
		//If The player pressed Space button & The game didnt start yet
        if (Input.GetButtonDown("Jump") && startGame == false)
        {
            startGame = true;		//Turn Start Game boolean to true
            mainMenuPanel.SetActive(false);		//Turn off Main Menu Panel
        }

        if (Input.GetButtonDown("Jump") && gameOver == true)
        {
			startGame = false;		//Turn Start Game boolean to false
			gameOver = false;		//Turn Game Over boolean to false
            SceneManager.LoadScene("MainGame");		//Load Main Game Scene
        }
	}
}
