using UnityEngine;

public class AsteroidSpawner : MonoBehaviour
{
    public GameObject asteroid;

    public float spawnRate = 1.5f;

    void Start()
    {
        InvokeRepeating(
            "Spawn",
            1f,
            spawnRate
        );
    }

    void Spawn()
    {
        Vector2 spawnPos;

        int side =
            Random.Range(0,2);


        // Spawn cạnh trên
        if(side==0)
        {
            spawnPos =
                new Vector2(
                    Random.Range(-8f,8f),
                    6f
                );
        }

        // Spawn nửa trên cạnh phải
        else
        {
            spawnPos =
                new Vector2(
                    10f,
                    Random.Range(1f,5f)
                );
        }


        Instantiate(
            asteroid,
            spawnPos,
            Quaternion.identity
        );
    }
}