using UnityEngine;
[CreateAssetMenu(fileName = "Item", menuName = "ObjectData/Item")]

public class Item : ObjectEventBase
{
    public int changeHealthValue;
    public int changePowerValue;
    public int changeInspirationValue;
    public override void EventEffect()
    {
        Player player = RoomManager.roomManagerInstance.player;
        player.SetPlayerHealth(player.playerData.health + changeHealthValue);
        player.SetPlayerInspiration(player.playerData.inspiration + changeInspirationValue);
        player.SetPlayerPower(player.playerData.power + changePowerValue);
        Debug.Log("Player health:" + player.playerData.health + "  Player inspiration:" + player.playerData.inspiration + "  Player power:" + player.playerData.power);
    }
}
