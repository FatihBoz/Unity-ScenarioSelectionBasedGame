using UnityEngine;

public class LevelReward : MonoBehaviour, IReward
{
    public void GetReward()
    {
        PlayerAttributes.Instance.LevelUp();
        Debug.Log(PlayerAttributes.Instance.Level);
    }
}
