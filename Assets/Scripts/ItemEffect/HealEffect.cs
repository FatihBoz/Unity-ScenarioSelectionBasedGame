using UnityEngine;

public class HealEffect : MonoBehaviour, IStaffEffect
{
    [SerializeField] private int choiceCountToHeal = 7;
    [SerializeField] private float baseHealAmount = 0.1f;
    private int choiceCounter = 0;

    public void ApplyEffect(int tier)
    {
        if (EffectCanBeApplied())
        {
            EventManager<float>.TriggerEvent(EventKey.HEALTH_INCREASED, baseHealAmount * tier);
        }
    }


    private bool EffectCanBeApplied()
    {
        return choiceCounter >= choiceCountToHeal;
    }


    private void HealEffect_OnScenarioSelected(ScenarioSO s)
    {
        choiceCounter++;
    }

    private void OnEnable()
    {
        EventManager<ScenarioSO>.Subscribe(EventKey.SELECT_SCENARIO, HealEffect_OnScenarioSelected);
    }

    private void OnDisable()
    {
        EventManager<ScenarioSO>.Unsubscribe(EventKey.SELECT_SCENARIO, HealEffect_OnScenarioSelected);
    }
}
