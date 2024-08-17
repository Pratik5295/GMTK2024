using UnityEngine;

[DefaultExecutionOrder(1)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [Header("Background Audio Sources")]
    [SerializeField] private AudioSource backgroundSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayBackground(AudioClip clip,float volume = 1f)
    {
        backgroundSource.clip = clip;
        backgroundSource.loop = true;
        backgroundSource.Play();
    }

    public void StopBackground()
    {
        if (backgroundSource.isPlaying)
        {
            backgroundSource.Stop();
        }
    }
}
