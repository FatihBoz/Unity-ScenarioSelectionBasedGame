using UnityEngine;
using UnityEngine.UI;

public class SFXUIController : MonoBehaviour
{
    [SerializeField] private Button sfxButton;
    [SerializeField] private Slider sfxVolumeSlider;

    [Header("Sprite")]
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite sfxOffSprite;

    bool isSFXOn = true;

    // Start is called before the first frame update
    void Start()
    {
        sfxVolumeSlider.value = SoundEffectManager.Instance.GetAudioSourceVolume();
        sfxButton.onClick.AddListener(ToggleSFX);

        sfxVolumeSlider.onValueChanged.AddListener(SetVolume);

        CheckIfAudioSourceMuted();
    }

    void ToggleSFX()
    {
        SoundEffectManager.Instance.MuteAudioSource(isSFXOn);
        isSFXOn = !isSFXOn;
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        sfxButton.image.sprite = isSFXOn ? sfxOnSprite : sfxOffSprite;
    }

    void SetVolume(float volume)
    {
        SoundEffectManager.Instance.SetAudioSourceVolume(volume);
    }

    void CheckIfAudioSourceMuted()
    {
        if (SoundEffectManager.Instance.IsAudioSourceMuted())
        {
            isSFXOn = false;
            UpdateButtonSprite();
        }
        else
        {
            isSFXOn = true;
            UpdateButtonSprite();
        }

    }
}
