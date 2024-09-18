using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform staffInventoryUI;
    [SerializeField] private Transform lootItemInventoryUI;

    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject staffSlotPrefab;

    public GameObject envanter;

    private void InventoryUI_OnStaffInventoryUpdated(List<StaffSO> list)
    {
        //Clear staff inventory
        foreach(Transform staff in staffInventoryUI)
        {
            Destroy(staff.gameObject);
        }
        
        
        foreach (StaffSO staff in list)
        {
            GameObject obj = Instantiate(staffSlotPrefab, staffInventoryUI);
            if(obj.transform.Find("StaffImage").TryGetComponent<Image>(out var image))
            {
                image.sprite = staff.ItemSprite; 
            }
        }
    }

    private void InventoryUI_OnLootItemInventoryUpdated(Dictionary<LootItemSO, int> dict)
    {
        //Clear staff inventory
        foreach (Transform item in staffInventoryUI)
        {
            Destroy(item.gameObject);
        }


        foreach (LootItemSO item in dict.Keys)
        {
            GameObject obj = Instantiate(itemSlotPrefab, lootItemInventoryUI);
            if (obj.TryGetComponent<ItemSlot>(out var itemSlot))
            {
                itemSlot.ItemImage.sprite = item.ItemSprite;
                itemSlot.Count.text = item.ItemCount.ToString();
            }
        }
    }


    public void EnvanterAc()
    {
        envanter.SetActive(true);
    }


    private void OnEnable()
    {
        EventManager<Dictionary<LootItemSO, int>>.Subscribe(EventKey.LootItem_Inventory_Update, InventoryUI_OnLootItemInventoryUpdated);
        EventManager<List<StaffSO>>.Subscribe(EventKey.Staff_Inventory_Update, InventoryUI_OnStaffInventoryUpdated);
    }

    private void OnDisable()
    {
        EventManager<Dictionary<LootItemSO, int>>.Unsubscribe(EventKey.LootItem_Inventory_Update, InventoryUI_OnLootItemInventoryUpdated);
        EventManager<List<StaffSO>>.Unsubscribe(EventKey.Staff_Inventory_Update, InventoryUI_OnStaffInventoryUpdated);
    }
}
