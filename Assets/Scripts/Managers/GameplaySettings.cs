using UnityEngine;
using UnityEngine.UI;

public class GameplaySettings : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;

    [Header("Buttons")]
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button panelCloseButton;



    private void Start()
    {
        settingsButton.onClick.AddListener(OpenSettingsPanel);
        panelCloseButton.onClick.AddListener(CloseSettingsPanel);
    }

    private void OpenSettingsPanel()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        settingsPanel.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    private void CloseSettingsPanel()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        settingsPanel.SetActive(false);
        Time.timeScale = 1f; // Resume the game
    }

}
