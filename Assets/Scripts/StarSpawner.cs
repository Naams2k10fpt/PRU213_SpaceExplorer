using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject star;
    public GameObject shield;

    public Sprite bronzeShieldSprite;
    public Sprite silverShieldSprite;
    public Sprite goldShieldSprite;

    public float shieldSpawnChance = 0.15f;
    public float bronzeChance = 0.6f;
    public float silverChance = 0.3f;
    public int maxStarsWithoutShield = 5;

    public float bronzeDuration = 5f;
    public float silverDuration = 10f;
    public float goldDuration = 15f;

    public float spawnRate=4f;

    int starsSinceLastShield;

    void Start()
    {
        InvokeRepeating(
            "Spawn",
            2f,
            spawnRate
        );
    }


    void Spawn()
    {
        Vector2 pos =
            new Vector2(
                Random.Range(-8f,8f),
                6f
            );

        bool shieldByChance =
            Random.value<shieldSpawnChance;

        bool shieldGuaranteed =
            starsSinceLastShield>=
            maxStarsWithoutShield;

        if(
            shield!=null &&
            (
                shieldByChance ||
                shieldGuaranteed
            )
        )
        {
            SpawnShield(pos);
            starsSinceLastShield = 0;
            return;
        }

        Instantiate(
            star,
            pos,
            Quaternion.identity
        );

        starsSinceLastShield++;
    }

    void SpawnShield(Vector2 pos)
    {
        GameObject newShield =
            Instantiate(
                shield,
                pos,
                Quaternion.identity
            );

        float tierRoll = Random.value;
        Sprite selectedSprite;
        float selectedDuration;

        if(tierRoll<bronzeChance)
        {
            selectedSprite = bronzeShieldSprite;
            selectedDuration = bronzeDuration;
        }
        else if(
            tierRoll<
            bronzeChance+silverChance
        )
        {
            selectedSprite = silverShieldSprite;
            selectedDuration = silverDuration;
        }
        else
        {
            selectedSprite = goldShieldSprite;
            selectedDuration = goldDuration;
        }

        ShieldPickup pickup =
            newShield
            .GetComponent<ShieldPickup>();

        if(pickup!=null)
        {
            pickup.Configure(
                selectedSprite,
                selectedDuration
            );
        }
    }
}
