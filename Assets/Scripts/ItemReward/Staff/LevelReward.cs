using UnityEngine;

public class LevelReward : MonoBehaviour, IReward
{
    public void GetReward()
    {
        PlayerAttributes.Instance.LevelUp(1);
        Debug.Log(PlayerAttributes.Instance.Level);
    }
}
