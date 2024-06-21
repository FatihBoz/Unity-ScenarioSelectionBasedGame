using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthReward : MonoBehaviour,IReward
{
    public void GetReward()
    {
        Debug.Log("HP");
    }

}
