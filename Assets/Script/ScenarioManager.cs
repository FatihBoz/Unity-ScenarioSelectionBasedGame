using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance;

    [SerializeField] private ScenarioSO startingScenario;

    public delegate void ScenarioSelectedHandler(ScenarioSO scenario);
    public static event ScenarioSelectedHandler OnScenarioSelected;


    private void Awake()
    {
        if (instance != null)
        {
            return;   
        }

        instance = this;
    }

    private void Start()
    {
        SelectScenario(startingScenario);   
    }


    public void SelectScenario(ScenarioSO scenario)
    {
        OnScenarioSelected?.Invoke(scenario);
    }
}
