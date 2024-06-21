using UnityEngine;

public class ItemReward : MonoBehaviour, IReward
{
    public void GetReward()
    {
        Debug.Log("item");
    }
}