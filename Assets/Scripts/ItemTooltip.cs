using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTooltip : MonoBehaviour
{
    [SerializeField] protected TextMeshProUGUI itemName;
    [SerializeField] protected TextMeshProUGUI itemExplanation;
    [SerializeField] protected Button itemDropButton;

    private Button tooltipCloseButtonPanel;
    private ItemSO currentItem;

    protected virtual void Start()
    {
        itemDropButton.onClick.AddListener(OnItemDropButtonClicked);
    }

    protected virtual void OnItemDropButtonClicked()
    {
        DropItem(currentItem);
    }


    public void SetItem(ItemSO item)
    {
        currentItem = item;
        itemName.text = item.name;
        itemName.color = ItemQualityColor.GetColor(item.ItemQuality);
        itemExplanation.text = item.ItemExplanation;
    }

    public void SetCloseTooltipButtonPanel(Button closeButtonPanel)
    {
        tooltipCloseButtonPanel = closeButtonPanel;
    }

    protected void DropItem(ItemSO item)
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_DROPPED, item);
        ItemSlot.tooltipIsActive = false;
        DestroyCloseButtonPanel();
        Destroy(this.gameObject);
    }


    protected void DestroyCloseButtonPanel()
    {
        Destroy(tooltipCloseButtonPanel.gameObject);
    }
}
