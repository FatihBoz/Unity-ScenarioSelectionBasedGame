using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Staff Holder", menuName = "Staff Holder")]
public class StaffHolderSO : ScriptableObject
{
    #region Item List
    [SerializeField] private List<StaffSO> itemsTier1;
    [SerializeField] private List<StaffSO> itemsTier2;
    [SerializeField] private List<StaffSO> itemsTier3;
    [SerializeField] private List<StaffSO> itemsTier4;
    #endregion Item List

    private List<List<StaffSO>> itemTiers;

    private void OnEnable()
    {
        itemTiers = new List<List<StaffSO>> { itemsTier1, itemsTier2, itemsTier3, itemsTier4 };
    }

    public List<StaffSO> GetItemsByTier(int tier)
    {
        if (tier < 1 || tier > itemTiers.Count)
        {
            throw new ArgumentOutOfRangeException("Tier must be between 1 and 4");
        }

        return itemTiers[tier - 1];
    }
}