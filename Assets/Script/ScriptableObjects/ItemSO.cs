using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [TextArea][SerializeField] private string ItemExplanation;
    [SerializeField] private string itemName;
    [SerializeField] private int tier;
    [SerializeField] private Sprite itemSprite;
    [SerializeField] private GameObject itemEffectPrefab;
    [SerializeField] private Color color;


    public string GetItemName() => itemName;

    public int GetTier() => tier;

    public Sprite GetItemSprite() => itemSprite;

    public string GetItemExplanation() => ItemExplanation;

    public GameObject GetItemEffectPrefab() => itemEffectPrefab; 

    public Color GetColor() => color;
}
