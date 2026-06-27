using UnityEngine;
using TMPro;

public class EndGameUI
:MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        scoreText.text =
            "Score: " +
            GameManager.finalScore +
            "    Time: " +
            GameManager.FormatTime(
                GameManager.finalTime
            );
    }
}
