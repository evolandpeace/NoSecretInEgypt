using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DG.Tweening;

public class IconHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RectTransform rectTransform;
    /*public Image borderImage;*/
    public Vector3 hoverScale = new Vector3(1.2f, 1.2f, 1.2f);
    private Vector3 originalScale;
    private Vector3 finalScale;
    public float duration = 0.3f; // 动画持续时间

    void Start()
    {
        finalScale = new Vector3(hoverScale.x * rectTransform.localScale.x,
            hoverScale.y * rectTransform.localScale.y, hoverScale.z * rectTransform.localScale.z);
        originalScale = rectTransform.localScale;
        /*borderImage.enabled = false;*/
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        rectTransform.DOScale(finalScale, duration).SetEase(Ease.OutBack);
        /*borderImage.enabled = true;*/
        /*borderImage.DOFade(1, duration); // 如果边框图片有透明度*/
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        rectTransform.DOScale(originalScale, duration).SetEase(Ease.OutBack);
        /*borderImage.DOFade(0, duration).OnComplete(() => borderImage.enabled = false); // 在动画结束后禁用边框*/
    }
}