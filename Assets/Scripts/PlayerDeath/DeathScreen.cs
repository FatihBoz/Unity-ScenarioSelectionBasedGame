using DG.Tweening;
using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    [SerializeField] private string sceneNameToLoad;
    [SerializeField] private float sceneChangeDelay = 1f;
    [SerializeField] private Image sceneTransitionImagePrefab;
    [SerializeField] private Canvas canvas;

    private void OnEnable()
    {
        EventManager<PlayerAttributes>.Subscribe(EventKey.Player_Died,DeathScreen_OnPlayerDied);
    }

    private void OnDisable()
    {
        EventManager<PlayerAttributes>.Unsubscribe(EventKey.Player_Died, DeathScreen_OnPlayerDied);
    }

    private void DeathScreen_OnPlayerDied(PlayerAttributes attributes)
    {
        Image img = Instantiate(sceneTransitionImagePrefab, canvas.transform);
        img.DOFade(1, sceneChangeDelay * 2).OnComplete(ToDeathScene);
    }
     
    private async void ToDeathScene()
    {
        await Task.Delay(TimeSpan.FromSeconds(sceneChangeDelay));

        SceneLoader.Instance.ChangeScene(sceneNameToLoad);
    }
}
