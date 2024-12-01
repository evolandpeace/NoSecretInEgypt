using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.PlayerLoop;
using UnityEngine.Video;

public class StartMenuController : MonoBehaviour
{

    public GameObject player;
    
    public Image backgroundImage;


    public Sprite newBackgroundImage;


    public Button startButton;


    public float fadeDuration = 2f;

    private float timer;
    private bool backside;
    private VideoPlayer vp;
    void Start()
    {

        timer = 0;
        backside = false;
        vp = player.GetComponent<VideoPlayer>();
        /*startButton.onClick.AddListener(OnStartButtonClick);*/
    }


    public void OnStartButtonClick()
    {

        Debug.Log("B");
        if (!backside)
        {
            backside = true;
            player.SetActive(true);
            vp.Play();
        }

        if (backside && timer > 2)
        {
            SceneManager.LoadScene("SampleScene");  
            Debug.Log("T");
        }
        /*StartCoroutine(BackgroundFadeAndStartGame());*/
    }

    void Update()
    {
        if (backside && timer < 3)
        {
            timer += Time.deltaTime;
        }
    }

    IEnumerator BackgroundFadeAndStartGame()
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

        
        SceneManager.LoadScene("SampleScene");  
    }
}