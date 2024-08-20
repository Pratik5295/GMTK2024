using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance = null;

    [Header("Background Audio Sources")]
    [SerializeField] private AudioSource backgroundSource;

    [Header("Foreground Audio Sources")]
    [SerializeField] private List<AudioSource> foregroundSources;

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

    #region Background Audio Sources
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
    #endregion


    #region Foreground Audio Sources

    public GameObject PlayForeground(AudioClip clip, float volume = 1f)
    {
        AudioSource availableSource = foregroundSources.Find(source => !source.isPlaying);
        if (availableSource != null)
        {
            availableSource.clip = clip;
            availableSource.volume = volume;
            availableSource.Play();
            return availableSource.gameObject;
        }
        else
        {
            Debug.LogWarning("No available audio source to play the clip.");
            return null;
        }
    }

    #endregion
}
