using UnityEngine;

public abstract class ItemSO : ScriptableObject
{
    [TextArea]
    [SerializeField] protected string itemExplanation;

    [SerializeField] protected Sprite itemSprite;

    [SerializeField] protected string itemName;


    public string ItemName { get => itemName; set => itemName = value; }
    public Sprite ItemSprite { get => itemSprite; }
    public string ItemExplanation { get => itemExplanation; set => itemExplanation = value; }
}
