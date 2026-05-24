using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        // Rơi thẳng xuống
        transform.position +=
            Vector3.down *
            speed *
            Time.deltaTime;

        // Ra khỏi màn hình thì xóa
        if(transform.position.y < -6)
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
                "Player"
            )
        )
        {
            GameManager.instance
                .AddScore(100);

            Destroy(gameObject);
        }
    }
}