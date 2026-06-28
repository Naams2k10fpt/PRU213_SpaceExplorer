using UnityEngine;

public class Laser : MonoBehaviour
{
    public float speed = 10f;
    public AudioClip[] asteroidDestroySounds;

    static AudioSource audioSource;

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
            PlayRandomAsteroidSound();

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

    void PlayRandomAsteroidSound()
    {
        if(
            asteroidDestroySounds==null ||
            asteroidDestroySounds.Length==0
        )
            return;

        CreateAudioSource();

        AudioClip clip =
            asteroidDestroySounds[
                Random.Range(
                    0,
                    asteroidDestroySounds.Length
                )
            ];

        if(clip!=null)
            audioSource.PlayOneShot(clip);
    }

    void CreateAudioSource()
    {
        if(audioSource!=null)
            return;

        GameObject soundObject =
            new GameObject("AsteroidSoundAudio");

        DontDestroyOnLoad(soundObject);

        audioSource =
            soundObject
            .AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }
}
