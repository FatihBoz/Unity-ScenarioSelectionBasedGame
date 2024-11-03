using UnityEngine;
using UnityEngine.UI;

public class SFXUIController : MonoBehaviour
{
    [SerializeField] private Button sfxButton;

    [Header("Sprite")]
    [SerializeField] private Sprite sfxOnSprite;
    [SerializeField] private Sprite sfxOffSprite;

    bool isSFXOn = true;

    // Start is called before the first frame update
    void Start()
    {
        sfxButton.onClick.AddListener(ToggleSFX);
    }

    void ToggleSFX()
    {
        isSFXOn = !isSFXOn; 
        UpdateButtonSprite();
    }

    void UpdateButtonSprite()
    {
        sfxButton.image.sprite = isSFXOn ? sfxOnSprite : sfxOffSprite;
    }

}
