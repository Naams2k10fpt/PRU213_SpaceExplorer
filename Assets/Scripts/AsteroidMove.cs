using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public float speed;

    Vector2 direction;

    public float rotateSpeed;

    void Start()
    {
        direction =
            new Vector2(-1,-1)
            .normalized;

        speed=
            Random.Range(3f,6f);

        rotateSpeed=
            Random.Range(
                50f,
                200f
            );
    }

    void Update()
    {
        transform.position +=
            (Vector3)
            (
            direction*
            speed*
            Time.deltaTime
            );

        transform.Rotate(
            0,
            0,
            rotateSpeed*
            Time.deltaTime
        );


        if(
            transform.position.y<-6 ||
            transform.position.x<-10
        )
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
        "Player"))
        {
            GameManager.instance
            .GameOver();

            Destroy(
            gameObject);
        }
    }
}   