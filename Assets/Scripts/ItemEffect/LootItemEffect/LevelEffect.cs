public class LevelEffect : LootItemEffect
{
    public override void ApplyItemEffect(ItemSO item)
    {
        PlayerAttributes.Instance.LevelUp(item.Tier);
        FloatingTextManager.Instance.InstantiateFloatingText(FloatingTextType.Level_Up);
    }
}
