using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager instance;

    [SerializeField] private ScenarioSO startingScenario;

    
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        SelectScenario(startingScenario);   
    }


    public void SelectScenario(ScenarioSO scenario)
    {

        EventManager<ScenarioSO>.TriggerEvent(EventKey.SELECT_SCENARIO, scenario);
    }
}
