using UnityEngine;

public class DamageReductionEffect : MonoBehaviour, IItemEffect
{
    private readonly float baseDamageReductionAmount = 0.1f;
    public void ApplyEffect(int tier)
    {
        ItemAttributes.Instance.SetDamageReduction(tier * baseDamageReductionAmount);
    }
}
