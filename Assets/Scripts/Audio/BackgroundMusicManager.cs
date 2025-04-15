using System;
using UnityEngine;

public class BackgroundMusicManager : MonoBehaviour
{
    public static BackgroundMusicManager Instance { get; private set; }

    [SerializeField] private AudioSource a_source;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

        Destroy(this.gameObject);
    }

    public void SetAudioSourceVolume(float volume)
    {
        a_source.volume = volume;
    }

    public float GetAudioSourceVolume()
    {
        return a_source.volume;
    }

    public void MuteAudioSource(bool mute)
    {
        a_source.mute = mute;
    }

    public bool IsAudioSourceMuted()
    {
        return a_source.mute;
    }

}
