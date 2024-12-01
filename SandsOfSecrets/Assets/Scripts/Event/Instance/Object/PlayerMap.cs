using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[CreateAssetMenu(fileName = "PlayerMap", menuName = "ObjectData/PlayerMap")]
public class PlayerMap : ObjectEventBase
{
    public List<RoomData> roomdatas=new();
    private RoomBase[] rooms;
    public override void EventEffect()
    {
        Debug.Log("i am map");
        rooms=RoomManager.roomManagerInstance.rooms;
        foreach(var roomdata in roomdatas)
        {
            
            int id = roomdata.roomID;
            
            RoomBase room = rooms.FirstOrDefault(r => r.roomID == id);
            if(!room.isVisible&&roomdata.isVisible)
            room.isVisible=roomdata.isVisible;
            if(!room.isEntered&&roomdata.isEntered)
            room.SetisEnteredTrue();
        }
        RoomManager.roomManagerInstance.InitRoomsActive();
        roomdatas.Clear();
    }

}
