using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario", menuName = "Scenario")]
public class ScenarioSO : ScriptableObject
{
    #region TEXT

    [TextArea][SerializeField] private string narratorText;
    [TextArea][SerializeField] private string cardText;
    [TextArea][SerializeField] private string backText;

    #endregion TEXT

    #region OTHER

    [SerializeField] private Sprite image;
    [SerializeField] private List<ScenarioSO> nextOptions;
    [SerializeField] private GameObject rewardPrefab;

    #endregion OTHER

    public List<ScenarioSO> GetNextOptions() => nextOptions;

    public string GetCardText() => cardText;

    public GameObject GetRewardPrefab() => rewardPrefab;

    public string GetNarratorText() => narratorText;

    public string GetBackText() => backText;

    public Sprite GetSprite() => image;
}