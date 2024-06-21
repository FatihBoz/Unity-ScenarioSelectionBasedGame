using UnityEngine;

public class RewardManager : MonoBehaviour
{
    
    private void RewardManager_OnScenarioSelected(ScenarioSO s)
    {
        if (s.GetRewardPrefab() == null)
            return;

        IReward r = s.GetRewardPrefab().GetComponent<IReward>();
        r?.GetReward(); 
    }
    
    private void OnEnable()
    {
        ScenarioManager.OnScenarioSelected += RewardManager_OnScenarioSelected;
    }

    private void OnDisable()
    {
        ScenarioManager.OnScenarioSelected -= RewardManager_OnScenarioSelected;
    }
}
