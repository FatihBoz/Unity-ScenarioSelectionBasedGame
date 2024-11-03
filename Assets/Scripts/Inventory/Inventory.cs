using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxStaffSlot;
    [SerializeField] private int maxLootItemSlot;

    private Dictionary<ItemSO, int> itemInventory;
    private Dictionary<ItemSO,int> staffInventory;
    private ItemSO currentlyEquippedStaff;

    private void Awake()
    {
        itemInventory = new(maxLootItemSlot);
        staffInventory = new(maxStaffSlot);
    }

    public void AddItem(ItemSO item, int count = 1)
    {
        // Check if the item is a StaffSO
        if (item.ItemType == ItemType.Staff)
        {
            print("Add Item'a girdi");
            AddItemToInventory(item, staffInventory,maxStaffSlot,count);
            UpdateStaffUI();
        }
        else if(item.ItemType == ItemType.LootItem)
        {
            AddItemToInventory(item, itemInventory, maxLootItemSlot,count);
            UpdateLootItemUI();
        }
    }
    
    public void RemoveItem(ItemSO item)
    {
        if (item.ItemType == ItemType.Staff)
        {
            RemoveItemFromInventory(item,staffInventory);
            UpdateStaffUI();
        }
        else if(item.ItemType == ItemType.LootItem)
        {
            RemoveItemFromInventory(item, itemInventory);
            UpdateLootItemUI();
        }
    }
    


    void AddItemToInventory<T>(T item, Dictionary<T,int> inventory,int maxSlotCount,int count = 1)
    {
        if (inventory.Count >= maxSlotCount)
            return;


        if (inventory.ContainsKey(item))
        {
            inventory[item] += count;
        }
        // If the item does not exist, add it with the specified count
        else
        {
            inventory[item] = count;
        }

    }



    void RemoveItemFromInventory<T>(T item, Dictionary<T, int> inventory, int count = 1)
    {
        if (!inventory.ContainsKey(item))
            return;

        // If only a certain part of the items will be deleted
        if (inventory[item] > count)
        {
            inventory[item] -= count;
        }
        // If the whole item will be deleted
        else if (inventory[item] <= count)
        {
            inventory.Remove(item);
        }
    }


    private void UpdateStaffUI()
    {
        EventManager<Dictionary<ItemSO,int>>.TriggerEvent(EventKey.Staff_Inventory_Update, staffInventory);
    }

    private void UpdateLootItemUI()
    {
        EventManager<Dictionary<ItemSO, int>>.TriggerEvent(EventKey.LootItem_Inventory_Update, itemInventory);
    }


    private void InventoryManager_OnItemTaken(ItemSO item)
    {
        AddItem(item);
    }

    private void InventoryUI_OnItemDropped(ItemSO item)
    {
        RemoveItem(item);
    }



    private void InventoryManager_OnStaffEquipped(ItemSO staff)
    {
        if (currentlyEquippedStaff != null)
        {
            ItemSO previousStaff = currentlyEquippedStaff;
            currentlyEquippedStaff = staff;
            RemoveItem(staff);
            //if inventory still does not have empty slot,
            //previous staff will be deleted.
            AddItem(previousStaff);
            
        }
        else
        {
            RemoveItem(staff);
            currentlyEquippedStaff = staff;
        }
        
        UpdateEquippedStaffUI();
    }

    void UpdateEquippedStaffUI()
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.Equipped_Staff_Changed, currentlyEquippedStaff);
    }



    private void OnEnable()
    {
        EventManager<ItemSO>.Subscribe(EventKey.STAFF_EQUIPPED, InventoryManager_OnStaffEquipped);
        EventManager<ItemSO>.Subscribe(EventKey.ITEM_TAKEN, InventoryManager_OnItemTaken);
        EventManager<ItemSO>.Subscribe(EventKey.ITEM_DROPPED, InventoryUI_OnItemDropped);

    }



    private void OnDisable()
    {
        EventManager<ItemSO>.Unsubscribe(EventKey.STAFF_EQUIPPED, InventoryManager_OnStaffEquipped);
        EventManager<ItemSO>.Unsubscribe(EventKey.ITEM_TAKEN, InventoryManager_OnItemTaken);
        EventManager<ItemSO>.Unsubscribe(EventKey.ITEM_DROPPED, InventoryUI_OnItemDropped);
    }
}
