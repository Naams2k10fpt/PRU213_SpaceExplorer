using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.InputSystem;

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
    public TMP_Text pauseText;
    public GameObject pausePanel;
    public GameObject[] lifeIcons;

    float elapsedTime;
    int currentLives;
    int displayedScore;
    int highestScore;
    Coroutine scoreAnimation;
    Coroutine resumeCountdown;
    bool isPaused;

    void Awake()
    {
        instance=this;
    }

    void Start()
    {
        currentLives = startingLives;
        displayedScore = score;
        highestScore = score;

        UpdateScoreText(displayedScore);
        UpdateTimerText();
        UpdateLivesUI();

        if(pauseText!=null)
            pauseText.gameObject.SetActive(false);

        if(pausePanel!=null)
            pausePanel.SetActive(false);
    }

    void Update()
    {
        if(
            Keyboard.current!=null &&
            Keyboard.current.escapeKey.wasPressedThisFrame
        )
        {
            TogglePause();
        }

        if(isPaused)
            return;

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

        highestScore =
            Mathf.Max(
                highestScore,
                score
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
        Time.timeScale = 1f;

        finalScore = highestScore;
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

    void TogglePause()
    {
        if(!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;

            if(pauseText!=null)
                pauseText.gameObject.SetActive(false);

            if(pausePanel!=null)
                pausePanel.SetActive(true);
            else
                SetPauseText("PAUSED");

            return;
        }

        if(resumeCountdown==null)
        {
            if(pausePanel!=null)
                pausePanel.SetActive(false);

            resumeCountdown =
                StartCoroutine(
                    ResumeCountdownRoutine()
                );
        }
    }

    IEnumerator ResumeCountdownRoutine()
    {
        for(int i=3;i>0;i--)
        {
            SetPauseText(i.ToString());

            yield return
                new WaitForSecondsRealtime(1f);
        }

        isPaused = false;
        Time.timeScale = 1f;
        resumeCountdown = null;

        if(pauseText!=null)
            pauseText.gameObject.SetActive(false);
    }

    void SetPauseText(string text)
    {
        if(pauseText==null)
            return;

        pauseText.gameObject.SetActive(true);
        pauseText.text = text;
    }

    void OnDestroy()
    {
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            "Gameplay"
        );
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            "MainMenu"
        );
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
