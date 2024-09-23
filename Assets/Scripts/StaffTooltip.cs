using UnityEngine;
using UnityEngine.UI;

public class StaffTooltip : ItemTooltip
{
    [SerializeField] private Button itemEquipButton;

    private StaffSO currentStaff;

    protected override void Start()
    {
        base.Start();
        itemEquipButton.onClick.AddListener(OnItemEquipButtonClicked);
    }

    public void SetStaff(StaffSO staff)
    {
        currentStaff = staff;
        itemName.text = staff.ItemName;
        itemName.color = ItemQualityColor.GetColor(staff.ItemQuality);
        itemExplanation.text = staff.ItemExplanation;
    }

    private void OnItemEquipButtonClicked()
    {
        EventManager<StaffSO>.TriggerEvent(EventKey.STAFF_EQUIPPED, currentStaff);
        DestroyCloseButtonPanel();
        Destroy(this.gameObject); 
    }

    protected override void OnItemDropButtonClicked()
    {
        DropItem(currentStaff);
    }
}
