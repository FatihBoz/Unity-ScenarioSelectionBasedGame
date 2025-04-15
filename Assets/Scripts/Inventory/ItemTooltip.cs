using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemTooltip : MonoBehaviour
{
    [Header("TEXT")]
    [SerializeField] protected TextMeshProUGUI itemName;
    [SerializeField] protected TextMeshProUGUI itemExplanation;
    [Header("BUTTON")]
    [SerializeField] protected Button tooltipPrimaryButton;
    [SerializeField] protected Button tooltipDropButton;
    [Header("PREFABS")]
    [SerializeField] private GameObject floatingText;

    protected ItemSO currentItem;
    protected Button tooltipCloseButtonPanel;
   

    protected virtual void Start()
    {
        if (tooltipDropButton == null || tooltipPrimaryButton == null)
        {
            return;
        }

        tooltipDropButton.onClick.AddListener(OnItemDropButtonClicked);
        tooltipPrimaryButton.onClick.AddListener(OnPrimaryButtonClicked);
    }


    public void SetItem(ItemSO item)
    {
        currentItem = item;
        itemName.text = item.name;
        itemName.color = ItemQualityColor.Instance.GetColor(item.ItemQuality);
        itemExplanation.text = item.ItemExplanation;
    }

    protected void DropItem(ItemSO item)
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_DROPPED, item);
        ItemSlot.tooltipIsActive = false;
        Destroy(this.gameObject);
    }

    public void SetCloseTooltipButtonPanel(Button closeButtonPanel)
    {
        tooltipCloseButtonPanel = closeButtonPanel;
    }

    protected void DestroyCloseButtonPanel()
    {
        Destroy(tooltipCloseButtonPanel.gameObject);
    }


    protected void OnItemDropButtonClicked()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        DestroyCloseButtonPanel();
        DropItem(currentItem);
    }

    public virtual void OnPrimaryButtonClicked()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        Instantiate(floatingText, transform.position, Quaternion.identity);
    }
}
