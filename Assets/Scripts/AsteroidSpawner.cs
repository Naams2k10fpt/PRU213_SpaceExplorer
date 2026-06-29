using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;

    public float spawnRate = 1.5f;
    public float difficultyInterval = 10f;
    public float spawnRateDecrease = 0.2f;
    public float minSpawnRate = 0.5f;

    void Start()
    {
        InvokeRepeating(
            "Spawn",
            1f,
            spawnRate
        );

        InvokeRepeating(
            "IncreaseDifficulty",
            difficultyInterval,
            difficultyInterval
        );
    }

    void Spawn()
    {
        Vector2 spawnPos =
            new Vector2(
                Random.Range(-8f,8f),
                6f
            );

        Vector2 direction =
            new Vector2(
                Random.Range(-0.8f,0.8f),
                Random.Range(-1f,-0.4f)
            );

        GameObject newAsteroid =
            Instantiate(
            asteroid,
            spawnPos,
            Quaternion.identity
        );

        AsteroidMove asteroidMove =
            newAsteroid
            .GetComponent<AsteroidMove>();

        if(asteroidMove!=null)
        {
            asteroidMove
            .SetDirection(direction);
        }
    }

    void IncreaseDifficulty()
    {
        spawnRate =
            Mathf.Max(
                minSpawnRate,
                spawnRate - spawnRateDecrease
            );

        CancelInvoke("Spawn");

        InvokeRepeating(
            "Spawn",
            spawnRate,
            spawnRate
        );
    }
}
