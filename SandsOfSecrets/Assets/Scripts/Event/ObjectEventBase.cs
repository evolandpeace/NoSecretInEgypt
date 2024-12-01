using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectEventBase : ScriptableObject
{
    public string eventDescription;
    public abstract void EventEffect();
}

