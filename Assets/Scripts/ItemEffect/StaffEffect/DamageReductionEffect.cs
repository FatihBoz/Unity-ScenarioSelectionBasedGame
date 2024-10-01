using UnityEngine;

public class DamageReductionEffect : MonoBehaviour, IStaffEffect 
{
    private readonly float baseDamageReductionAmount = 0.1f;
    public void ApplyEffect(int tier)
    {
        StaffAttributes.Instance.SetDamageReduction(tier * baseDamageReductionAmount);
    }
}
