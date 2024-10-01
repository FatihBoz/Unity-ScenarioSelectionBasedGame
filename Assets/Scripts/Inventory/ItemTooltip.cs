using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemTooltip : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI itemName;
    [SerializeField] protected TextMeshProUGUI itemExplanation;
    [SerializeField] protected Button tooltipPrimaryButton;
    [SerializeField] protected Button tooltipDropButton;

    protected ItemSO currentItem;
    protected Button tooltipCloseButtonPanel;
   

    protected virtual void Start()
    {
        tooltipDropButton.onClick.AddListener(OnItemDropButtonClicked);
        tooltipPrimaryButton.onClick.AddListener(OnPrimaryButtonClicked);
    }


    public void SetItem(ItemSO item)
    {
        currentItem = item;
        itemName.text = item.name;
        itemName.color = ItemQualityColor.GetColor(item.ItemQuality);
        itemExplanation.text = item.ItemExplanation;
    }

    protected void DropItem(ItemSO item)
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_DROPPED, item);
        ItemSlot.tooltipIsActive = false;
        DestroyCloseButtonPanel();
        Destroy(this.gameObject);
    }


    protected void OnItemDropButtonClicked()
    {
        DropItem(currentItem);
    }

    protected void DestroyCloseButtonPanel()
    {
        Destroy(tooltipCloseButtonPanel.gameObject);
    }

    public void SetCloseTooltipButtonPanel(Button closeButtonPanel)
    {
        tooltipCloseButtonPanel = closeButtonPanel;
    }


    public abstract void OnPrimaryButtonClicked();
}
