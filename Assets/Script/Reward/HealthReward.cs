using System;
using UnityEngine;

public class HealthReward : MonoBehaviour,IReward
{
    private readonly float baseDamage = 20f;
    private readonly float damageScale = 2f;
    public void GetReward()
    {
        EventManager<float>.TriggerEvent(EventKey.HEALTH_DECREASED, CalculateReducedDamage());
    }

    private float CalculateDamage() => baseDamage + (damageScale * PlayerAttributes.Instance.Level); //Without considering damage reduction
    private float CalculateReducedDamage() => CalculateDamage() * (1 - ItemAttributes.Instance.GetDamageReduction()); //with damage reduction

}
