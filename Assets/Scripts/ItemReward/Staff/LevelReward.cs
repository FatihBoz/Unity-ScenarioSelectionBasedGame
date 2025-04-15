using UnityEngine;

public class LevelReward : MonoBehaviour, IReward
{
    public void GetReward()
    {
        PlayerAttributes.Instance.LevelUp(1);
        FloatingTextManager.Instance.InstantiateFloatingText(FloatingTextType.Level_Up);
    }
}
