using UnityEngine;

public class DeathReward : MonoBehaviour, IReward
{
    public void GetReward()
    {
        EventManager<float>.TriggerEvent(EventKey.HEALTH_DECREASED, PlayerAttributes.Instance.MaxHp);
    }
}
