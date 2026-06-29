using UnityEngine;
using TMPro;

public class EndGameUI
:MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        scoreText.enableWordWrapping = false;
        scoreText.enableAutoSizing = true;
        scoreText.fontSizeMin = 24f;
        scoreText.fontSizeMax = 36f;
        scoreText.overflowMode = TextOverflowModes.Overflow;

        scoreText.text =
            "Best Score: " +
            GameManager.finalScore +
            "    Time: " +
            GameManager.FormatTime(
                GameManager.finalTime
            );
    }
}
