using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScenarioCard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private TextMeshProUGUI backText;
    [SerializeField] private Image image;

    public ScenarioSO Scenario { get; private set; }

    private ScenarioCardAnimationController cardAnimationController;

    private void Awake()
    {
        cardAnimationController = GetComponent<ScenarioCardAnimationController>();
    }

    public void Display(ScenarioSO givenScenario)
    {
        this.Scenario = givenScenario;
        text.text = Scenario.GetCardText();
        backText.text = Scenario.GetBackText();
        image.sprite = Scenario.GetSprite();
    }

    public void OnScenarioCardClicked()
    {
        if (cardAnimationController.isFlipped)
        {
            //Delete edited card instead of turn back to default
            DestroyImmediate(this.gameObject);
            //This line does not rely on this game object and will be executed after destroying
            ScenarioManager.instance.SelectScenario(Scenario);
            //!But this is not the best approach ig.Consider it as risky
            //todo:Rearrange this method
        }
    }
}