using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip singleClip;
    public AudioClip[] playlist;
    public bool loopSingleClip = true;
    public float volume = 0.45f;

    AudioSource audioSource;
    int lastIndex = -1;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        if(audioSource==null)
            audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.volume = volume;
    }

    void Start()
    {
        if(playlist!=null && playlist.Length>0)
            PlayNextTrack();
        else if(singleClip!=null)
            PlaySingleClip();
    }

    void Update()
    {
        if(playlist==null || playlist.Length==0)
            return;

        if(!audioSource.isPlaying)
            PlayNextTrack();
    }

    void PlaySingleClip()
    {
        audioSource.clip = singleClip;
        audioSource.loop = loopSingleClip;
        audioSource.Play();
    }

    void PlayNextTrack()
    {
        int index = 0;

        if(playlist.Length>1)
        {
            do
            {
                index = Random.Range(0, playlist.Length);
            }
            while(index==lastIndex);
        }

        lastIndex = index;
        audioSource.clip = playlist[index];
        audioSource.loop = false;
        audioSource.Play();
    }
}
