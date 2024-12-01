using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class RoomEventBase : ScriptableObject
{

    public RoomEventType roomEventType;
    public string roomDescription;
    public abstract void EventEffect();
    public abstract void Init();
}

public enum RoomEventType
{
    OnlyOnce,
    Reuseable
}
