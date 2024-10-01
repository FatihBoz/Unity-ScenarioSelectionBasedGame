using UnityEngine;

public class StaffItemReward : MonoBehaviour, IReward
{
    [SerializeField] private ItemHolderSO staffHolder;
    
    public void GetReward()
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.STAFF_FOUND,staffHolder.SelectItem());
    }

    
}