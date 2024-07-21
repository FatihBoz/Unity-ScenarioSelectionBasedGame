using UnityEngine;

public class DisplayScenarioCard : MonoBehaviour
{
    [SerializeField] private Transform scenarioCardHolder;
    [SerializeField] private GameObject scenarioCardPrefab;

    private void MakeInvisibleOfAllCards()
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
        MakeInvisibleOfAllCards();
        for (int i = 0; i < scenario.GetNextOptions().Count; i++)
        {
            scenarioCardHolder.GetChild(i).gameObject.SetActive(true);
            scenarioCardHolder.GetChild(i).GetComponent<ScenarioCard>().Display(scenario.GetNextOptions()[i]);
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