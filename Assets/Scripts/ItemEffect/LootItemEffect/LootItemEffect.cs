using UnityEngine;

public abstract class LootItemEffect : MonoBehaviour
{
    [SerializeField] private LootItemEffectType lootItemType;

    public LootItemEffectType ItemEffectType { get => lootItemType; }
    public abstract void ApplyItemEffect(ItemSO item);


}
