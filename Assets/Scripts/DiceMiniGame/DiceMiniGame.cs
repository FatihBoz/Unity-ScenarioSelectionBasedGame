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

    [Header("Float Values")]
    [SerializeField] private float diceTotalTextDisappearTime = 1f;
    [SerializeField] private float spriteChangeTimeAfterRoll = 1f;
    int totalRollCount = 0;

    [Header("*** ITEM PURCHASE ***")]

    [SerializeField] private Canvas miniGameCanvas;
    [SerializeField] private ItemHolderSO itemHolder;
    [SerializeField] private Transform itemsPanel;
    [SerializeField] private GameObject traderItemPrefab;


    private readonly int itemTierRangeToSelect = 3;
    private readonly int itemCountToSelect = 4;

    private void Start()
    {
        rollDiceButton.onClick.AddListener(RollDice);
        StartCoroutine(SelectTraderItems(itemCountToSelect));
    }

    private IEnumerator SelectTraderItems(int itemCount)
    {
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
        totalRollCount = 0;
        foreach (Dice d in diceList)
        {
            d.gameObject.SetActive(true);
            d.Roll();
        }

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
        rollDiceButton.interactable = true;
    }
}
