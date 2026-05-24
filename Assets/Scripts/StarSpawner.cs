using UnityEngine;

public class StarSpawner : MonoBehaviour
{
    public GameObject star;

    public float spawnRate=4f;

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

    Instantiate(
        star,
        pos,
        Quaternion.identity
    );
}
}