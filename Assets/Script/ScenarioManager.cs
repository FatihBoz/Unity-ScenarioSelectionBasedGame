using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance;
    public static Action<ScenarioSO> OnScenarioSelected;

    [SerializeField] private ScenarioSO startingScenario;

    
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
