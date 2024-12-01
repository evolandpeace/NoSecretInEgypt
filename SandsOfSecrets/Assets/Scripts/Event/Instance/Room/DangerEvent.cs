using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Danger", menuName = "RoomEvent/Danger")]
public class DangerEvent : RoomEventBase
{
    public int reducedHealth;
    public int reducedPower;
    public int reducedInspiration;
    public int HealthThreshold;
    public int PowerThreshold;
    public int InspirationThreshold;
    public override void EventEffect()
    {
        
        Player player = RoomManager.roomManagerInstance.player;
        if (player.playerData.inspiration < InspirationThreshold ||
            player.playerData.health < HealthThreshold ||
            player.playerData.power < PowerThreshold)
        {
            player.SetPlayerHealth(player.playerData.health - reducedHealth);
            player.SetPlayerInspiration(player.playerData.inspiration - reducedInspiration);
            player.SetPlayerPower(player.playerData.power - reducedPower);
        }
    }

    public override void Init()
    {
        
    }
}
