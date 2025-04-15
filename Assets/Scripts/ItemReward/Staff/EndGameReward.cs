using UnityEngine;

public class EndGameReward : MonoBehaviour,IReward
{
    public void GetReward()
    {
        EventManager<IReward>.TriggerEvent(EventKey.Game_End, this);
    }
}
