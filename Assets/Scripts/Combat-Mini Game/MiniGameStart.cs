using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameStart : MonoBehaviour
{
    [Header("Float Values")]
    [SerializeField] private float timeToBeVisibleAfterAnimation = 1f;

    [SerializeField] private float scrollTextVisibilityTime = .5f;

    [SerializeField] private float blackScreenVisibilityTime = .5f;


    [Header("Starting Panel UI")]
    [SerializeField] private Button combatStartButton;

    [SerializeField] private TextMeshProUGUI enemyNameText;

    [SerializeField] private GameObject scroll;

    [SerializeField] private Image BackgroundImageSprite;


    [Header("Other")]
    [SerializeField] private GameObject canvas;

    [SerializeField] private GameObject startingPanel;

    [SerializeField] private GameObject miniGamePrefab;

    [SerializeField] private Image blackScreen;


    private EnemySO enemy;
    private TextMeshProUGUI scrollText;



    private void Awake()
    {
        scrollText = scroll.GetComponentInChildren<TextMeshProUGUI>();

        combatStartButton.onClick.AddListener(OnCombatStartButtonClicked);
    }

    private void OnCombatStartButtonClicked()
    {
        GameObject obj = Instantiate(miniGamePrefab, canvas.transform);

        Image image = Instantiate(blackScreen, canvas.transform);
        //make black screen visible
        image.DOFade(1f, blackScreenVisibilityTime)
            .OnComplete(() => ActivateMiniGame(image,obj));
    }

    void ActivateMiniGame(Image image,GameObject miniGame)
    {
        miniGame.SetActive(true);   
        //assign enemy
        EventManager<EnemySO>.TriggerEvent(EventKey.ENEMY_FOUND, enemy);

        startingPanel.SetActive(false);
        //Make black screen invisible
        image.DOFade(0f, blackScreenVisibilityTime)
            .OnComplete(() => Destroy(image));
    }


    private void MiniGameStart_OnScenarioSelected(ScenarioSO scenario)
    {
        if (scenario.HasEnemy)
        {
            enemy = scenario.Enemy;
            enemyNameText.text = enemy.Name;
            scrollText.text = enemy.EnemyInfo;
            //Change background image of combat starting screen
            BackgroundImageSprite.sprite = BackgroundManager.Instance.GetBackgroundImage().sprite;

            startingPanel.SetActive(true);
            StartCoroutine(ShowTextAfterDelay());
        }
    }

    private IEnumerator ShowTextAfterDelay()
    {
        yield return new WaitForSeconds(timeToBeVisibleAfterAnimation);
        scrollText.DOFade(1f, scrollTextVisibilityTime);
    }


    private void OnEnable()
    {
        EventManager<ScenarioSO>.Subscribe(EventKey.SELECT_SCENARIO, MiniGameStart_OnScenarioSelected);
    }

    private void OnDisable() 
    {
        EventManager<ScenarioSO>.Unsubscribe(EventKey.SELECT_SCENARIO, MiniGameStart_OnScenarioSelected);
    }
}
