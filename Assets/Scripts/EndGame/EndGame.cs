using System;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject endGamePanel;



    private void OnEnable()
    {
        EventManager<IReward>.Subscribe(EventKey.Game_End, ShowEndGamePanel);
    }

    private void OnDisable()
    {
        EventManager<IReward>.Unsubscribe(EventKey.Game_End, ShowEndGamePanel);
    }

    private void ShowEndGamePanel(IReward reward)
    {
        endGamePanel.SetActive(true);
    }
}
