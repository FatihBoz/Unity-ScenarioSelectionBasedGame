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
    [SerializeField] private LootItemEffectType lootItemType; //if "None", means there is no item
    [SerializeField] private bool merchantEncounter = false;

    #endregion OTHER


    public bool HasEnemy { get => hasEnemy;}

    public EnemySO Enemy { get => enemy;}

    public LootItemEffectType LootItemType { get => lootItemType; }

    public List<ScenarioSO> GetNextOptions() => nextOptions;

    public string GetCardText() => cardText;

    public GameObject GetRewardPrefab() => rewardPrefab;

    public string GetNarratorText() => narratorText;

    public string GetBackText() => backText;

    public Sprite GetSprite() => image;

    public Location GetLocation() => location;

    public bool IsMerchantEncounter() => merchantEncounter;
}