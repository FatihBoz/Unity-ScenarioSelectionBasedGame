using UnityEngine;

public class HealerEffect : LootItemEffect
{
    [SerializeField] private float baseHealValue = 5f;

    public override void ApplyItemEffect(ItemSO item)
    {
        EventManager<float>.TriggerEvent(EventKey.HEALTH_INCREASED, (baseHealValue * item.Tier));
    }
}

