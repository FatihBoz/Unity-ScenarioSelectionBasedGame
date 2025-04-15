using UnityEngine;

public class HealthReward : MonoBehaviour,IReward
{
    [SerializeField] private float baseDamage = 20f;
    [SerializeField] private float damageScale = 2f;
    [SerializeField] private HealthRewardType type;

    public void GetReward()
    {
        if (type == HealthRewardType.Increase)
        {
            EventManager<float>.TriggerEvent(EventKey.HEALTH_INCREASED, CalculateDamage());
            FloatingTextManager.Instance.InstantiateFloatingText(FloatingTextType.Health_Increased);
            return;
        }

        if (CanNullified(StaffAttributes.Instance.NullifyChance)) // if the damage is nullified
        {
            FloatingTextManager.Instance.InstantiateFloatingText(FloatingTextType.Damage_Nullified);
            return;
        }


        EventManager<float>.TriggerEvent(EventKey.HEALTH_DECREASED, CalculateReducedDamage()); //else take the damage
        FloatingTextManager.Instance.InstantiateFloatingText(FloatingTextType.Health_Decreased);
    }

    private float CalculateDamage() => baseDamage + (damageScale * PlayerAttributes.Instance.Level); //Without considering damage reduction
    private float CalculateReducedDamage() => CalculateDamage() * (1 - PlayerAttributes.Instance.DamageReduction); //with damage reduction

    private bool CanNullified(float chance)
    {
        float r = Random.Range(0f, 1f);

        return r < chance;
    }

}

public enum HealthRewardType
{
    Decrease,
    Increase
}