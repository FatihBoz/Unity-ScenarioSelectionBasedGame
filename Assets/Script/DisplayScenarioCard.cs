using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayScenarioCard : MonoBehaviour
{

    [SerializeField] private List<ScenarioCard> scenarioCards;

    private void OnEnable()
    {
        ScenarioManager.OnScenarioSelected += DisplayScenarioCard_OnScenarioSelected;
    }


    private void MakeInvisibleOfAllCards()
    {
        foreach (var card in scenarioCards)
        {
            card.gameObject.SetActive(false);
        }
    }



    private void DisplayScenarioCard_OnScenarioSelected(ScenarioSO scenario)
    {
        MakeInvisibleOfAllCards();
        for (int i = 0; i < scenario.GetNextOptions().Count; i++)
        {
            scenarioCards[i].gameObject.SetActive(true);
            scenarioCards[i].Display(scenario);
        }
    }


    private void OnDisable()
    {
        ScenarioManager.OnScenarioSelected -= DisplayScenarioCard_OnScenarioSelected;
    }
}
