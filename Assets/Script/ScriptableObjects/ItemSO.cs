using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private string itemName;
    [SerializeField] private int tier;
    [SerializeField] private Sprite itemSprite;
    [TextArea][SerializeField] private string ItemExplanation;

    public string GetItemName() => itemName;

    public int GetTier() => tier;

    public Sprite GetItemSprite() => itemSprite;

    public string GetItemExplanation() => ItemExplanation;
}
