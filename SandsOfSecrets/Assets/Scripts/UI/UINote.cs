using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UINote : MonoBehaviour
{
    public Button noteButton;
    public TextMeshProUGUI roomInfo;
    private bool isButtonClick;
    private void UndateRoomInfoOnNoteMode(RoomBase room)
    {
        if (room.isEntered)
        {
            switch (room.roomType)
            {
                case RoomType.Normal:
                    roomInfo.text = "This is a normal room\n";
                    if (room.roomObjects.Count > 0)
                    {
                        foreach (var ob in room.roomObjects)
                        {

                            foreach (var obe in ob.objectEvents)
                            {
                                roomInfo.text += obe.eventDescription + "\n";
                            }
                        }
                    }
                    break;
                case RoomType.Door:
                    roomInfo.text = "This is a Door\n";
                    UpdateNoteforDangerAndDoor(room);
                    break;
                case RoomType.Danger_Once:
                    roomInfo.text = "This is a Danger Room\n";
                    UpdateNoteforDangerAndDoor(room);
                    break;
                case RoomType.Danger_Reusable:
                    roomInfo.text = "This is a Danger Room\n";
                    UpdateNoteforDangerAndDoor(room);
                    break;
                case RoomType.HiddenRoad:
                    roomInfo.text = "This is a Hidden Road\n";
                    break;
                case RoomType.Item:
                    roomInfo.text = "This is a item Room\n";
                    foreach(var item in room.roomObjects)
                    {
                        foreach (var obe in item.objectEvents)
                        {
                            roomInfo.text += obe.eventDescription + "\n";
                        }
                    }
                    break;
                case RoomType.Message:
                    roomInfo.text = "There's a body there\n ";
                    foreach (var item in room.roomObjects)
                    {
                        foreach (var obe in item.objectEvents)
                        {
                            roomInfo.text += obe.eventDescription + "\n";
                        }
                    }
                    break;
            }
        }
        else
        {
            roomInfo.text = "I don't know what is in it";
        }
    }
    public void UpdateNoteUIOnNormalMode(string text)
    {
        roomInfo.text = text + "\n";
    }
    public void UpdateNoteforDangerAndDoor(RoomBase room)
    {
        switch (room.roomType)
        {
            case RoomType.Danger_Once:
                if (room.m_roomEvents[0] != null)
                {
                    DangerEvent e = (DangerEvent)room.m_roomEvents[0];
                    if (e.reducedHealth != 0) roomInfo.text += "You lost " + e.reducedHealth + " health";
                    if (e.reducedPower != 0) roomInfo.text += "You lost " + e.reducedPower + " Power";
                    if (e.reducedInspiration!=0) roomInfo.text += "You lost " + e.reducedInspiration + " Inspiration";
                }
                break;
            case RoomType.Danger_Reusable:
                if (room.m_roomEvents[0] != null)
                {
                    DangerEvent e = (DangerEvent)room.m_roomEvents[0];
                    if (e.reducedHealth != 0) roomInfo.text += "You lost " + e.reducedHealth + " health";
                    if (e.reducedPower != 0) roomInfo.text += "You lost " + e.reducedPower + " Power";
                    if (e.reducedInspiration != 0) roomInfo.text += "You lost " + e.reducedInspiration + " Inspiration";
                }
                break;
            case RoomType.Door:
                if (room.m_roomEvents[0] != null)
                {
                    if (room.m_roomEvents[0] is DoorEvent)
                    {
                        DoorEvent e = (DoorEvent)room.m_roomEvents[0];
                    

                        if (e.neededHealth != 0) roomInfo.text += "You need " + e.neededHealth + " health to pass the door";
                        if (e.neededPower != 0) roomInfo.text += "You need " + e.neededPower + " Power to pass the door";
                        if (e.neededInspiration != 0) roomInfo.text += "You need " + e.neededInspiration + " Inspiration to pass the door";
                        if(e.keyid!=0) roomInfo.text += "You need a key to pass the door";
                    }
                    else
                    {
                        Door_OpenEvent e = (Door_OpenEvent)room.m_roomEvents[0];
                        if (e.neededHealth != 0) roomInfo.text += "You need " + e.neededHealth + " health to open the door";
                        if (e.neededPower != 0) roomInfo.text += "You need " + e.neededPower + " Power to open the door";
                        if (e.neededInspiration != 0) roomInfo.text += "You need " + e.neededInspiration + " Inspiration to open the door";
                        if (e.keyid != 0) roomInfo.text += "You need a key to open the door";
                    }
                }
                break;
        }

    }
    public void OnNoteButtonClick()
    {
        if(isButtonClick)
        {
            RoomManager.roomManagerInstance.ChangeRoomsState(RoomState.Normal);
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
            isButtonClick = false;
        }
        else
        {
            RoomManager.roomManagerInstance.ChangeRoomsState(RoomState.Note);
            Cursor.SetCursor(LevelManager.instance.cursorTexture, Vector2.zero, CursorMode.Auto);
            isButtonClick = true;
        }
    }
    private void Start()
    {
        RoomManager.roomManagerInstance.OnNoteModeRoomBeClicked += UndateRoomInfoOnNoteMode;
    }
}
