using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [Header("TEXT")]
    [TextArea][SerializeField] private string itemExplanation;

    [SerializeField] private string itemName;


    [Header("INTEGER VALUES")]
    [SerializeField] private int tier;


    [Header("OTHER")]
    [SerializeField] private Sprite itemSprite;

    [SerializeField] private GameObject itemEffectPrefab;

    [SerializeField] private ItemType itemType;



    public string ItemName { get => itemName; }

    public Sprite ItemSprite { get => itemSprite; }

    public string ItemExplanation { get => itemExplanation; }

    public int Tier { get => tier; }

    public GameObject ItemEffectPrefab { get => itemEffectPrefab; }

    public ItemType ItemType { get => itemType; }

    public ItemQuality ItemQuality
    {
        get
        {
            // Match tier and ItemQuality
            return (ItemQuality)Mathf.Clamp(tier, 0, System.Enum.GetValues(typeof(ItemQuality)).Length - 1);
        }
    }
}

public enum ItemQuality
{
    None,
    Common,
    Rare,
    Epic,
    Legendary
}

public enum ItemType
{
    None,
    LootItem,
    Staff
}

