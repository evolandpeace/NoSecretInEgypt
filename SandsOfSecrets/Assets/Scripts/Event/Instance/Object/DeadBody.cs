using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DeadBody", menuName = "ObjectData/DeadBody")]
public class DeadBody : ObjectEventBase
{
    public int roomid;
    public override void EventEffect()
    {
        RoomBase room = RoomManager.roomManagerInstance.FindRoomWithID(roomid);
        
        if (room)
        {
            room.SetisEnteredTrue();
            room.isKnowable = true;
            room.isVisible = true;
            room.gameObject.SetActive(true);
            room.RemoveOnlyOnceEvent();
            if(room.roomType==RoomType.HiddenRoad)
            {
                int n = LevelManager.instance.infoPool.numOfFindHidenRoad+1;
                LevelManager.instance.infoPool.SetnumOfFindHidenRoad(n);
            }
            room.OnPlayerPositionChanged(RoomManager.roomManagerInstance.player.playerTransform.anchoredPosition);
        }
    }
}
