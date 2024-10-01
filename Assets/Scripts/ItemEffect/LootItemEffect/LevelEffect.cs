using UnityEngine;

public class LevelEffect : LootItemEffect
{
    public override void ApplyItemEffect(ItemSO item)
    {
        PlayerAttributes.Instance.LevelUp(item.Tier);
    }
}
