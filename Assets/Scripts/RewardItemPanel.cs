using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardItemPanel : MonoBehaviour
{
    private StaffSO currentItem;

    #region ITEM ELEMENTS

    [SerializeField] private GameObject backgroundPanel;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemExplanation;
    [SerializeField] private Image itemImage;
    [SerializeField] private Image backgroundColor;

    #endregion ITEM ELEMENTS

    private void RewardItemPanel_OnItemFound(StaffSO item)
    {
        currentItem = item;
        DisplayItem(item);
        SetItemPanelActive();
    }

    public void OnDiscardButtonPressed()
    {
        SetItemPanelInactive();
    }

    public void OnEquipButtonPressed()
    {
        EventManager<StaffSO>.TriggerEvent(EventKey.STAFF_EQUIPPED, currentItem);
        SetItemPanelInactive();
    }

    private void DisplayItem(StaffSO item)
    {
        itemName.text = item.ItemName;
        itemExplanation.text = item.ItemExplanation;
        backgroundColor.color = item.GetColor();
        itemImage.sprite = item.ItemSprite;
        itemImage.SetNativeSize();
    }

    private void SetItemPanelInactive()
    {
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
        EventManager<StaffSO>.Subscribe(EventKey.STAFF_FOUND, RewardItemPanel_OnItemFound);
    }

    private void OnDisable()
    {
        EventManager<StaffSO>.Unsubscribe(EventKey.STAFF_FOUND, RewardItemPanel_OnItemFound);
    }
}