using System;
using System.Collections.Generic;


public static class EventManager<TEventArgs>
{
    private static Dictionary<EventKey, Action<TEventArgs>> Events = new();

    public static void Subscribe(EventKey eventType, Action<TEventArgs> action)
    {
        if (Events.ContainsKey(eventType))
        {
            Events[eventType] += action;
        }
        else
        {
            Events[eventType] = action;
        }
    }

    public static void Unsubscribe(EventKey eventType, Action<TEventArgs> action)
    {
        if (Events.ContainsKey(eventType))
        {
            Events[eventType] -= action;
            if (Events[eventType] == null)
            {
                Events.Remove(eventType);
            }
        }
    }

    public static void TriggerEvent(EventKey eventType,TEventArgs eventArgs)
    {
        if (Events.ContainsKey(eventType))
        {
            Events[eventType]?.Invoke(eventArgs);
        }
    }
}

public enum EventKey
{
    SELECT_SCENARIO,
    STAFF_FOUND,
    LOOT_ITEM_FOUND,
    STAFF_EQUIPPED,
    INVENTORY_SETUP,
    Staff_Inventory_Update,
    LootItem_Inventory_Update,
    HEALTH_DECREASED,
    HEALTH_INCREASED,
    HEALTH_UI_CHANGED,
    MiniGameCombat_Enemy_TakeDamage,
    MiniGameCombat_Player_TakeDamage,
    ENEMY_FOUND
}