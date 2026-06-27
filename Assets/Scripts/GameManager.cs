using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static int finalScore;
    public static float finalTime;

    public int score;
    public int startingLives = 3;
    public int asteroidHitPenalty = 500;
    public float scoreAnimationDuration = 0.35f;

    public TMP_Text scoreText;
    public TMP_Text timerText;
    public GameObject[] lifeIcons;

    float elapsedTime;
    int currentLives;
    int displayedScore;
    Coroutine scoreAnimation;

    void Awake()
    {
        instance=this;
    }

    void Start()
    {
        currentLives = startingLives;
        displayedScore = score;

        UpdateScoreText(displayedScore);
        UpdateTimerText();
        UpdateLivesUI();
    }

    void Update()
    {
        elapsedTime +=
            Time.deltaTime;

        UpdateTimerText();
    }

    public void AddScore(int point)
    {
        score =
            Mathf.Max(
                0,
                score + point
            );

        AnimateScoreText();
    }

    public void ApplyAsteroidHitPenalty()
    {
        AddScore(-asteroidHitPenalty);
    }

    void AnimateScoreText()
    {
        if(scoreAnimation!=null)
            StopCoroutine(scoreAnimation);

        if(scoreAnimationDuration<=0f)
        {
            displayedScore = score;
            UpdateScoreText(displayedScore);
            return;
        }

        scoreAnimation =
            StartCoroutine(
                AnimateScoreRoutine(
                    displayedScore,
                    score
                )
            );
    }

    IEnumerator AnimateScoreRoutine(
        int fromScore,
        int toScore
    )
    {
        float elapsed = 0f;

        while(elapsed<scoreAnimationDuration)
        {
            elapsed += Time.deltaTime;

            float progress =
                Mathf.Clamp01(
                    elapsed / scoreAnimationDuration
                );

            displayedScore =
                Mathf.RoundToInt(
                    Mathf.Lerp(
                        fromScore,
                        toScore,
                        progress
                    )
                );

            UpdateScoreText(displayedScore);

            yield return null;
        }

        displayedScore = toScore;
        UpdateScoreText(displayedScore);
        scoreAnimation = null;
    }

    public bool LoseLife()
    {
        if(currentLives<=0)
            return true;

        currentLives--;
        UpdateLivesUI();

        if(currentLives<=0)
        {
            GameOver();
            return true;
        }

        return false;
    }

    public void GameOver()
    {
        finalScore = score;
        finalTime = elapsedTime;

        SceneManager.LoadScene(
            "EndGame"
        );
    }

    void UpdateScoreText(int value)
    {
        if(scoreText!=null)
        {
            scoreText.text =
                "Score: " + value;
        }
    }

    void UpdateTimerText()
    {
        if(timerText!=null)
        {
            timerText.text =
                "Time: " +
                FormatTime(elapsedTime);
        }
    }

    void UpdateLivesUI()
    {
        if(lifeIcons==null)
            return;

        for(int i=0;i<lifeIcons.Length;i++)
        {
            if(lifeIcons[i]!=null)
            {
                lifeIcons[i]
                .SetActive(i<currentLives);
            }
        }
    }

    public static string FormatTime(
        float time
    )
    {
        int minutes =
            Mathf.FloorToInt(
                time / 60f
            );

        int seconds =
            Mathf.FloorToInt(
                time % 60f
            );

        return
            minutes.ToString("00") +
            ":" +
            seconds.ToString("00");
    }
}
