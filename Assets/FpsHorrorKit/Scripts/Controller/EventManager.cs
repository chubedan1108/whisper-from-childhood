using JetBrains.Annotations;
using System;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    //Event trigger door system
    public Action OnDoorSystemStart;
    public bool IsTriggerStart = false;
    public void TriggerStartLogic()
    {
        OnDoorSystemStart?.Invoke();
        IsTriggerStart = true;
    }
}
