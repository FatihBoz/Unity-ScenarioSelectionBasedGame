
public class LootItemTooltip : ItemTooltip
{
    public override void OnPrimaryButtonClicked()
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.LootItem_Used, currentItem);
        DropItem(currentItem);
    }
}