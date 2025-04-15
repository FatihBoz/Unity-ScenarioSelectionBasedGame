using DG.Tweening;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    [Header("** Movement **")]
    [SerializeField] private float timeToDisappear;
    [SerializeField] private float distanceToTravelOnYAxis;
    private RectTransform rectTransform;

    [Header("** Scale **")]
    [SerializeField] private float timeToFullyScaled;
    [SerializeField] private float finalScale = 1f;

    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + distanceToTravelOnYAxis, timeToDisappear)
            .OnComplete(() => Destroy(gameObject));

        rectTransform.DOScale(finalScale, timeToFullyScaled);
    }

    public void SetText(string text, Color color)
    {
        this.text.text = text;
        this.text.color = color;
    }



}
