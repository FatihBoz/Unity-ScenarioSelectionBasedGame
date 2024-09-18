using UnityEngine;

[CreateAssetMenu(fileName = "new Loot Item",menuName = "Loot Item")]
public class LootItemSO : ItemSO
{
    [SerializeField] private int value;
    [SerializeField] private int itemCount;


    public int Value { get => value; set => this.value = value; }
    public int ItemCount { get => itemCount; set => itemCount = value; }
}
