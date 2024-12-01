using System;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerData playerData;
    public RectTransform playerTransform { get; set; }
    public List<ObjectData> playerBackPack { get; set; }
    public ObjectData mapObject;
    public PlayerMap playerMap;
    public bool hasShieldForDangerOnce;
    public event Action<Vector2> OnPositionChanged;
    public event Action OnPlayerDied;
    public event Action OnPlayerDataChange;
    public event Action <ObjectData>OnPlayerBackPackAdd;
    public event Action <ObjectData>OnPlayerBackPackRemove;
    public event Action OnPlayerBackPackClear;
    public Vector2 currentPosition { get; private set; }
    public Vector2 lastPosition { get; private set; }

    public void AddPlayerBackPack(ObjectData obd)
    {
        playerBackPack.Add(obd);
        OnPlayerBackPackAdd?.Invoke(obd);
    }
    public void RemovePlayerBackPack(ObjectData obd)
    {
        playerBackPack.Remove(obd);
        OnPlayerBackPackRemove?.Invoke(obd);
    }

    public void ClearPlayerBackPack()
    {
        playerBackPack.Clear();
        OnPlayerBackPackClear?.Invoke();
    }
    public void OnPlayerBorn(RoomBase bornPoint, PlayerData playerData)
    {
        playerTransform.anchoredPosition = bornPoint.roomTransform.anchoredPosition;
        this.playerData = playerData;
    }

    public bool FindObject(int id)
    {
        foreach(var o in playerBackPack)
        {
            if(o.objectID==id)
                return true;
        }
        return false;
    }

    private void CheakPlayerMove()
    {

        if (playerTransform.anchoredPosition != currentPosition)
        {
            lastPosition = currentPosition;
            currentPosition = playerTransform.anchoredPosition;
            // 当位置发生变化时触发事件
            OnPositionChanged?.Invoke(currentPosition);
        }
    }



    private void InitPlayer()
    {
        playerTransform = GetComponent<RectTransform>();
        //PlayerBorn;
        //currentPosition = playerTransform.anchoredPosition;
        lastPosition = playerTransform.anchoredPosition;
        playerBackPack = new List<ObjectData>();

    }
    public void SetPlayerHealth(int h)
    {
        if(h<0&&hasShieldForDangerOnce)
        {
            hasShieldForDangerOnce = false;
            return;
        }
        playerData.SetHealth(Math.Max(0,h));
        OnPlayerDataChange?.Invoke();
        if (playerData.health<=0)
        {
            OnPlayerDied?.Invoke();
        }
    }
    public void SetPlayerPower(int p)
    {
        playerData.SetPower(Math.Max(0, p));
        OnPlayerDataChange?.Invoke();
    }
    public void SetPlayerInspiration(int i)
    {
        playerData.SetInspiration(Math.Max(0, i));
        OnPlayerDataChange?.Invoke();
    }
    private void Awake()
    {
        InitPlayer();
    }
    private void Start()
    {      

    }
    private void Update()
    {
        CheakPlayerMove();
    }

}

public class PlayerData
{
    public string playername;

    public int health { get; private set; }
    public int inspiration { get; private set; }
    public int power { get; private set; }

    public void SetHealth(int h)
    {
        health = h;

    }
    public void SetInspiration(int i)
    {
        inspiration = i;
        
    }
    public void SetPower(int p)
    {
        power = p;
        
    }
}

