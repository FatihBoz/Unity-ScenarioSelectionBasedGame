
public class MaxManaEffect   : LootItemEffect
{
    private readonly float baseManaIncreasingAmount = 5f;
    public override void ApplyItemEffect(ItemSO item)
    {
        PlayerAttributes.Instance.IncreaseMaxMana(baseManaIncreasingAmount * item.Tier);
    }
}
