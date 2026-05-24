using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject instructionPanel;

    public void PlayGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void ShowInstructions()
    {
        instructionPanel.SetActive(true);
    }

    public void CloseInstructions()
    {
        instructionPanel.SetActive(false);
    }
}