using UnityEngine;

public class Star : MonoBehaviour
{
    public float speed = 3f;
    public AudioClip collectSound;

    static AudioSource audioSource;

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
            PlaySound();

            GameManager.instance
                .AddScore(100);

            Destroy(gameObject);
        }
    }

    void PlaySound()
    {
        if(collectSound==null)
            return;

        CreateAudioSource();
        audioSource.PlayOneShot(collectSound);
    }

    void CreateAudioSource()
    {
        if(audioSource!=null)
            return;

        GameObject soundObject =
            new GameObject("StarSoundAudio");

        DontDestroyOnLoad(soundObject);

        audioSource =
            soundObject
            .AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }
}
