using System;
using UnityEngine;

public class ItemReward : MonoBehaviour, IReward
{
    [SerializeField] private ItemHolderSO itemHolder;
    private readonly int[] _tierBoundaries = { 3, 6, 9, int.MaxValue };

    public void GetReward()
    {
        EventManager<ItemSO>.TriggerEvent(EventKey.ITEM_FOUND, SelectItem());
    }

    private ItemSO SelectItem()
    {
        var items = itemHolder.GetItemsByTier(DetermineTier());
        return items[UnityEngine.Random.Range(0, items.Count)];
    }

    private int DetermineTier()
    {
        for (int i = 0; i < _tierBoundaries.Length; i++)
        {
            if (PlayerAttributes.Instance.Level <= _tierBoundaries[i])
                return i + 1;
        }

        throw new ArgumentOutOfRangeException("Player level must be non-negative");
    }
}