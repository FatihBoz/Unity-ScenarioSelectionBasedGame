using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DiceTargetArea : MonoBehaviour,IDiceTarget, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image visibleTargetArea;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private ItemSlot itemSlot;

    [Header("*** CHANGEABLES ***")]
    [SerializeField] private Button purchaseButton;
    [SerializeField] private GameObject purchasedIcon;
    [SerializeField] private TextMeshProUGUI coinText;

    private ItemSO currentItem;
    private int coinNeedToPurchase;
    private bool isInArea;


    private void Start()
    {
        Initialize();
        purchaseButton.onClick.AddListener(OnPurhcaseButtonClicked);
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


    void OnPurhcaseButtonClicked()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_TAKEN, itemSlot.CurrentItem);
        purchaseButton.gameObject.SetActive(false);
        purchasedIcon.SetActive(true);
    }

    public void PlaceDiceOnItem(int coinAmount)
    {
        coinNeedToPurchase -= coinAmount;

        if (coinNeedToPurchase <= 0)
        {
            
            SetCoinText(0);

            visibleTarget.gameObject.SetActive(false);
            purchaseButton.gameObject.SetActive(true);
        }
        else
        {
            SetCoinText(coinNeedToPurchase);
        }
    }

    void Initialize()
    {
        currentItem = itemSlot.CurrentItem;
        coinNeedToPurchase = currentItem.CoinNeedToPurchase; // burada hata verdi
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
