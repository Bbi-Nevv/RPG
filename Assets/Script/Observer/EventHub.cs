using UnityEngine;
using System;

public static class EventHub 
{
    public static event Action<int> OnHealthChange;
    public static void TriggerHealthChange(int newHealth)
    {
        OnHealthChange?.Invoke(newHealth);
    }

    public static event Action<float> OnPlayerStun;
    public static void TriggerPlayerStun(float duration)
    {
        OnPlayerStun?.Invoke(duration);
    }

}
