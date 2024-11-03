using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item Holder", menuName = "Item Holder")]
public class ItemHolderSO : ScriptableObject
{
    [SerializeField] private List<ItemSO> itemHolder;

    private readonly int[] _tierBoundaries = { 3, 6, 9, int.MaxValue };


    public ItemSO SelectItem() //For staff items
    {
        var items = GetItemsByTier(DetermineTier());
        return items[UnityEngine.Random.Range(0, items.Count)];
    }


    public ItemSO SelectItem(LootItemEffectType itemEffectType) //for loot items
    {
        var items = GetItemsByTier(DetermineTier());

        List<ItemSO> tempList = new();
        //Iterate through each item in the filtered items list
        foreach (var item in items)
        {
            var lootItemEffect = item.ItemEffectPrefab.GetComponent<LootItemEffect>();
            // If the LootItemEffect component exists and its type matches the one we are looking for
            if (lootItemEffect != null && lootItemEffect.ItemEffectType == itemEffectType)
            {
                tempList.Add(item);
            }
        }

        // if there is at least one element in the list
        return tempList.Count > 0
            //return a random item
            ? tempList[UnityEngine.Random.Range(0, tempList.Count)]
            //if list is empty return null
            : null;
    }


    public List<ItemSO> GetItemsByTier(int tier)
    {
        List<ItemSO> tempList = itemHolder.Where(item => item.Tier == tier).ToList();

        return tempList;
    }

    int DetermineTier()
    {
        for (int i = 0; i < _tierBoundaries.Length; i++)
        {
            if (PlayerAttributes.Instance.Level <= _tierBoundaries[i])
                return i + 1;
        }

        throw new ArgumentOutOfRangeException("Player level must be non-negative");
    }
}

public enum LootItemEffectType
{
    None,
    Level,
    Healer,
    MaxHealth,
    MaxMana
}