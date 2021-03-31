using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("Loading Game scene");
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Debug.Log("Closing application");
        Application.Quit();
    }
}
