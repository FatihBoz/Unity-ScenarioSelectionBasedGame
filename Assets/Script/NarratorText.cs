using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class NarratorText : MonoBehaviour
{
    public float delayTime;
    public TextMeshProUGUI narratorText;

    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        ScenarioManager.OnScenarioSelected += NarratorText_OnScenarioSelected;
    }

    private void NarratorText_OnScenarioSelected(ScenarioSO scenario)
    {
        StartCoroutine(AnimateNarratorText(scenario));
    }

    private IEnumerator AnimateNarratorText(ScenarioSO scenario)
    {
        rt.DOAnchorPosY(128, 0.75f);
        yield return new WaitForSeconds(delayTime);
        narratorText.text = scenario.GetNarratorText();
        rt.DOAnchorPosY(-130, 0.75f);
    }

    private void OnDisable()
    {
        ScenarioManager.OnScenarioSelected -= NarratorText_OnScenarioSelected;
    }
}