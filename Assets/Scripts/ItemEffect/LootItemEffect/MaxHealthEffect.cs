using UnityEngine;

public class MaxHealthEffect : LootItemEffect
{
    [SerializeField] float baseMaxHealthBoosterValue = 3;
    public override void ApplyItemEffect(ItemSO item)
    {
        PlayerAttributes.Instance.IncreaseMaxHealth(baseMaxHealthBoosterValue * item.Tier);
        FloatingTextManager.Instance.InstantiateFloatingText(FloatingTextType.Max_Health_Increased);
    }
}
