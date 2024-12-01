using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class RoomBase : MonoBehaviour, IPointerClickHandler
{
    public int roomID;
    [HideInInspector]public bool isKnowable = true;
    [HideInInspector]public bool isVisible=false;
    [HideInInspector]public bool isInteractive=false;
    [HideInInspector] public bool isSelected;
    [HideInInspector] public bool isEntered { get; private set; }
    [HideInInspector]public RoomState roomState;
    private int roomlength;
    private RoomImage m_roomImage;
    [HideInInspector]public RectTransform roomTransform;
    public string description;
    public RoomType roomType;
    

    
    public List<RoomEventBase> roomEvents;
    public List<RoomEventBase> m_roomEvents;
    public List<ObjectData> roomObjects;
    private List<GameObject> m_roomObjects=new();

    public void SetisEnteredTrue()
    {
        RoomImage roomImage = GetComponentInChildren<RoomImage>();
        isEntered = true;
        roomImage.SwitchImageOnPlayerEnter();
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (roomState)
        {
            case RoomState.Normal:

                if (isInteractive)
                {
                    if (isSelected)
                    {
                        isSelected = false;
                        RoomManager.roomManagerInstance.player.playerTransform.anchoredPosition = roomTransform.anchoredPosition;
                        switch (roomType)
                        {
                            case RoomType.Danger_Once:
                                int n = LevelManager.instance.infoPool.numOfEnterDanger_Once + 1;
                                LevelManager.instance.infoPool.SetnumOfEnterDanger_Once(n);
                                break;
                            case RoomType.Danger_Reusable:
                                int n1 = LevelManager.instance.infoPool.numOfEnterDangerReuseable + 1;
                                LevelManager.instance.infoPool.SetnumOfEnterDangerReuseable(n1);
                                break;
                        }

                    }
                    else
                    {
                        isSelected = true;
                        
                    }
                }
                break;
            case RoomState.Note:
                RoomBase room = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<RoomBase>();
                RoomManager.roomManagerInstance.OnNoteModeRoomBeClicked(room);
                break;
            case RoomState.ToBeSelected:
                SelectedRoomAndReborn(eventData);
                break;
        }
    }

    private static void SelectedRoomAndReborn(PointerEventData eventData)
    {
        RoomBase r1 = eventData.pointerCurrentRaycast.gameObject.GetComponentInParent<RoomBase>();
        RoomBase curR = RoomManager.roomManagerInstance.FindRoomWithPlayerPos();
        DeadBody db = (DeadBody)RoomManager.roomManagerInstance.playerDeadBody.objectEvents[0];
        db.roomid = r1.roomID;
        curR.roomObjects.Add(RoomManager.roomManagerInstance.playerDeadBody);
        RoomManager.roomManagerInstance.ChangeRoomsState(RoomState.Normal);
        Cursor.SetCursor(null,Vector2.zero,CursorMode.Auto);
        RoomManager.roomManagerInstance.RefreshAllRoomsImage();
        RoomManager.roomManagerInstance.InitRoomsData();
        RoomManager.roomManagerInstance.InitRoomsActive();
        LevelManager.instance.cover.SetActive(false);
        LevelManager.instance.game.SetActive(false);
        LevelManager.instance.creator.OnCharacterCreate();

    }

    public void OnPlayerPositionChanged(Vector2 newPosition)
    {

        if (Vector2.Distance(newPosition,roomTransform.anchoredPosition)<=roomlength)
        {
            OnPlayerCloseTo();
        }
        else 
        {
            OnPlayerFarFrom();
        }
    }


    private void OnPlayerCloseTo()
    {
        if (isKnowable)
        {
            isVisible = true;
            this.gameObject.SetActive(true);
            isInteractive = true;
        }
    }
    private void OnPlayerFarFrom()
    {
        isInteractive = false;       
    }
    public void OnPlayerEnter()
    {
        isEntered = true;
        m_roomImage.SwitchImageOnPlayerEnter();
        for (int i = 0; i < roomObjects.Count; i++)
        {
            //GameObject o = RoomManager.roomManagerInstance.poolTool.SpawnObject(RoomManager.roomManagerInstance.objprefab);
            GameObject o = RoomManager.roomManagerInstance.InstantiateObjectForRoom();
            ObjectBase ob = o.GetComponent<ObjectBase>();
            ob.InitData(roomObjects[i]);
            m_roomObjects.Add(o);
        }
        foreach (var e in m_roomEvents)
        {
            LevelManager.instance.uIManager.uiNote.UpdateNoteUIOnNormalMode(e.roomDescription);
            LevelManager.instance.uIManager.uiNote.UpdateNoteforDangerAndDoor(this);
            e.EventEffect();

        }
        RemoveOnlyOnceEvent();

    }

    public void RemoveOnlyOnceEvent()
    {
        for (int i = 0; i < m_roomEvents.Count; i++)
        {
            if (m_roomEvents[i].roomEventType == RoomEventType.OnlyOnce)
            {
                m_roomEvents.RemoveAt(i);
                i--;
            }
        }
    }

    public void OnPlayerLeave()
    {
        LevelManager.instance.uIManager.uiNote.UpdateNoteUIOnNormalMode("  ");

        foreach (var item in m_roomObjects)
        {
            //RoomManager.roomManagerInstance.poolTool.ReturnObjectToPool(item);
            
            Destroy(item);
        }
        
    }
    public void InitRoomEvent()
    {
        m_roomEvents = new List<RoomEventBase>(roomEvents);
        foreach(var e in m_roomEvents)
        {
            if(e) e.Init();
        }
    }
    private void Awake()
    {

        roomTransform = GetComponent<RectTransform>();
        roomlength = (int)(roomTransform.rect.width*roomTransform.localScale.x)+50;
        
        roomState = RoomState.Normal;
        m_roomImage = GetComponentInChildren<RoomImage>();

        

    }
    void Start()
    {

        if (!isVisible)
        {
            this.gameObject.SetActive(false);
        }
    }


}

public enum RoomType
{
    Normal,
    Danger_Once,
    Danger_Reusable,
    Door,
    Item,
    Message,
    HiddenRoad
}

public enum RoomState
{
    Normal,
    Note,
    ToBeSelected
}
