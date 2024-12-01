using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectBase : MonoBehaviour,IPointerClickHandler
{
    public int objectID;
    public string objectName;
    public string objectDescription;

    public Image objecticon;
    public ObjectData objectData;
    public ObjectType objectType;
    private List<ObjectEventBase> m_objectEvents;
    private bool isSelected;
    public RectTransform objectTransform;
    public void InitData(ObjectData objectData)
    {
        this.objectData = objectData;
        objectID = objectData.objectID;
        objectName = objectData.objectName;
        objectDescription = objectData.objectDescription;
        objecticon.sprite = objectData.objecticon;
        objectType = objectData.objectType;
        m_objectEvents = objectData.objectEvents;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(isSelected)
        {
            isSelected = false;
            foreach (var e in m_objectEvents)
            {
                LevelManager.instance.uIManager.uiNote.UpdateNoteUIOnNormalMode(e.eventDescription+"\n");
            }
            foreach(var e in m_objectEvents)
            {
                e.EventEffect();
                
            }
            if (objectType == ObjectType.Item|| objectType == ObjectType.Map)
            {
                if(objectType==ObjectType.Item)
                RoomManager.roomManagerInstance.player.AddPlayerBackPack(objectData);
                RoomBase room = RoomManager.roomManagerInstance.FindRoomWithPlayerPos();
                for (int i = 0; i < room.roomObjects.Count; i++)
                {
                    if (room.roomObjects[i].objectID == objectID)
                    {
                        room.roomObjects.RemoveAt(i);
                        if(room.roomObjects.Count==0)
                        {
                            RoomImage roomImage =room.GetComponentInChildren<RoomImage>();
                            roomImage.SwitchSpecialImage();
                            roomImage.exploredImage = roomImage.specialImage;
                        }
                        break;
                    }
                }
                this.gameObject.SetActive(false);
            }
        }
        else
        {
            isSelected=true;
            //todo:外观变化
        }
    }

}


