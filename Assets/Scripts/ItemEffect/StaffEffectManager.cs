using UnityEngine;

public class StaffEffectManager : MonoBehaviour
{
    private void ItemEffectManager_OnItemEquipped(StaffSO item)
    {
        IStaffEffect itemEffect = item.GetItemEffectPrefab().GetComponent<IStaffEffect>();
        itemEffect?.ApplyEffect(item.GetTier());
    }

    private void OnEnable()
    {
        EventManager<StaffSO>.Subscribe(EventKey.STAFF_EQUIPPED, ItemEffectManager_OnItemEquipped);
    }

    private void OnDisable()
    {
        EventManager<StaffSO>.Unsubscribe(EventKey.STAFF_EQUIPPED, ItemEffectManager_OnItemEquipped);
    }
}