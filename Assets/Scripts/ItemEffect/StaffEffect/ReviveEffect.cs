using UnityEngine;

public class ReviveEffect : MonoBehaviour, IStaffEffect
{
    public void ApplyEffect(int tier)
    {
        StaffAttributes.Instance.EscapeFromDeath();
    }
}   
