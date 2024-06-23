using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Holder", menuName = "Item Holder")]
public class ItemHolderSO : ScriptableObject
{
    #region Item List
    [SerializeField] private List<ItemSO> itemsTier1;
    [SerializeField] private List<ItemSO> itemsTier2;
    [SerializeField] private List<ItemSO> itemsTier3;
    [SerializeField] private List<ItemSO> itemsTier4;
    #endregion Item List

    private List<List<ItemSO>> itemTiers;

    private void OnEnable()
    {
        itemTiers = new List<List<ItemSO>> { itemsTier1, itemsTier2, itemsTier3, itemsTier4 };
    }

    public List<ItemSO> GetItemsByTier(int tier)
    {
        if (tier < 1 || tier > itemTiers.Count)
        {
            throw new ArgumentOutOfRangeException("Tier must be between 1 and 4");
        }

        return itemTiers[tier - 1];
    }
}