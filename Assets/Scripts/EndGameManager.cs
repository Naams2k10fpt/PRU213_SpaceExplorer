using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameManager
:MonoBehaviour
{
    public void PlayAgain()
    {
        SceneManager.LoadScene(
            "Gameplay"
        );
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(
            "MainMenu"
        );
    }

    public void Quit()
    {
        Application.Quit();
    }
}