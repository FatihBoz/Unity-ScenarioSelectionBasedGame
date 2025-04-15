using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DiceMiniGame : MonoBehaviour
{
    [Header("*** DICE ROLL ***")]

    [SerializeField] private List<Dice> diceList;
    [SerializeField] private Button rollDiceButton;
    [SerializeField] private TextMeshProUGUI diceTotalForOneRound;
    [SerializeField] private TextMeshProUGUI goldCoinAmount;
    [SerializeField] private int diceRollingCost = 6;
    private readonly int itemTierRangeToSelect = 2;
    private readonly int itemCountToSelect = 4;

    [Header("Float Values")]
    [SerializeField] private float diceTotalTextDisappearTime = 1f;
    [SerializeField] private float spriteChangeTimeAfterRoll = 1f;
    int totalRollCount = 0;

    [Header("*** ITEM PURCHASE ***")]

    [SerializeField] private ItemHolderSO itemHolder;
    [SerializeField] private Transform itemsPanel;
    [SerializeField] private GameObject traderItemPrefab;

    [Header("** Background **")]
    [SerializeField] private Image backgroundImage;

    private Canvas miniGameCanvas;






    private void Start()
    {
        rollDiceButton.onClick.AddListener(RollDice);

        backgroundImage.sprite = BackgroundManager.Instance.GetBackgroundImage().sprite;

        StartCoroutine(SelectTraderItems(itemCountToSelect)); // burada hata verdi

        if (!CanRoll())
        {
            rollDiceButton.interactable = false;
        }
        UpdateGoldCoinText();
    }


    public void CloseGame()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        Destroy(gameObject);
    }



    private IEnumerator SelectTraderItems(int itemCount)
    {

        foreach (Transform item in itemsPanel)
        {
            Destroy(item.gameObject);
        }

        for (int i = 0; i < itemCount; i++)
        {
            //Only items with  equal or +1 level can be selected.
            ItemSO itemSO = itemHolder.SelectItemForTrader(itemTierRangeToSelect);

            GameObject obj = Instantiate(traderItemPrefab,itemsPanel);

            ItemSlot slot = obj.GetComponentInChildren<ItemSlot>();
            
            slot.SetCanvas(miniGameCanvas);
            slot.SetItem(itemSO);

            yield return new WaitForEndOfFrame();
        }
    }

    void RollDice()
    {
        SoundEffectManager.Instance.PlayButtonClickSF();
        totalRollCount = 0;
        foreach (Dice d in diceList)
        {
            d.gameObject.SetActive(true);
            d.Roll();
        }
        GoldCoinManager.Instance.SpendGoldCoins(diceRollingCost);
        UpdateGoldCoinText();

        rollDiceButton.interactable = false;
        StartCoroutine(DelayedSpriteChange());



        
            
    }

    private IEnumerator DelayedSpriteChange()
    {
        yield return new WaitForSeconds(spriteChangeTimeAfterRoll);

        foreach (Dice d in diceList)
        {
            totalRollCount += d.ChangeSprite();
        }

        diceTotalForOneRound.gameObject.SetActive(true);
        diceTotalForOneRound.text = totalRollCount.ToString();

        StartCoroutine(DelayedDisappear());
    }


    private IEnumerator DelayedDisappear()
    {
        yield return new WaitForSeconds(diceTotalTextDisappearTime);
        diceTotalForOneRound.gameObject.SetActive(false);
        if (!CanRoll())
        {
            rollDiceButton.interactable = false;
        }
        else
        {
            rollDiceButton.interactable = true;
        }
    }

    void UpdateGoldCoinText()
    {
        goldCoinAmount.text = GoldCoinManager.Instance.GoldCoinAmount.ToString();
    }

    bool CanRoll()
    {
        return GoldCoinManager.Instance.GoldCoinAmount >= diceRollingCost;
    }

    public void SetCanvas(Canvas canvas)
    {   
        miniGameCanvas = canvas;

        foreach (Dice d in diceList)
        {
            d.SetCanvas(miniGameCanvas);
        }
    }
}
