using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class RoomManager : MonoBehaviour
{
    public RoomBase[] rooms;
    public Player player;
    public GameObject objprefab;
    public ObjectData playerDeadBody;
    public static RoomManager roomManagerInstance;
    public Action<RoomBase> OnNoteModeRoomBeClicked;
    public Action<GameObject> OnInstantiateObjectForRoom;
    private Sprite unexploredImage;
    private Sprite[] dangerImages;
    private Sprite messageImage;
    private Sprite[] itemImages;
    private Sprite[] doorImages;
    private Sprite doorLock;
    private Sprite doorUnlock;
    private Sprite itemFull;
    private Sprite itemNull;



    public List<RoomBase> FindRoomsForDoor()
    {
        RoomBase playerroom = FindRoomWithPlayerPos();
        List<RoomBase> roomBases = new List<RoomBase>();
        foreach (var room in rooms)
        {
            if (Vector2.Distance(room.roomTransform.anchoredPosition, playerroom.roomTransform.anchoredPosition) == 200)
            {
                roomBases.Add(room);
            }

        }
        return roomBases;
    }
    private void LoadImage()
    {
        unexploredImage = Resources.Load<Sprite>("Sprites/Room/Normal/未探索房间");

        dangerImages = Resources.LoadAll<Sprite>("Sprites/Room/Danger");
        doorImages = Resources.LoadAll<Sprite>("Sprites/Room/Door");
        itemImages = Resources.LoadAll<Sprite>("Sprites/Room/Item");
        messageImage = Resources.Load<Sprite>("Sprites/Room/Message/信息房间");
        doorLock =  Resources.Load<Sprite>("Sprites/Room/Door/未开门房间");
        doorUnlock = Resources.Load<Sprite>("Sprites/Room/Door/开门房间");
        itemFull = Resources.Load<Sprite>("Sprites/Room/Item/问号房间2");
        itemNull = Resources.Load<Sprite>("Sprites/Room/Item/问号房间1");
    }
    private void OnPlayerMove(Vector2 pos)
    {
        foreach(var room in rooms)
        {
            room.OnPlayerPositionChanged(pos);
        }
        RoomBase lastRoom = FindRoomWithPlayerLastPos();
        RoomBase curRoom = FindRoomWithPlayerPos();
        if (lastRoom != null)
        {
            lastRoom.OnPlayerLeave();
        }
        if(curRoom!=null)
        {
            curRoom.OnPlayerEnter();
        }
    }
    public RoomBase FindRoomWithPlayerLastPos()
    {
        foreach (RoomBase room in rooms)
        {
            if (player.lastPosition == room.roomTransform.anchoredPosition)
            {
                return room;
            }
        }
        return null;
    }
    public RoomBase FindRoomWithPlayerPos()
    {
        foreach (RoomBase room in rooms) 
        {
            if(player.playerTransform.anchoredPosition==room.roomTransform.anchoredPosition)
            {
                return room;
            }
        }
        return null;
    }
    public RoomBase FindRoomWithID(int id)
    {
        foreach (RoomBase room in rooms)
        {
            if(room.roomID==id)
            return room;
        }
        return null;
    }

    public void InitRoomsActive()
    {

        foreach (RoomBase room in rooms)
        {
            if(!room.isVisible)room.gameObject.SetActive(false);
            else room.gameObject.SetActive(true);
        }
    }
    public void InitRoomsData()
    {
        foreach (var room in rooms)
        {
            room.isVisible = false;
            room.isInteractive = false;
            room.isSelected = false;
            if(room.roomType==RoomType.HiddenRoad)
            {
                room.isKnowable = false;
            }
            else room.isKnowable = true;
            room.InitRoomEvent();
        }
    }

    public void InitRoomImage()
    {
        foreach(var room in rooms)
        {
            RoomImage roomImage = room.GetComponentInChildren<RoomImage>();
            roomImage.unexploredImage = unexploredImage;
            switch (room.roomType)
            {
                case RoomType.Normal:
                    roomImage.images.Add(unexploredImage);
                    roomImage.exploredImage = unexploredImage;
                    break;
                case RoomType.Danger_Once:
                    roomImage.images.Add(dangerImages[0]);
                    roomImage.exploredImage = dangerImages[0];
                    break;
                case RoomType.Danger_Reusable:
                    roomImage.images.Add(dangerImages[1]);
                    roomImage.exploredImage = dangerImages[1];
                    break;
                case RoomType.Door:
                    roomImage.images.AddRange(doorImages);
                    roomImage.exploredImage = doorLock;
                    roomImage.specialImage = doorUnlock;
                    break;
                case RoomType.Item:
                    roomImage.images.AddRange(itemImages);
                    roomImage.exploredImage = itemFull;
                    roomImage.specialImage = itemNull;
                    break;
                case RoomType.Message:
                    roomImage.images.Add(messageImage);
                    roomImage.exploredImage = messageImage;
                    roomImage.specialImage = messageImage;
                    break;
                case RoomType.HiddenRoad:
                    roomImage.images.Add(unexploredImage);
                    roomImage.exploredImage = unexploredImage;
                    break;
            }

        }
    }
    public void CollectRoomInfos()
    {
        foreach (RoomBase room in rooms)
        {
            if(room.roomType!=RoomType.HiddenRoad)
            {
                RoomData roomData=new RoomData();
                roomData.roomID = room.roomID;
                roomData.isVisible = room.isVisible;
                roomData.isKnowable=room.isKnowable;
                roomData.isEntered = room.isEntered;
                player.playerMap.roomdatas.Add(roomData);
            }
        }
    }

    public void RefreshAllRoomsImage()
    {
        foreach(RoomBase room in rooms)
        {
            RoomImage roomImage = room.GetComponentInChildren<RoomImage>();
            roomImage.RefreshImage();
        }
    }

    public void ChangeRoomsState(RoomState roomState)
    {
        foreach(var room in rooms)
        {
            room.roomState = roomState;
        }
    }

    public GameObject InstantiateObjectForRoom()
    {
        GameObject go = Instantiate(objprefab);
        OnInstantiateObjectForRoom?.Invoke(go);
        return go;
    }
    private void Awake()
    {
        roomManagerInstance = this;
        rooms = GetComponentsInChildren<RoomBase>();
        player.OnPositionChanged += OnPlayerMove;
        InitRoomsData();
        LoadImage();
        InitRoomImage();
    }



    void Start()
    {
        
    }


    void Update()
    {
        
    }
}

public class RoomData
{
    public int roomID;
    public bool isKnowable;
    public bool isVisible;
    public bool isEntered;
}
