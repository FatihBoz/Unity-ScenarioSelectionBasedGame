using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button quitButton;


    private void Start()
    {
        startButton.onClick.AddListener(OnStartButtonClicked);
        quitButton.onClick.AddListener(OnQuitButtonPressed);
    }

    void OnStartButtonClicked()
    {
        SceneLoader.Instance.ChangeScene("GameplayScene");
    }

    void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
