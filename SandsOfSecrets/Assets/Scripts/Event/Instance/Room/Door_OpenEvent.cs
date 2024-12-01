using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Door_Open", menuName = "RoomEvent/Door_Open")]
public class Door_OpenEvent : RoomEventBase
{
    public int neededHealth;
    public int neededInspiration;
    public int neededPower;
    public int keyid;
    private bool isLock = true;
    public override void EventEffect()
    {
        List<RoomBase> rooms = RoomManager.roomManagerInstance.FindRoomsForDoor();
        Player player = RoomManager.roomManagerInstance.player;
        if ((player.playerData.health >= neededHealth
            && player.playerData.inspiration >= neededInspiration
            && player.playerData.power >= neededPower) && player.FindObject(keyid))
        {
            isLock = false;
            RoomBase room = RoomManager.roomManagerInstance.FindRoomWithPlayerPos();
            RoomImage roomimage = room.GetComponentInChildren<RoomImage>();
            roomimage.SwitchSpecialImage();

        }
        if (isLock)
        {
            foreach (var room in rooms)
            {
                if (room.roomTransform.anchoredPosition != player.lastPosition)
                {
                    room.isInteractive = false;
                }
            }
        }
    }

    public override void Init()
    {
        isLock = true;
    }
    private void OnEnable()
    {
        isLock = true;
    }
}
