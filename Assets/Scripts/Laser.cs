using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;

    void Update()
    {
        transform.position +=
            Vector3.up *
            speed *
            Time.deltaTime;

        if(transform.position.y>7)
        {
            Destroy(gameObject);
        }
    }


    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if(
            other.CompareTag(
                "Asteroid"
            )
        )
        {
            GameManager.instance
            .AddScore(10);

            Destroy(
                other.gameObject
            );

            Destroy(
                gameObject
            );
        }
    }
}