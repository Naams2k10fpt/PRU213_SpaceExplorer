using UnityEngine;
using TMPro;

public class EndGameUI
:MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        scoreText.text =
            "Final Score : "
            +GameManager.finalScore;
    }
}