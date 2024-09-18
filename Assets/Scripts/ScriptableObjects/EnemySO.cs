using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy",menuName = "Enemy")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private List<ElementInteraction> interactions;

    [SerializeField] private List<SkillSO> skills;

    [SerializeField] private string _name;

    [SerializeField] private int maxHp;

    [TextArea]
    [SerializeField] private string enemyInfo;



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


    public int MaxHp { get => maxHp; set => maxHp = value; }

    public string Name { get => _name; set => _name = value; }

    public List<SkillSO> Skills { get => skills; set => skills = value; }

    public List<ElementInteraction> Interactions { get => interactions; set => interactions = value; }

    public string EnemyInfo { get => enemyInfo; set => enemyInfo = value; }
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