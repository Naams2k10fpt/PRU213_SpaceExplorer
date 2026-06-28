using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 6f;
    public GameObject laserPrefab;
    public Transform firePoint;
    public TMP_Text heatText;
    public GameObject shieldVisual;
    public AudioClip overheatBeepSound;
    public AudioClip overheatSound;

    public float maxHeat = 100f;
    public float heatPerShot = 5f;
    public float normalCoolRate = 15f;
    public float overheatCoolRate = 25f;
    public float resumeHeat = 0f;
    public float invulnerabilityDuration = 1.5f;
    public float blinkInterval = 0.15f;

    private Camera cam;
    private SpriteRenderer sr;
    private AudioSource audioSource;
    private float currentHeat;
    private bool isOverheated;
    private bool isInvulnerable;
    private bool hasShield;
    private bool useWasdControls;
    private Coroutine shieldCoroutine;



    void Start()
    {
        cam = Camera.main;
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        if(audioSource==null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;

        useWasdControls =
            PlayerPrefs.GetString(
                MenuManager.ControlModeKey,
                MenuManager.ControlModeArrows
            )==MenuManager.ControlModeWasd;

        if(shieldVisual!=null)
            shieldVisual.SetActive(false);

        UpdateHeatText();
    }

    void Update()
    {
        Vector2 move = Vector2.zero;

        if(useWasdControls)
        {
            if(Keyboard.current.aKey.isPressed)
                move.x = -1;

            if(Keyboard.current.dKey.isPressed)
                move.x = 1;

            if(Keyboard.current.wKey.isPressed)
                move.y = 1;

            if(Keyboard.current.sKey.isPressed)
                move.y = -1;
        }
        else
        {
            if(Keyboard.current.leftArrowKey.isPressed)
                move.x = -1;

            if(Keyboard.current.rightArrowKey.isPressed)
                move.x = 1;

            if(Keyboard.current.upArrowKey.isPressed)
                move.y = 1;

            if(Keyboard.current.downArrowKey.isPressed)
                move.y = -1;
        }

        transform.position +=
            (Vector3)(move.normalized * speed * Time.deltaTime);

        LimitScreen();

        CoolWeapon();

        if (Keyboard.current.spaceKey.wasPressedThisFrame || 
            Mouse.current.leftButton.wasPressedThisFrame)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(isOverheated)
            return;

        Instantiate(
            laserPrefab,
            firePoint.position,
            Quaternion.identity
        );

        float previousHeat =
            currentHeat;

        currentHeat =
            Mathf.Min(
                maxHeat,
                currentHeat + heatPerShot
            );

        if(
            previousHeat<maxHeat*0.9f &&
            currentHeat>=maxHeat*0.9f &&
            currentHeat<maxHeat
        )
        {
            PlaySound(overheatBeepSound);
        }

        if(currentHeat>=maxHeat)
        {
            isOverheated = true;
            PlaySound(overheatSound);
        }

        UpdateHeatText();
    }

    void CoolWeapon()
    {
        float currentCoolRate =
            isOverheated
            ? overheatCoolRate
            : normalCoolRate;

        currentHeat =
            Mathf.Max(
                0f,
                currentHeat - currentCoolRate * Time.deltaTime
            );

        if(isOverheated && currentHeat<=resumeHeat)
            isOverheated = false;

        UpdateHeatText();
    }

    void UpdateHeatText()
    {
        if(heatText==null)
            return;

        float heatNormalized =
            maxHeat>0f
            ? currentHeat / maxHeat
            : 0f;

        int heatPercent =
            Mathf.CeilToInt(
                heatNormalized * 100f
            );

        if(isOverheated)
        {
            heatText.text =
                "OVERHEATED! " + heatPercent + "%";

            heatText.color = Color.red;
        }
        else
        {
            heatText.text =
                "Heat: " + heatPercent + "%";

            heatText.color =
                currentHeat>=maxHeat*0.7f
                ? Color.yellow
                : Color.white;
        }
    }

    void PlaySound(AudioClip clip)
    {
        if(clip==null)
            return;

        audioSource.PlayOneShot(clip);
    }

    public void TryTakeHit()
    {
        if(isInvulnerable)
            return;

        if(hasShield)
        {
            ConsumeShield();
            return;
        }

        isInvulnerable = true;

        GameManager.instance
        .ApplyAsteroidHitPenalty();

        bool gameOver =
            GameManager.instance
            .LoseLife();

        if(!gameOver)
        {
            StartCoroutine(
                InvulnerabilityRoutine()
            );
        }
    }

    public void ActivateShield(float duration)
    {
        hasShield = true;

        if(shieldVisual!=null)
            shieldVisual.SetActive(true);

        if(shieldCoroutine!=null)
            StopCoroutine(shieldCoroutine);

        shieldCoroutine =
            StartCoroutine(
                ShieldDurationRoutine(duration)
            );
    }

    IEnumerator ShieldDurationRoutine(
        float duration
    )
    {
        yield return
            new WaitForSeconds(duration);

        hasShield = false;
        shieldCoroutine = null;

        if(shieldVisual!=null)
            shieldVisual.SetActive(false);
    }

    void ConsumeShield()
    {
        hasShield = false;

        if(shieldCoroutine!=null)
        {
            StopCoroutine(shieldCoroutine);
            shieldCoroutine = null;
        }

        if(shieldVisual!=null)
            shieldVisual.SetActive(false);
    }

    IEnumerator InvulnerabilityRoutine()
    {
        float elapsed = 0f;

        while(elapsed<invulnerabilityDuration)
        {
            sr.enabled = !sr.enabled;

            yield return
                new WaitForSeconds(
                    blinkInterval
                );

            elapsed += blinkInterval;
        }

        sr.enabled = true;
        isInvulnerable = false;
    }

    void LimitScreen()
    {
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        // nửa kích thước player
        float playerHalfWidth =
            sr.bounds.extents.x;

        float playerHalfHeight =
            sr.bounds.extents.y;

        float x = Mathf.Clamp(
            transform.position.x,
            -halfWidth + playerHalfWidth,
            halfWidth - playerHalfWidth
        );

        float y = Mathf.Clamp(
            transform.position.y,
            -halfHeight + playerHalfHeight,
            halfHeight - playerHalfHeight
        );

        transform.position =
            new Vector3(x, y, 0);
    }
}
