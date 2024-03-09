using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance;
    public AudioSource audioSource;
    public AudioClip musicClip;

    // only one instance of MusicPlayer exists
    private void Awake()
    {
        // If an instance already exists, destroy this one
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (audioSource.clip == null)
        {
            audioSource.clip = musicClip;
        }

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }
}
