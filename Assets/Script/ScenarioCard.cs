using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI backText;
    [SerializeField] private Image image;
    private ScenarioSO scenario;

    private ScenarioCardAnimationController cardAnimationController;

    private void Awake()
    {
        cardAnimationController = GetComponent<ScenarioCardAnimationController>();
    }

    public void Display(ScenarioSO givenScenario)
    {
        this.scenario = givenScenario;
        text.text = scenario.GetCardText();
        backText.text = scenario.GetBackText();
        image.sprite = scenario.GetSprite();
    }

    public void OnScenarioCardClicked()
    {
        if (cardAnimationController.isFlipped)
        {
            //Delete edited card instead of turn back to default
            DestroyImmediate(this.gameObject);
            //This line does not rely on this game object and will be executed after destroying
            ScenarioManager.instance.SelectScenario(scenario);
            //!But this is not the best approach ig.Consider it as risky
            //TODO:Rearrange this method
        }
    }
}