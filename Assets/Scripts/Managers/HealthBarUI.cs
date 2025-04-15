using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float animationDuration = 1f;

    private void OnHealthChanged(float hpRatio) 
    {
        hpBar.DOFillAmount(hpRatio, animationDuration);
    }

    private void OnEnable()
    {
        EventManager<float>.Subscribe(EventKey.HEALTH_UI_CHANGED, OnHealthChanged);
    }

    private void OnDisable()
    {
        EventManager<float>.Unsubscribe(EventKey.HEALTH_UI_CHANGED, OnHealthChanged);
    }
}