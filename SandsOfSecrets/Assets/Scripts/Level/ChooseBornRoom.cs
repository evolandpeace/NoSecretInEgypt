using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBornRoom : MonoBehaviour
{
    public int northRoom;
    public int southRoom;
    public int westRoom;
    public int eastRoom;
    public GameObject menu;
    private int selectedRoom;

    public void NorthButton()
    {
        selectedRoom = northRoom;
        menu.SetActive(false);
        OnChooseBornRoomEnd();
    }

    public void SouthButton()
    {
        selectedRoom = southRoom;
        menu.SetActive(false);
        OnChooseBornRoomEnd();
    }

    public void WestButton()
    {
        selectedRoom = westRoom;
        menu.SetActive(false);
        OnChooseBornRoomEnd();
    }

    

    public void EastButton()
    {
        selectedRoom = eastRoom;
        menu.SetActive(false);
        OnChooseBornRoomEnd();
    }
    private static void OnChooseBornRoomEnd()
    {       
        LevelManager.instance.game.SetActive(true);
        LevelManager.instance.OnPlayerBorn_L();
        int n = LevelManager.instance.infoPool.numOfEnterGame + 1;
        LevelManager.instance.infoPool.SetnumOfEnterGame(n);
    }

    public void OnChooseBornRoom()
    {
        menu.SetActive(true);
        int n = LevelManager.instance.infoPool.numOfChooseBornPlace + 1;
        LevelManager.instance.infoPool.SetnumOfChooseBornPlace(n);
        
    }

    public int ReturnSelectedRoom()
    {
        return selectedRoom; 
    }
}
