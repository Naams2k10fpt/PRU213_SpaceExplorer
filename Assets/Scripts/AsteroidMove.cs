using UnityEngine;

public class AsteroidMove : MonoBehaviour
{
    public float speed;

    Vector2 direction;
    bool hasCustomDirection;

    public float rotateSpeed;

    void Start()
    {
        if(!hasCustomDirection)
        {
            direction =
                new Vector2(
                    Random.Range(-1f,1f),
                    Random.Range(-1f,-0.3f)
                )
                .normalized;
        }

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
            transform.position.y>7 ||
            transform.position.x<-10 ||
            transform.position.x>10
        )
        {
            Destroy(gameObject);
        }
    }


    public void SetDirection(
        Vector2 newDirection
    )
    {
        direction =
            newDirection
            .normalized;

        hasCustomDirection =
            true;
    }


    private void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if(
        other.CompareTag(
        "Player"))
        {
            PlayerController player =
                other
                .GetComponent<PlayerController>();

            if(player!=null)
            {
                player.TryTakeHit();
            }

            Destroy(
            gameObject);
        }
    }
}
