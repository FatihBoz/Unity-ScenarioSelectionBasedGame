using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public static BackgroundManager Instance { get; private set; }

    [SerializeField] private Sprite[] backgroundImages;
    [SerializeField] private Image backgroundImage;

    private readonly float visibilityTime = .75f; //Required time to make image visible
    private Location previousLocation;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void BackgroundManager_ScenarioSelected(ScenarioSO scenario)
    {
        //if place is changed
        if (previousLocation != scenario.GetLocation())
        {
            //Make current image invisible
            backgroundImage.color = new Color(1, 1, 1, 0);

            //Change sprite
            backgroundImage.sprite = backgroundImages[(int)scenario.GetLocation() - 1]; //there is an additional "NONE"

            //Make new image visible slowly
            backgroundImage.DOFade(1, visibilityTime);
            //Update previous location
            previousLocation = scenario.GetLocation();
        }
    }

    public Image GetBackgroundImage()
    {
        return backgroundImage;
    }


    private void OnEnable()
    {
        EventManager<ScenarioSO>.Subscribe(EventKey.SELECT_SCENARIO, BackgroundManager_ScenarioSelected);
    }

    private void OnDisable()
    {
        EventManager<ScenarioSO>.Unsubscribe(EventKey.SELECT_SCENARIO, BackgroundManager_ScenarioSelected);
    }
}

public enum Location
{
    NONE,
    FAMILY_HOUSE,
    FOREST_ENTRY,
    FOREST_RUINS
}