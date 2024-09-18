using System;
using System.Collections.Generic;


public class ElementalStrength
{
    private readonly Dictionary<Element,Element> elementStrengths = new();

    public ElementalStrength()
    {
        //determines which element is stronger against which
        elementStrengths.Add(Element.Fire, Element.Wind | Element.Wild);       // Fire over both Wind and Wild

        elementStrengths.Add(Element.Water, Element.Fire);

        elementStrengths.Add(Element.Wind, Element.Earth | Element.Physical);

        elementStrengths.Add(Element.Earth, Element.Water | Element.Fire);
    }


    public bool IsElementStronger(Element attackingElement, Element defendingElement)
    {
        // if attacking element is strong against at least one element
        if (elementStrengths.ContainsKey(attackingElement))
        {
            //returns true if the defending element exists in the elements that the attacking element is strong against
            return (elementStrengths[attackingElement] & defendingElement) != Element.None;
        }

        return false;
    }
}


[Flags]
public enum Element
{
    None = 0,
    Fire = 1 << 0,
    Water = 1 << 1,
    Wind = 1 << 2,
    Earth = 1 << 3,
    Physical = 1 << 4,
    Wild = 1 << 5
}
