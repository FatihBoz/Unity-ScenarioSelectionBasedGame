using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelReward : MonoBehaviour, IReward
{
    public void GetReward()
    {
        Debug.Log("level");
    }
}
