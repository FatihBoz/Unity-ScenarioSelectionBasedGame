using UnityEngine;

public class DamageReducingEffect : LootItemEffect
{
    private readonly float baseDamageReductionValue = 2f;
    public override void ApplyItemEffect(ItemSO item)
    {
        PlayerAttributes.Instance.IncreaseDamageReduction(baseDamageReductionValue * item.Tier);
    }
}
