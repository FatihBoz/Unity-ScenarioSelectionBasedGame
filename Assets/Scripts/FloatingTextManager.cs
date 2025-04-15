using System;
using System.Collections.Generic;
using UnityEngine;

public class FloatingTextManager : MonoBehaviour
{
    public static FloatingTextManager Instance { get; private set; }

    [Header("** Floating Text **")]
    [SerializeField] private List<FloatingTextKey> floatingTexts;
    [SerializeField] private FloatingText floatingTextPrefab;
    [SerializeField] private Canvas mainCanvas;

    private void Awake()
    {
        Instance = this;
    }


    public void InstantiateFloatingText(FloatingTextType type)
    {
        FloatingText text = Instantiate(floatingTextPrefab,mainCanvas.transform.position,Quaternion.identity, mainCanvas.transform);

        FloatingTextKey floatingTextKey = GetFloatingTextKey(type);

        text.SetText(floatingTextKey.Text,floatingTextKey.Color);
    }


    public FloatingTextKey GetFloatingTextKey(FloatingTextType type)
    {
        return floatingTexts.Find(floatingText => floatingText.Type == type);
    }
}


[Serializable]
public class FloatingTextKey
{
    [SerializeField] private FloatingTextType type;
    [SerializeField] private string text;
    [SerializeField] private Color color;

    public FloatingTextType Type => type;
    public string Text => text;
    public Color Color => color;
}



public enum FloatingTextType
{
    Max_Health_Increased,
    Health_Increased,
    Health_Decreased,
    Gold_Earned,
    Level_Up,
    Damage_Nullified
}