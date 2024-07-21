using UnityEngine;

public class ItemEffectManager : MonoBehaviour
{
    private void ItemEffectManager_OnItemEquipped(ItemSO item)
    {
        IItemEffect itemEffect = item.GetItemEffectPrefab().GetComponent<IItemEffect>();
        itemEffect?.ApplyEffect(item.GetTier());
    }

    private void OnEnable()
    {
        EventManager<ItemSO>.Subscribe(EventKey.ITEM_EQUIPPED, ItemEffectManager_OnItemEquipped);
    }

    private void OnDisable()
    {
        EventManager<ItemSO>.Unsubscribe(EventKey.ITEM_EQUIPPED, ItemEffectManager_OnItemEquipped);
    }
}