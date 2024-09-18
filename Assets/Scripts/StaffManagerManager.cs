using UnityEngine;

public class StaffManager : MonoBehaviour
{
    public static StaffSO CurrentItem { get; private set; }

    public void ItemManager_OnItemEquip(StaffSO item)
    {
        CurrentItem = item;
    }

    private void OnEnable()
    {
        EventManager<StaffSO>.Subscribe(EventKey.STAFF_EQUIPPED, ItemManager_OnItemEquip);
    }

    private void OnDisable()
    {
        EventManager<StaffSO>.Unsubscribe(EventKey.STAFF_EQUIPPED, ItemManager_OnItemEquip);
    }
}
