using UnityEngine;
[CreateAssetMenu(fileName = "ShieldForDangerOnce", menuName = "ObjectData/ShieldForDangerOnce")]
public class ShieldForDangerOnce : ObjectEventBase
{
    public override void EventEffect()
    {
        RoomManager.roomManagerInstance.player.hasShieldForDangerOnce = true;
    }
}
