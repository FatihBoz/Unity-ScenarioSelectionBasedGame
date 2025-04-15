

public class StaffTooltip : ItemTooltip
{
    public override void OnPrimaryButtonClicked()
    {
        base.OnPrimaryButtonClicked();
        EventManager<ItemSO>.TriggerEvent(EventKey.STAFF_EQUIPPED, currentItem);
        DestroyCloseButtonPanel();
        ItemSlot.tooltipIsActive = false;
        Destroy(this.gameObject);
    }
}
