using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float animationDuration = 1f;

    //todo:given parameter is not being used.
    private void HealthBarUI_OnHealthDecreased(float hp) 
    {
        hpBar.DOFillAmount(PlayerAttributes.Instance.CurrentHp / PlayerAttributes.Instance.MaxHp, animationDuration);
    }

    private void OnEnable()
    {
        EventManager<float>.Subscribe(EventKey.HEALTH_DECREASED, HealthBarUI_OnHealthDecreased);
    }

    private void OnDisable()
    {
        EventManager<float>.Unsubscribe(EventKey.HEALTH_DECREASED, HealthBarUI_OnHealthDecreased);
    }
}