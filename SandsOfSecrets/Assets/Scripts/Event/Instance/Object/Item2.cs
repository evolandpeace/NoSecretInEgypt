using UnityEngine;
[CreateAssetMenu(fileName = "Item2", menuName = "ObjectData/Item2")]

public class Item2 : ObjectEventBase
{
    public int HealthValue;
    public int PowerValue;
    public int InspirationValue;
    public override void EventEffect()
    {
        Player player = RoomManager.roomManagerInstance.player;
        if(player.playerData.health<HealthValue) player.SetPlayerHealth(HealthValue);
        if(player.playerData.power<PowerValue) player.SetPlayerPower(PowerValue);
        if (player.playerData.inspiration < InspirationValue) player.SetPlayerInspiration(InspirationValue);
    }


}
