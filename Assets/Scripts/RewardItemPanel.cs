using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemPanel : MonoBehaviour
{
    private ItemSO currentItem;

    #region ITEM ELEMENTS

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemExplanation;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image backgroundColor;

    #endregion ITEM ELEMENTS

    private void RewardItemPanel_OnItemFound(ItemSO item)
    {
        currentItem = item;
        DisplayItem(item);
        SetItemPanelActive();
    }

    public void OnDiscardButtonPressed()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        SetItemPanelInactive();
    }

    public void OnTakeButtonPressed()
    {   
        SoundEffectManager.Instance.PlayButtonClickSF();
        EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_TAKEN, currentItem);
        SetItemPanelInactive();
    }

    private void DisplayItem(ItemSO item)
    {
        itemName.text = item.ItemName;
        itemExplanation.text = item.ItemExplanation;
        backgroundColor.color = ItemQualityColor.Instance.GetColor(item.ItemQuality);
        itemImage.sprite = item.ItemSprite;
        itemImage.SetNativeSize();
    }

    private void SetItemPanelInactive()
    {
        transform.DOKill();
        backgroundPanel.SetActive(false);
        gameObject.transform.localScale = Vector3.zero;
    }

    private void SetItemPanelActive()
    {
        backgroundPanel.SetActive(true);
        this.gameObject.transform.DOScale(1f, 1f);
    }

    private void OnEnable()
    {
        EventManager<ItemSO>.Subscribe(EventKey.STAFF_FOUND, RewardItemPanel_OnItemFound);
    }

    private void OnDisable()
    {
        EventManager<ItemSO>.Unsubscribe(EventKey.STAFF_FOUND, RewardItemPanel_OnItemFound);
    }
}