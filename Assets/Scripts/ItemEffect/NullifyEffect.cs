using UnityEngine;

public class NullifyEffect : MonoBehaviour, IStaffEffect
{
    [SerializeField] private float baseNullifyingChance = 0.1f;
    public void ApplyEffect(int tier)
    {
        StaffAttributes.Instance.SetNullifyChance(baseNullifyingChance * tier);
    }

 
    
}
