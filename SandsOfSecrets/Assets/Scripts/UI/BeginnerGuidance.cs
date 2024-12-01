using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BeginnerGuidance : MonoBehaviour
{
    public Image darkOverlay;  
    public TextMeshProUGUI text;           
    public float displayTime = 2f;  
    private InfoPool pool;

    private bool isDisplayed = false;
    private float timer = 0f;
    public GameObject menu;


    public void BeginGuide()
    {
        
        menu.SetActive(true);
    }
    public void ShowTextWithDarkOverlay(string text)
    {
        
        if (!isDisplayed)
        {
            
            darkOverlay.gameObject.SetActive(true);
            this.text.gameObject.SetActive(true);
            this.text.text = text;  
            isDisplayed = true;
            timer = displayTime;
        }
    }

    void Update()
    {
        if (isDisplayed)
        {
            timer -= Time.deltaTime;

            
            if (timer > 0)
            {
                return;
            }

            
            if (Input.GetMouseButtonDown(0) || Input.touchCount > 0) // 判断玩家是否点击了屏幕
            {
                HideTextAndDarkOverlay();
            }
        }
    }

    void HideTextAndDarkOverlay()
    {
        
        darkOverlay.gameObject.SetActive(false);
        text.gameObject.SetActive(false);
        menu.SetActive(false);
        isDisplayed = false;
    }
}
