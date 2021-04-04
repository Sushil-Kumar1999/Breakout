using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("Beginning new game");
        SceneManager.LoadScene("Game");
    }

    public void ExitGame()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
