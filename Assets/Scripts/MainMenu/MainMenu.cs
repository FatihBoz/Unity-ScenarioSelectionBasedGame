using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private string sceneNameToLoad;


    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonPressed);
    }

    void OnStartButtonClicked()
    {
        SceneLoader.Instance.ChangeScene(sceneNameToLoad);
        SoundEffectManager.Instance.PlayButtonClickSF();
    }

    void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
