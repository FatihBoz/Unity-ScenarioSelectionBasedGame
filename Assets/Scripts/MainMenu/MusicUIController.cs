using UnityEngine;
using UnityEngine.UI;

public class MusicUIController : MonoBehaviour
{
    [SerializeField] private Button musicButton;

    [SerializeField] private Slider musicVolumeSlider;

    [Header("Sprite")]
    [SerializeField] private Sprite musicOnSprite;

    [SerializeField] private Sprite musicOffSprite;

    bool isMusicOn = true;

    void Start()
    {
        musicVolumeSlider.value = BackgroundMusicManager.Instance.GetAudioSourceVolume();

        musicButton.onClick.AddListener(ToggleSFX);

        musicVolumeSlider.onValueChanged.AddListener(SetVolume);

        CheckIfAudioSourceMuted();
    }

    void ToggleSFX()
    {
        BackgroundMusicManager.Instance.MuteAudioSource(isMusicOn);
        isMusicOn = !isMusicOn;
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        musicButton.image.sprite = isMusicOn ? musicOnSprite : musicOffSprite;
    }

    void SetVolume(float volume)
    {
        BackgroundMusicManager.Instance.SetAudioSourceVolume(volume);
    }

    void CheckIfAudioSourceMuted()
    {
        if(BackgroundMusicManager.Instance.IsAudioSourceMuted())
        {
            isMusicOn = false;
            UpdateButtonSprite();
        }
        else
        {
            isMusicOn = true;
            UpdateButtonSprite();
        }
    }
}
