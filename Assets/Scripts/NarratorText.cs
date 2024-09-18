using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class NarratorText : MonoBehaviour
{
    #region PUBLIC
    public float delayTime;
    public TextMeshProUGUI narratorText;
    public float defaultPosY;
    public float readyPosY;
    #endregion

    private RectTransform rt;

    private void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    private void NarratorText_OnScenarioSelected(ScenarioSO scenario)
    {
        StartCoroutine(AnimateNarratorText(scenario));
    }

    private IEnumerator AnimateNarratorText(ScenarioSO scenario)
    {
        rt.DOAnchorPosY(defaultPosY, 0.75f);
        yield return new WaitForSeconds(delayTime);
        narratorText.text = scenario.GetNarratorText();
        rt.DOAnchorPosY(readyPosY, 0.75f);
    }

    private void OnEnable()
    {
        EventManager<ScenarioSO>.Subscribe(EventKey.SELECT_SCENARIO, NarratorText_OnScenarioSelected);
    }

    private void OnDisable()
    {
        EventManager<ScenarioSO>.Unsubscribe(EventKey.SELECT_SCENARIO, NarratorText_OnScenarioSelected);
    }
}