using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType
{    
    LEVEL_START,
    LEVEL_END,
    PLAYER_DAMAGED,
    PLAYER_DIED,
    ITEM_PICKUP,
    HOTKEY_PRESSED
}

public static class EventManager
{
    private static Dictionary<EventType, System.Action> eventDictionary = new Dictionary<EventType, System.Action>();

    public static void Subscribe(EventType _type, System.Action _function)
    {
        if (!eventDictionary.ContainsKey(_type))
        {
            eventDictionary.Add(_type, null);
        }
        eventDictionary[_type] += _function;
    }

    public static void Unsubscribe(EventType _type, System.Action _function) 
    {
        if (eventDictionary.ContainsKey(_type) && eventDictionary[_type] != null)
        {
            eventDictionary[_type] -= _function;
        }
    }

    public static void Invoke(EventType _type)
    {
        eventDictionary[_type]?.Invoke();
    }

}
