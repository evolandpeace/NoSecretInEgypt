using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "Object", menuName = "ObjectData/Object")]

public class ObjectData : ScriptableObject
{
    public int objectID;
    public string objectName;
    public string objectDescription;
    public ObjectType objectType;
    public Sprite objecticon;

    public List<ObjectEventBase> objectEvents; 
}
public enum ObjectType
{
    DeadBody,
    Item,
    Map
}