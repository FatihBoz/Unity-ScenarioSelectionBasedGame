using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private Transform staffInventoryUI;
    [SerializeField] private Transform lootItemInventoryUI;

    [Header("Prefabs")]
    [SerializeField] private GameObject itemSlotPrefab;
    [SerializeField] private GameObject staffSlotPrefab;

    [Header("Equipped Staff")]
    [SerializeField] private Image equippedStaffImage;
    [SerializeField] private TextMeshProUGUI equippedStaffName;
    [SerializeField] private TextMeshProUGUI equippedStaffExplanation;

    [SerializeField] private Canvas canvas;
    [SerializeField] private GameObject inventory;

    //todo:INVENTORY UPDATE METODLARI TEK METODDA BÝRLEÞTÝRÝLEBÝLÝR.
    private void InventoryUI_OnStaffInventoryUpdated(Dictionary<ItemSO, int> dict)
    {
        //Clear staff inventory
        foreach (Transform staff in staffInventoryUI)
        {
            Destroy(staff.gameObject);
        }

        foreach (ItemSO staff in dict.Keys)
        {
            GameObject obj = Instantiate(staffSlotPrefab, staffInventoryUI);
            if (obj.TryGetComponent<ItemSlot>(out var itemSlot))
            {
                print("Staff Inventory UI'a girdi");
                itemSlot.SetItem(staff, dict[staff]);
                itemSlot.SetCanvas(canvas);
            }
        }
    }

    private void InventoryUI_OnLootItemInventoryUpdated(Dictionary<ItemSO, int> dict)
    {
        //Clear loot item inventory
        foreach (Transform item in lootItemInventoryUI)
        {
            Destroy(item.gameObject);
        }

        foreach (ItemSO item in dict.Keys)
        {
            GameObject obj = Instantiate(itemSlotPrefab, lootItemInventoryUI);
            if (obj.TryGetComponent<ItemSlot>(out var itemSlot))
            {
                itemSlot.SetItem(item, dict[item]);
                itemSlot.SetCanvas(canvas);
            }
        }
    }


    private void InventoryUI_OnEquippedStaffChanged(ItemSO staff)
    {
        equippedStaffImage.sprite = staff.ItemSprite;
        equippedStaffName.text = staff.ItemName;
        equippedStaffName.color = ItemQualityColor.GetColor(staff.ItemQuality);
        equippedStaffExplanation.text = staff.ItemExplanation;

        if (!equippedStaffImage.IsActive())
        {
            equippedStaffImage.gameObject.SetActive(true);
        }
    }



    public void Open_Close_Inventory()
    {
        //if object is not active, make it active and vice versa
        inventory.SetActive(!inventory.activeSelf);
    }

    private void OnEnable()
    {
        EventManager<Dictionary<ItemSO, int>>.Subscribe(EventKey.LootItem_Inventory_Update, InventoryUI_OnLootItemInventoryUpdated);
        EventManager<Dictionary<ItemSO,int>>.Subscribe(EventKey.Staff_Inventory_Update, InventoryUI_OnStaffInventoryUpdated);
        EventManager<ItemSO>.Subscribe(EventKey.Equipped_Staff_Changed, InventoryUI_OnEquippedStaffChanged);
    }



    private void OnDisable()
    {
        EventManager<Dictionary<ItemSO, int>>.Unsubscribe(EventKey.LootItem_Inventory_Update, InventoryUI_OnLootItemInventoryUpdated);
        EventManager<Dictionary<ItemSO, int>>.Unsubscribe(EventKey.Staff_Inventory_Update, InventoryUI_OnStaffInventoryUpdated);
        EventManager<ItemSO>.Unsubscribe(EventKey.Equipped_Staff_Changed, InventoryUI_OnEquippedStaffChanged);
    }
}