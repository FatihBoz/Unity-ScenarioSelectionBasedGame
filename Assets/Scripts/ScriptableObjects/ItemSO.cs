using UnityEngine;

public class ItemSO : ScriptableObject
{
    [TextArea]
    [SerializeField] protected string itemExplanation;

    [SerializeField] protected Sprite itemSprite;

    [SerializeField] protected string itemName; 
    
    [SerializeField] private int value;

    [SerializeField] protected ItemQuality itemQuality;


    public int Value { get => value; set => this.value = value; }

    public string ItemName { get => itemName; set => itemName = value; }

    public Sprite ItemSprite { get => itemSprite; }

    public string ItemExplanation { get => itemExplanation; set => itemExplanation = value; }

    public ItemQuality ItemQuality { get => itemQuality; set => itemQuality = value; }
}

public enum ItemQuality
{
    None,
    Common,
    Rare,
    Epic,
    Legendary
}
