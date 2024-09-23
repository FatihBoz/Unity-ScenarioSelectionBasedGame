using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public static bool tooltipIsActive;

    [SerializeField] private TextMeshProUGUI count;

    [SerializeField] private Image itemImage;

    [SerializeField] private GameObject itemTooltipPrefab;  // For general items

    [SerializeField] private GameObject staffTooltipPrefab;  // For staffs specifically

    [SerializeField] private Vector2 offSet;

    [SerializeField] private Button closeButtonPanel;


    private Canvas canvas;
    private Button itemSlot;
    private ItemSO currentItem;



    private void Awake()
    {
        itemSlot = GetComponent<Button>();
       
    }

    private void Start()
    {
        itemSlot.onClick.AddListener(MakeTooltipActive);
        tooltipIsActive = false;
    }

    private void MakeTooltipActive()
    {
        if (currentItem == null || canvas == null || tooltipIsActive) 
            return;

        // Check if the current item is a StaffSO
        if (currentItem is StaffSO staffItem)
        {
            OpenTooltip(staffItem,staffTooltipPrefab);
        }
        else
        {
            OpenTooltip(currentItem,itemTooltipPrefab);
        }
    }



    void OpenTooltip(ItemSO item, GameObject tooltipPrefab)
    {
        Button b = Instantiate(closeButtonPanel, canvas.transform);

        GameObject tooltipInstance = Instantiate(tooltipPrefab, (Vector2)transform.position + offSet, Quaternion.identity, canvas.transform);

        //Check if the item is of type StaffSO and tooltip has a StaffTooltip component
        if (item is StaffSO staffItem && tooltipInstance.TryGetComponent<StaffTooltip>(out var staffTooltip))
        {
            // Set the StaffSO specific information
            staffTooltip.SetStaff(staffItem);
            staffTooltip.SetCloseTooltipButtonPanel(b);
        }
        //if it is not StaffSO
        else
        {
            if (tooltipInstance.TryGetComponent<ItemTooltip>(out var itemTooltip))
            {
                //Set the general ItemSO information
                itemTooltip.SetItem(currentItem);
                itemTooltip.SetCloseTooltipButtonPanel(b);
            }
        }

        tooltipIsActive = true;
        SetCloseButtonPanel(tooltipInstance,b);
    }


    void SetCloseButtonPanel(GameObject tooltip,Button b)
    {

        b.onClick.AddListener(() =>
        {
            Destroy(tooltip);
            tooltipIsActive = false;
            Destroy(b.gameObject);

        });
    }


    public void SetItem(ItemSO item,int itemCount)
    {
        currentItem = item;
        itemImage.sprite = item.ItemSprite;
        count.text = itemCount.ToString();

        if(itemCount > 1)
        {
            count.gameObject.SetActive(true);
        }
    }

    public void SetCanvas(Canvas canvas)
    {
        this.canvas = canvas;
    }
}
