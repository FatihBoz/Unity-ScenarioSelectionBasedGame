using UnityEngine;

public class GoldCoinEffect :  LootItemEffect
{
    [SerializeField] private int minExtraGold = 2;
    [SerializeField] private int maxExtraGold = 6;

    public override void ApplyItemEffect(ItemSO item)
    {
        GoldCoinManager.Instance.EarnGoldCoins(item.CoinNeedToPurchase + RandomExtraGold());
        FloatingTextManager.Instance.InstantiateFloatingText(FloatingTextType.Gold_Earned);
    }

    int RandomExtraGold()
    {
        return Random.Range(minExtraGold, maxExtraGold + 1);
    }

}
