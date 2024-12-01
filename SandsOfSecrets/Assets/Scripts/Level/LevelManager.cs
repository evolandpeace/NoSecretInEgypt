using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public CTRLMap ctrlMap;
    public Player player;
    public UIManager uIManager;
    public PlayerCreator creator;
    public GameObject game;
    public GameObject cover;
    public ChooseBornRoom choice;
    public BeginnerGuidance beginnerGuidance;
    private PlayerData _playerData;
    public InfoPool infoPool;
    public int[] playerBornRoomID;
    public GameObject winPanel;
    public Texture2D cursorTexture;

    private void OnPlayerDied_L()
    {
        int nod = infoPool.numOfDeaths;
        infoPool.SetnumOfDeaths(nod+1);
        //choose room
        cover.SetActive(true);
        Debug.Log("Please choose a room as your secret");
        RoomManager.roomManagerInstance.ChangeRoomsState(RoomState.ToBeSelected);
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);
        RoomManager.roomManagerInstance.CollectRoomInfos();
        RoomBase room = RoomManager.roomManagerInstance.FindRoomWithPlayerPos();
        player.playerBackPack.Add(player.mapObject);
        foreach (var ob in player.playerBackPack)
        {
            room.roomObjects.Add(ob);
        }
        player.ClearPlayerBackPack();
        /*RoomManager.roomManagerInstance.InitRoomsData(); 
        RoomManager.roomManagerInstance.InitRoomsActive();
        game.SetActive(false);
        creator.OnCharacterCreate();*/

    }
    public void OnPlayerBorn_L()
    {
        _playerData = new PlayerData();
        _playerData = creator.GetCharacterData();
        int selectedid = choice.ReturnSelectedRoom();
        RoomBase bornroom = RoomManager.roomManagerInstance.FindRoomWithID(selectedid);
        ctrlMap.MoveMaptoCenter(bornroom);
        player.OnPlayerBorn(bornroom,_playerData);
        player.SetPlayerHealth(_playerData.health);
        player.SetPlayerPower(_playerData.power);
        player.SetPlayerInspiration(_playerData.inspiration);
    }

    private void UpdatePlayerAttributes_L()
    {
        uIManager.uiPlayerAttributes.UpdatePlayerAttributes(player.playerData);
    }


    


    private void Awake()
    {
        instance = this;
        player.OnPlayerDied += OnPlayerDied_L;
        player.OnPlayerDataChange += UpdatePlayerAttributes_L;
        infoPool = new InfoPool();

    }
    private void Start()
    {

        game.SetActive(false);
        if (creator)
            creator.OnCharacterCreate();
        else Debug.Log("creater is null");

        
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

            ctrlMap.MoveMapForPlayerCenter(player);
        }
    }
}


public class InfoPool
{
    public int numOfDeaths { get; private set; }
    public int numOfEnterGame { get; private set; }
    public int numOfChooseBornPlace { get; private set; }
    public int numOfCreateCharacter { get; private set; }
    public int numOfFindHidenRoad { get; private set; }
    public int numOfEnterDanger_Once { get; private set; }
    public int numOfEnterDangerReuseable { get; private set; }
    public InfoPool()
    {
        SetnumOfDeaths(0);
        SetnumOfEnterGame(0);
        SetnumOfChooseBornPlace(0);
        SetnumOfCreateCharacters(0);
        SetnumOfFindHidenRoad(0);
        SetnumOfEnterDanger_Once(0);
        SetnumOfEnterDangerReuseable(0);
    }
    public void SetnumOfDeaths(int numOfDeaths)
    {
        this.numOfDeaths = numOfDeaths;
        if(this.numOfDeaths==1)
        {
            LevelManager.instance.beginnerGuidance.BeginGuide();
            LevelManager.instance.beginnerGuidance.ShowTextWithDarkOverlay("Now you could choose a room as your \"secret\", " +
                " belike any dead bodies you have met, then a new adventurer will begin his story instead of you");
        }
    }
    public void SetnumOfEnterGame(int numOfEnterGame)
    {
        this.numOfEnterGame = numOfEnterGame;
        if(this.numOfEnterGame==1)
        {
            LevelManager.instance.beginnerGuidance.BeginGuide();
            LevelManager.instance.beginnerGuidance.ShowTextWithDarkOverlay("Move to a room around with you by double-click, " +
                "You could interact with anything in a room with double-click object in right, " +
                "then you could see what you got in bottom, and by using the \"Note\" with certain room," +
                " you could review what you have seen there.");
        }
    }
    public void SetnumOfChooseBornPlace(int numOfChooseBornPlace)
    {
        this.numOfChooseBornPlace = numOfChooseBornPlace;
        if(this.numOfChooseBornPlace==1)
        {
            LevelManager.instance.beginnerGuidance.BeginGuide();
            LevelManager.instance.beginnerGuidance.ShowTextWithDarkOverlay("You could choose a direction to start, for the one first to know this place, " +
                "I suggest you choose the north");
        }
    }
    public void SetnumOfCreateCharacters(int numOfCreateCharacter)
    {
        this.numOfCreateCharacter = numOfCreateCharacter;
        if(this.numOfCreateCharacter==1)
        {
            LevelManager.instance.beginnerGuidance.BeginGuide();
            LevelManager.instance.beginnerGuidance.ShowTextWithDarkOverlay("You will create your \"kid\" here, allot 10 points to three attributes, " +
                "then you could begin your adventure.");
        }
    }
    public void SetnumOfFindHidenRoad(int numOfFindHidenRoad)
    {
        this.numOfFindHidenRoad = numOfFindHidenRoad;
        if(this.numOfFindHidenRoad==1)
        {
            LevelManager.instance.beginnerGuidance.BeginGuide();
            LevelManager.instance.beginnerGuidance.ShowTextWithDarkOverlay("You have just get a usage of a hidden room," +
                " these rooms are special because only you can see them in your eye but you can't truly mark them in your map");
        }
    }
    public void SetnumOfEnterDanger_Once(int numOfEnterDanger_Once)
    {
        this.numOfEnterDanger_Once = numOfEnterDanger_Once;
        if(this.numOfEnterDanger_Once==1)
        {
            LevelManager.instance.beginnerGuidance.BeginGuide();
            LevelManager.instance.beginnerGuidance.ShowTextWithDarkOverlay("It seems like something attacked you furtively," +
                " but don't worry, next time you get here you won't be hurted.");
        }
    }
    public void SetnumOfEnterDangerReuseable(int numOfEnterDangerReuseable)
    {
        this.numOfEnterDangerReuseable = numOfEnterDangerReuseable;
        if(this.numOfEnterDangerReuseable==1)
        {
            LevelManager.instance.beginnerGuidance.BeginGuide();
            LevelManager.instance.beginnerGuidance.ShowTextWithDarkOverlay("This room has full hostility, " +
                "whenever you through here you need to pay the price.");
        }
    }
}
