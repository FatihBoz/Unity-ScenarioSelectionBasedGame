using System;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
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
        //todo:Return to main menu
    }
}
