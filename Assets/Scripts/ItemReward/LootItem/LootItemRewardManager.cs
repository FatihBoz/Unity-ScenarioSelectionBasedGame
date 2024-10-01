using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootItemRewardManager : MonoBehaviour
{
    [SerializeField] private ItemHolderSO lootItemHolder;
    [SerializeField] private GameObject rewardPanelPrefab;
    [SerializeField] private Canvas canvas;

    private readonly string imageComponentName = "Item Image";
    private readonly string textComponentName = "Item Name Text";

    private void OnEnable()
    {
        EventManager<ScenarioCard>.Subscribe(EventKey.ScenarioCard_Flipped, OnLootItemRewardManager_ScenarioCardFlipped);
    }

    private void OnDisable()
    {
        EventManager<ScenarioCard>.Unsubscribe(EventKey.ScenarioCard_Flipped, OnLootItemRewardManager_ScenarioCardFlipped);
    }

    private void OnLootItemRewardManager_ScenarioCardFlipped(ScenarioCard scenarioCard)
    {
        if (scenarioCard.Scenario.LootItemType == LootItemEffectType.None)
        {
            return;
        }
        //Insantiate panel as child of scenario card, because selected scenario card will be destroyed,
        //no need to destroy reward panel additionally
        GameObject rewardPanel = Instantiate(rewardPanelPrefab, scenarioCard.gameObject.transform);
        //Reset reward panel's position
        rewardPanel.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        //Select a random item
        ItemSO item = lootItemHolder.SelectItem(scenarioCard.Scenario.LootItemType);

        EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_TAKEN, item);

        //todo:It is better to create a new class to set image and text
        
        if(rewardPanel.transform.Find(imageComponentName).TryGetComponent<Image>(out var image))
        {
            image.sprite = item.ItemSprite;
        }

        if (rewardPanel.transform.Find(textComponentName).TryGetComponent<TextMeshProUGUI>(out var text))
        {
            text.text = item.ItemName;
            text.color = ItemQualityColor.GetColor(item.ItemQuality);
        }

    }
}
