using UnityEngine;

public class HealthReward : MonoBehaviour,IReward
{
    [SerializeField] private float baseDamage = 20f;
    [SerializeField] private float damageScale = 2f;
    
    public void GetReward()
    {
        if (CanNullified(StaffAttributes.Instance.NullifyChance)) // if the damage is nullified
        {
            return;
            //todo:There will be an indicator shows that the damage was nullified
        }


        EventManager<float>.TriggerEvent(EventKey.HEALTH_DECREASED, CalculateReducedDamage()); //else take the damage
    }

    private float CalculateDamage() => baseDamage + (damageScale * PlayerAttributes.Instance.Level); //Without considering damage reduction
    private float CalculateReducedDamage() => CalculateDamage() * (1 - StaffAttributes.Instance.DamageReduction); //with damage reduction

    private bool CanNullified(float chance)
    {
        float r = Random.Range(0f, 1f);

        return r < chance;
    }

}
