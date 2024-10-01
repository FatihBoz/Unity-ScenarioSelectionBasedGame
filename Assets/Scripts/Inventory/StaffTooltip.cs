

public class StaffTooltip : ItemTooltip
{
    public override void OnPrimaryButtonClicked()
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.STAFF_EQUIPPED, currentItem);
        DestroyCloseButtonPanel();
        Destroy(this.gameObject);
    }
}
