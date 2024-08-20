using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(3)]
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioClip backgroundMusic;

    private void Start()
    {
        AudioManager.Instance.PlayBackground(backgroundMusic);
    }
}
