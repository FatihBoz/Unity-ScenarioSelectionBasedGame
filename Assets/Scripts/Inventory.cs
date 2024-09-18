using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxStaffSlot;
    [SerializeField] private int maxLootItemSlot;

    private Dictionary<LootItemSO, int> itemInventory;
    private List<StaffSO> staffInventory;

    private void Awake()
    {
        itemInventory = new(maxLootItemSlot);
        staffInventory = new(maxStaffSlot);
    }

    public void AddLootItem(LootItemSO newItem, int count)
    {
        if (itemInventory.Count < maxLootItemSlot)
        {
            return;
        }


        if (itemInventory.ContainsKey(newItem))
        {
            itemInventory[newItem] += count;
        }
        else
        {
            itemInventory.Add(newItem, count);
            //Update UI
            EventManager<Dictionary<LootItemSO, int>>.
                TriggerEvent(EventKey.LootItem_Inventory_Update, itemInventory);
        }
    }

    public void AddStaff(StaffSO staff)
    {
        if (staffInventory.Count < maxStaffSlot)
        {
            staffInventory.Add(staff);
            //Update UI
            EventManager<List<StaffSO>>.
                TriggerEvent(EventKey.Staff_Inventory_Update,staffInventory);
        }
    }




    public void RemoveStaff(StaffSO staff)
    {
        if (staffInventory.Contains(staff))
        {
            staffInventory.Remove(staff);
            //Update UI
            EventManager<List<StaffSO>>.
                TriggerEvent(EventKey.Staff_Inventory_Update, staffInventory);
        }
    }

    public void RemoveLootItem(LootItemSO lootItem,int count)
    {
        //if only a certain part of the items will be deleted
        if (itemInventory[lootItem] > count)
        {
            itemInventory[lootItem] -= count;
        }
        //if whole item will be deleted
        else if (itemInventory[lootItem] == count)
        {
            itemInventory.Remove(lootItem);
            //Update UI
            EventManager<Dictionary<LootItemSO, int>>.
                TriggerEvent(EventKey.LootItem_Inventory_Update, itemInventory);
        }
    }



    private void InventoryManager_OnStaffEquipped(StaffSO staff)
    {
        AddStaff(staff);
    }

    private void InventoryManager_OnLootItemFound(LootItemSO item)
    {
        AddLootItem(item, 1);
    }

    private void OnEnable()
    {
        EventManager<StaffSO>.Subscribe(EventKey.STAFF_EQUIPPED, InventoryManager_OnStaffEquipped);
        EventManager<LootItemSO>.Subscribe(EventKey.LOOT_ITEM_FOUND, InventoryManager_OnLootItemFound);
        
    }



    private void OnDisable()
    {
        EventManager<StaffSO>.Unsubscribe(EventKey.STAFF_EQUIPPED, InventoryManager_OnStaffEquipped);
        EventManager<LootItemSO>.Unsubscribe(EventKey.LOOT_ITEM_FOUND, InventoryManager_OnLootItemFound);
    }
}
