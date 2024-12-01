using TMPro;
using UnityEngine;

public class UIPlayerAttributes : MonoBehaviour
{
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerHealth;
    public TextMeshProUGUI playerPower;
    public TextMeshProUGUI playerInspiration;

    public void UpdatePlayerAttributes(PlayerData playerData)
    {
        playerName.text = playerData.playername;
        playerHealth.text = "Health:"+playerData.health.ToString();
        playerPower.text = "Power:" + playerData.power.ToString();
        playerInspiration.text = "Inspiration:" + playerData.inspiration.ToString();
    }
}
