using UnityEngine;

[CreateAssetMenu(fileName = "New Staff", menuName = "Staff")]
public class StaffSO : ItemSO
{
    [SerializeField] private int tier;
    [SerializeField] private GameObject itemEffectPrefab;
    [SerializeField] private Color color;


    public int GetTier() => tier;

    public GameObject GetItemEffectPrefab() => itemEffectPrefab; 

    public Color GetColor() => color;
}
