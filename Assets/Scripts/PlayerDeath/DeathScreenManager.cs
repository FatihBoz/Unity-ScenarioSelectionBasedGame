using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class DeathScreenManager : MonoBehaviour
{


    [Header("Components")]
    [SerializeField] private Image backgroundImage;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button replayButton;

    [Header("Fade Values")]
    [SerializeField] private float fadeDuration = 1f;
    [SerializeField] private float backgroundFadeAmount = .5f;



    private void Start()
    {
        backgroundImage.DOFade(backgroundFadeAmount, fadeDuration).OnComplete(() => titleText.DOFade(1, fadeDuration).OnComplete(ButtonsFade));
    }

    void ButtonsFade()
    {
        homeButton.image.DOFade(1, fadeDuration);
        replayButton.image.DOFade(1, fadeDuration);
    }

}
