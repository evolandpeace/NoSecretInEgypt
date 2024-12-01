using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCreator : MonoBehaviour,IPointerClickHandler
{
    public int totalPoint;

    public int nowHp;

    public int nowAtk;

    public int nowIsp;

    public GameObject GoHp;
    public GameObject GoAtk;
    public GameObject GoIsp;
    public Sprite newBackgroundImage;
    public Image backgroundImage;
    public float fadeDuration = 2f;
    TextMeshProUGUI hp;
    TextMeshProUGUI atk;
    TextMeshProUGUI isp;
    public PlayerData pd;
    public GameObject menu;
    public GameObject button;
    private int count = 0;
    void Awake()
    {
        Init();
        menu.SetActive(false);
        button.SetActive(false);
        hp = GoHp.GetComponent<TextMeshProUGUI>();
        atk = GoAtk.GetComponent<TextMeshProUGUI>();
        isp = GoIsp.GetComponent<TextMeshProUGUI>();
        //OnCharacterCreate();
    }


    IEnumerator BackgroundFadeAndStartCreate()
    {

        float elapsedTime = 0f;
        Color startColor = backgroundImage.color;
        Color targetColor = new Color(startColor.r, startColor.g, startColor.b, 0f);


        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            backgroundImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }


        backgroundImage.sprite = newBackgroundImage;


        elapsedTime = 0f;
        targetColor = new Color(startColor.r, startColor.g, startColor.b, 1f);

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            backgroundImage.color = Color.Lerp(startColor, targetColor, elapsedTime / fadeDuration);
            yield return null;
        }

        button.SetActive(true);


    }
    private void Init()
    {
        nowHp = 1;
        nowAtk = 1;
        nowIsp = 1;
        pd = new PlayerData();
    }

    #region Button
    public void HpUp()
    {
        if(NotUpToLimit())nowHp++;
        
        hp.text = nowHp.ToString();
        
    }
    public void HpDown()
    {
        if(nowHp>1)nowHp--;
        hp.text = nowHp.ToString();
    }
    public void AtkUp()
    {
        if(NotUpToLimit())nowAtk++;
        atk.text = nowAtk.ToString();
    }
    public void AtkDown()
    {
        if(nowAtk>1)nowAtk--;
        atk.text = nowAtk.ToString();
    }
    public void IspUp()
    {
        if(NotUpToLimit())nowIsp++;
        isp.text = nowIsp.ToString();
    }
    public void IspDown()
    {
        if(nowIsp>1)nowIsp--;
        isp.text = nowIsp.ToString();
    }

    

    #endregion
    //the methods for buttons

    public void OnCharacterCreate()
    {
        menu.SetActive(true);
        int n = LevelManager.instance.infoPool.numOfCreateCharacter + 1;
        LevelManager.instance.infoPool.SetnumOfCreateCharacters(n);
        /*HpUp();
        AtkUp();
        IspUp();*/
    }

    public void OnCharacterCreateEnd()
    {
        menu.SetActive(false);
        pd.SetHealth(nowHp);
        pd.SetPower(nowAtk);
        pd.SetInspiration(nowIsp);
        LevelManager.instance.choice.OnChooseBornRoom();
        /*LevelManager.instance.game.SetActive(true);
        LevelManager.instance.OnPlayerBorn_L();*/
    }

    public PlayerData GetCharacterData()
    {
        return pd;
    }

    private bool NotUpToLimit()
    {
        return nowAtk + nowHp + nowIsp < totalPoint;

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        count++;
       if(count==1)
        StartCoroutine(BackgroundFadeAndStartCreate());
        
    }
}
