using UnityEngine;

public class SoundEffectManager : MonoBehaviour //SERIALIZABLE BÝR SOUND CLASSI KULLANJP TEK BÝR AUDIO MANAGER ÝLE HALLET
{
    public static SoundEffectManager Instance {  get; private set; }
    [SerializeField] private AudioSource a_source;

    [SerializeField] private AudioClip buttonClickSF;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }

    }

    public void PlaySoundEffect(AudioClip clip)
    {
        a_source.PlayOneShot(clip);
    }


    public void PlayButtonClickSF() //General clips (used by several classes) are called in this class while others not.
    {
        PlaySoundEffect(buttonClickSF);
    }

    public void SetAudioSourceVolume(float value)
    {

        if (a_source == null)
        {
            Debug.LogError("AudioSource is not assigned.");
            return;
        }
        a_source.volume = value;
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


