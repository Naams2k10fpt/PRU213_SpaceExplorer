using UnityEngine;

public class ShieldPickup : MonoBehaviour
{
    public float speed = 3f;
    public float shieldDuration = 5f;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();
    }

    public void Configure(
        Sprite shieldSprite,
        float duration
    )
    {
        if(spriteRenderer!=null)
            spriteRenderer.sprite = shieldSprite;

        shieldDuration = duration;
    }

    void Update()
    {
        transform.position +=
            Vector3.down *
            speed *
            Time.deltaTime;

        if(transform.position.y<-6)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(
        Collider2D other
    )
    {
        if(!other.CompareTag("Player"))
            return;

        PlayerController player =
            other
            .GetComponent<PlayerController>();

        if(player!=null)
            player.ActivateShield(shieldDuration);

        Destroy(gameObject);
    }
}
