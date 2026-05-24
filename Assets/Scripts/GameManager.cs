using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int finalScore;

    public int score;

    public TMP_Text scoreText;

    void Awake()
    {
        instance=this;
    }

    public void AddScore(int point)
    {
        score += point;

        scoreText.text =
            "Score: " + score;
    }

    public void GameOver()
    {
        finalScore = score;

        SceneManager.LoadScene(
            "EndGame"
        );
    }
}