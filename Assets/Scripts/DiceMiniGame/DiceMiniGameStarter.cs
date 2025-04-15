using UnityEngine;

public class DiceMiniGameStarter : MonoBehaviour
{
    [SerializeField] private Canvas miniGameCanvas;
    [SerializeField] private DiceMiniGame diceMiniGamePrefab;

    private void OnEnable()
    {
        EventManager<ScenarioSO>.Subscribe(EventKey.SELECT_SCENARIO, DiceMiniGameStarter_ScenarioSelected);
    }

    private void DiceMiniGameStarter_ScenarioSelected(ScenarioSO so)
    {
        if (so.IsMerchantEncounter())
        {
            DiceMiniGame miniGame = Instantiate(diceMiniGamePrefab, miniGameCanvas.transform);

            miniGame.SetCanvas(miniGameCanvas);
        }
        
    }

    private void OnDisable()
    {
        EventManager<ScenarioSO>.Unsubscribe(EventKey.SELECT_SCENARIO, DiceMiniGameStarter_ScenarioSelected);
    }
}
