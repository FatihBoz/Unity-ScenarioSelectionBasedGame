using UnityEngine;

public class DisplayScenarioCard : MonoBehaviour
{
    [SerializeField] private Transform scenarioCardHolder;
    [SerializeField] private GameObject scenarioCardPrefab;

    private void MakeAllCardsInvisible()
    {
        //Instantiate a card to replace the deleted one
        Instantiate(scenarioCardPrefab, scenarioCardHolder);

        for (int i = 0; i < scenarioCardHolder.childCount; i++)
        {
            scenarioCardHolder.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void DisplayScenarioCard_OnScenarioSelected(ScenarioSO scenario)
    {
        MakeAllCardsInvisible();

        //When player select to battle, there will be no next options
        //Instead when winner of the battle is determined, next options will be displayed
        if (scenario.HasEnemy)
        {
            return;
        }

        DisplayNextOptions(scenario);
    }


    private void DisplayNextOptions(ScenarioSO scenario)
    {
        for (int i = 0; i < scenario.GetNextOptions().Count; i++)
        {
            scenarioCardHolder.GetChild(i).gameObject.SetActive(true);
            scenarioCardHolder.GetChild(i).GetComponentInChildren<ScenarioCard>().Display(scenario.GetNextOptions()[i]);
        }
    }


    private void OnEnable()
    {
        EventManager<ScenarioSO>.Subscribe(EventKey.SELECT_SCENARIO, DisplayScenarioCard_OnScenarioSelected);
    }

    private void OnDisable()
    {
        EventManager<ScenarioSO>.Unsubscribe(EventKey.SELECT_SCENARIO, DisplayScenarioCard_OnScenarioSelected);
    }


}