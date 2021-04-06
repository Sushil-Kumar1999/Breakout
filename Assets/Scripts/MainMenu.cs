using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void NewGame()
    {
        Debug.Log("Beginning new game");
        GameManager.loadFromSavedGame = false;
        SceneManager.LoadScene(Scenes.GameScene);
    }

    public void ExitGame()
    {
        Debug.Log("Exiting the game");
        Application.Quit();
    }
}
