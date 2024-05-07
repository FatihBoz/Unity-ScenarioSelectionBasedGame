using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Image image;
    private ScenarioSO scenario;

    public void Display(ScenarioSO givenScenario)
    {
        this.scenario = givenScenario;
        text.text = scenario.GetText().text;
        image.sprite = scenario.GetSprite();
    }

    public void OnScenarioCardClick()
    {
        ScenarioManager.instance.SelectScenario(scenario);  
    }
}
