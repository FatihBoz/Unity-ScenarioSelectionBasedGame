using UnityEngine;
using UnityEngine.UI;

public class MusicUIController : MonoBehaviour
{
    [SerializeField] private Button musicButton;

    [Header("Sprite")]
    [SerializeField] private Sprite musicOnSprite;
    [SerializeField] private Sprite musicOffSprite;

    bool isSFXOn = true;

    void Start()
    {
        musicButton.onClick.AddListener(ToggleSFX);
    }

    void ToggleSFX()
    {
        isSFXOn = !isSFXOn;
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        musicButton.image.sprite = isSFXOn ? musicOnSprite : musicOffSprite;
    }
}
