using UnityEngine;

public class GoldCoinManager : MonoBehaviour
{
    public static GoldCoinManager Instance;

    private int goldCoin = 10;

    private void Awake()
    {
        Instance = this;
    }


    public void EarnGoldCoins(int coinAmount)
    {
        goldCoin += coinAmount;
        EventManager<int>.TriggerEvent(EventKey.Gold_Coin_Update, goldCoin);
    }

    public void SpendGoldCoins(int coinAmountNeeded)
    {
        if (goldCoin >= coinAmountNeeded)
        {
            goldCoin -= coinAmountNeeded;
            EventManager<int>.TriggerEvent(EventKey.Gold_Coin_Update, goldCoin);
        }
    }

    public int GoldCoinAmount => goldCoin;


}
