using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceTargetArea : MonoBehaviour,IDiceTarget, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image visibleTargetArea;
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private ItemSlot itemSlot;

    private ItemSO currentItem;
    private int coinNeedToPurchase;
    private bool isInArea;


    private void Start()
    {
        Initialize();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.CompareTag("Dice"))
        {
            isInArea = true;
            visibleTargetArea.DOFade(0.75f, 0.1f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isInArea)
        {
            visibleTargetArea.DOFade(1f, 0.1f);
        }
    }

    public void PlaceDiceOnItem(int coinAmount)
    {
        coinNeedToPurchase -= coinAmount;


        if (coinNeedToPurchase <= 0)
        {
            //item satýn alýnabilir olmalý
            SetCoinText(0);
        }
        else
        {
            SetCoinText(coinNeedToPurchase);
        }
    }

    void Initialize()
    {
        currentItem = itemSlot.CurrentItem;
        coinNeedToPurchase = currentItem.CoinNeedToPurchase;
        nameText.text = currentItem.ItemName;
        nameText.color = ItemQualityColor.Instance.GetColor(currentItem.ItemQuality);

        SetCoinText(coinNeedToPurchase);
    }


    void SetCoinText(int coinAmount)
    {
        coinText.text = coinAmount.ToString();
    }


    public bool isInTargetArea => isInArea;
    public Image visibleTarget => visibleTargetArea;
}
