using UnityEngine;

public class ReviveEffect : MonoBehaviour, IItemEffect
{
    public void ApplyEffect(int tier)
    {
        ItemAttributes.Instance.EscapeFromDeath();
    }
}   
