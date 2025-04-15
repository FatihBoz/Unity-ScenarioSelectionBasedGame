
public class LootItemTooltip : ItemTooltip
{
    public override void OnPrimaryButtonClicked()
    {
        base.OnPrimaryButtonClicked();
        EventManager<ItemSO>.TriggerEvent(EventKey.LootItem_Used, currentItem);
        DestroyCloseButtonPanel();
        DropItem(currentItem);
    }
}