using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Destination", menuName = "RoomEvent/Destination")]
public class Destination : RoomEventBase
{
    public List<int> neededItemID;
    public override void EventEffect()
    {
        int count = 0;
        Player player = RoomManager.roomManagerInstance.player;
        foreach(var id in neededItemID)
        {
            if (player.FindObject(id)) count++;
        }
        if(count==neededItemID.Count)
        {
            LevelManager.instance.winPanel.SetActive(true);
        }
    }

    public override void Init()
    {
        
    }
}
