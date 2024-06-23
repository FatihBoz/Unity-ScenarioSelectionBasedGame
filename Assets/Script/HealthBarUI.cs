using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private float animationDuration = 1f;


    private void OnEnable()
    {
        HealthReward.OnHealthDecreased += HealthBarUI_OnHealthDecreased;
    }

    private void HealthBarUI_OnHealthDecreased()
    {
        hpBar.DOFillAmount(PlayerAttributes.Instance.GetCurrentHp()/PlayerAttributes.Instance.GetMaxHp(),animationDuration);
    }

    private void OnDisable()
    {
        HealthReward.OnHealthDecreased -= HealthBarUI_OnHealthDecreased;
    }
}
