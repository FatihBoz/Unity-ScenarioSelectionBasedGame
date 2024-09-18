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
    [SerializeField] private Location location;
    [SerializeField] private bool hasEnemy;
    [SerializeField] private EnemySO enemy;

    #endregion OTHER


    public bool HasEnemy { get => hasEnemy; set => hasEnemy = value; }
    public EnemySO Enemy { get => enemy; set => enemy = value; }

    public List<ScenarioSO> GetNextOptions() => nextOptions;

    public string GetCardText() => cardText;

    public GameObject GetRewardPrefab() => rewardPrefab;

    public string GetNarratorText() => narratorText;

    public string GetBackText() => backText;

    public Sprite GetSprite() => image;

    public Location GetLocation() => location;
}