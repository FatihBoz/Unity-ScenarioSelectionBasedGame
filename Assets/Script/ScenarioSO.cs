using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "New Scenario" , menuName = "Scenario")]
public class ScenarioSO : ScriptableObject
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Sprite image;
    [SerializeField] private List<ScenarioSO> nextOoptions;
    [SerializeField] private Reward reward;

    public List<ScenarioSO> GetNextOptions() { return nextOoptions; }

    public Reward GetReward() { return reward;}

    public TextMeshProUGUI GetText() { return text;}

    public Sprite GetSprite() { return image;}
}
