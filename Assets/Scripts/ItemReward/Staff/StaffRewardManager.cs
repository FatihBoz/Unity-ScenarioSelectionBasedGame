using UnityEngine;

public class StaffRewardManager : MonoBehaviour
{
    private void RewardManager_OnScenarioSelected(ScenarioSO s)
    {
        IReward r = s.GetRewardPrefab().GetComponent<IReward>(); 
        r?.GetReward();
    }
    
    private void OnEnable()
    {
        EventManager<ScenarioSO>.Subscribe(EventKey.SELECT_SCENARIO, RewardManager_OnScenarioSelected);
    }

    private void OnDisable()
    {
        EventManager<ScenarioSO>.Unsubscribe(EventKey.SELECT_SCENARIO, RewardManager_OnScenarioSelected);
    }
}
