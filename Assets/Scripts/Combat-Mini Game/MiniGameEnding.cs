using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameEnding : MiniGamePanel
{
    [Header("Int Values")]
    [SerializeField] private int minLootCount = 1;
    [SerializeField] private int maxLootCount = 3;

    [Header("Loot Item Display")]
    [SerializeField] private GameObject lootItemTextPrefab;
    [SerializeField] private Transform lootItemHolder;

    [Header("Button")]
    [SerializeField] private Button continueButton;//End combat button


    private void Start()
    {
        continueButton.onClick.AddListener(OnContinueButtonClicked);
    }


    private void OnContinueButtonClicked()
    {
        panel.SetActive(false);
    }


    private void MiniGameEnding_OnMiniGameFinished(Combat combat)
    {
        panel.SetActive(true);
        SetBackgroundImage();

        //if enemy lost
        if (combat is EnemyCombat enemyCombat)
        {
            //!winPanelPrefab.SetActive(true);
            //Gain random loot items
            for (int i = 0; i < Random.Range(minLootCount, maxLootCount +1); i++)
            {
                int order = Random.Range(0, enemyCombat.LootItemDrop.Count);
                ItemSO item = enemyCombat.LootItemDrop[order];

                GameObject tempText = Instantiate(lootItemTextPrefab, lootItemHolder);

                if (tempText.TryGetComponent<TextMeshProUGUI>(out var itemName))
                {
                    itemName.text = "*"+item.ItemName;
                    itemName.color = ItemQualityColor.Instance.GetColor(item.ItemQuality);
                }


                EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_TAKEN, item);
            }            
        }
        else
        {
            //!losePanelPrefab.SetActive(true);
        }
    }




    private void OnEnable()
    {
        EventManager<Combat>.Subscribe(EventKey.MiniGame_Finished, MiniGameEnding_OnMiniGameFinished);
    }

    private void OnDisable()
    {
        EventManager<Combat>.Unsubscribe(EventKey.MiniGame_Finished, MiniGameEnding_OnMiniGameFinished);
    }


}
