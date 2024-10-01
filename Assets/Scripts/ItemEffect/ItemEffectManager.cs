using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    private void ItemEffectManager_OnItemEquipped(ItemSO item)
    {
        IStaffEffect itemEffect = item.ItemEffectPrefab.GetComponent<IStaffEffect>();
        itemEffect?.ApplyEffect(item.Tier);
    }

    private void LootItemEffectManager_OnLootItemUsed(ItemSO item)
    {
        if(item.ItemEffectPrefab.TryGetComponent<LootItemEffect>(out var itemEffect))
        {
            itemEffect.ApplyItemEffect(item);
        }
    }

    private void OnEnable()
    {
        EventManager<ItemSO>.Subscribe(EventKey.STAFF_EQUIPPED, ItemEffectManager_OnItemEquipped);
        EventManager<ItemSO>.Subscribe(EventKey.LootItem_Used, LootItemEffectManager_OnLootItemUsed);
    }

    private void OnDisable()
    {
        EventManager<ItemSO>.Unsubscribe(EventKey.STAFF_EQUIPPED, ItemEffectManager_OnItemEquipped);
        EventManager<ItemSO>.Unsubscribe(EventKey.LootItem_Used, LootItemEffectManager_OnLootItemUsed);
    }


}
