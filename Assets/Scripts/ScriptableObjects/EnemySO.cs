using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy",menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    [Header("LIST")]
    [SerializeField] private List<ElementInteraction> interactions;

    [SerializeField] private List<SkillSO> skills;

    [SerializeField] private List<ItemSO> lootItemDrop;


    [Header("ATTRIBUTES")]
    [SerializeField] private int maxHp;

    [SerializeField] private int maxMana;

    [SerializeField] private float manaRegenPerSecond;

    [SerializeField] private float manaPercentageThresholdForCombo;


    [Header("UI")]
    [TextArea][SerializeField] private string enemyInfo;

    [SerializeField] private string _name;


    public bool IsWeakTo(Element element)
    {
        //!If at least one of the elements in the list matches the given parameter 
        //!and also its type is weakness, it means enemy is weak to given element
        return Interactions.Any(e => e.element == element && e.interactionType == ElementInteractionType.Weakness);
    }

    public bool IsResistantTo(Element element)
    {
        //!if its type is resistance, it means enemy is resistant to given element
        return Interactions.Any(e => e.element == element && e.interactionType == ElementInteractionType.Resistance);
    }


    public int MaxHp { get => maxHp;}

    public string Name { get => _name;}

    public List<SkillSO> Skills { get => skills; }

    public List<ElementInteraction> Interactions { get => interactions; }

    public string EnemyInfo { get => enemyInfo; }

    public List<ItemSO> LootItemDrop { get => lootItemDrop; }

    public int MaxMana { get => maxMana; set => maxMana = value; }

    public float ManaRegenPerSecond { get => manaRegenPerSecond;}

    public float ManaPercentageThresholdForCombo { get => manaPercentageThresholdForCombo;}
}


public enum ElementInteractionType
{
    None,
    Weakness,
    Resistance
}


[System.Serializable]
public struct ElementInteraction
{
    public Element element;
    public ElementInteractionType interactionType   ;
}