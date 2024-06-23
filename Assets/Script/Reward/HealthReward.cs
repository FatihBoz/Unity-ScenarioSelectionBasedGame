using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReward : MonoBehaviour,IReward
{
    public static Action OnHealthDecreased;

    private readonly float baseDamage = 20f;
    private readonly float damageScale = 4f;
    public void GetReward()
    {
        PlayerAttributes.Instance.TakeDamage(CalculateReducedDamage());
        OnHealthDecreased?.Invoke();
    }

    private float CalculateDamage() => baseDamage + (damageScale * PlayerAttributes.Instance.GetLevel());
    private float CalculateReducedDamage() => CalculateDamage() * (1 - ItemAttributes.Instance.GetDamageReduction());

}
