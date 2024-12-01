using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class RoomImage : MonoBehaviour
{
    [HideInInspector]public List<Sprite> images;
    [HideInInspector]private Image m_image;
    [HideInInspector]public Sprite unexploredImage;
    [HideInInspector]public Sprite exploredImage;
    [HideInInspector] public Sprite specialImage;
    public void SwitchImageOnPlayerEnter()
    {
        m_image.sprite = exploredImage;
    }

    public void RefreshImage()
    {
        m_image.sprite = unexploredImage;
    }

    public void SwitchSpecialImage()
    {
        m_image.sprite = specialImage;
    }

    private void Start()
    {
        m_image = GetComponent<Image>();
        m_image.sprite = unexploredImage;
    }

}
