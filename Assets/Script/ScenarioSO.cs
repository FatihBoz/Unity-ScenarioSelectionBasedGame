using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario", menuName = "Scenario")]
public class ScenarioSO : ScriptableObject
{
    #region TEXT
    [TextArea][SerializeField] private string narratorText;
    [TextArea][SerializeField] private string cardText;
    [TextArea][SerializeField] private string backText;
    #endregion

    //!Rearrange these background things
    #region Background Image 
    [SerializeField] private bool changeBackground;  
    [SerializeField] private Sprite backgroundImage;
    #endregion 

    #region OTHER
    [SerializeField] private Sprite image;
    [SerializeField] private List<ScenarioSO> nextOptions;
    [SerializeField] private GameObject rewardPrefab;
    #endregion

    public List<ScenarioSO> GetNextOptions()
    { return nextOptions; }

    public string GetCardText()
    { return cardText; }

    public GameObject GetRewardPrefab()
    { return rewardPrefab; }

    public string GetNarratorText()
    { return narratorText; }

    public string GetBackText()
    { return backText; }

    public Sprite GetSprite()
    { return image; }
}