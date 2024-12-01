using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class BackPackObject : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public TextMeshProUGUI objectDescription;
    public ObjectData objectData;
    public void OnPointerEnter(PointerEventData eventData)
    {
        objectDescription.text = objectData.objectDescription;
        objectDescription.gameObject.SetActive(true);
        RectTransform parentRectTransform = objectDescription.transform.parent.GetComponent<RectTransform>();
        Vector2 localPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(parentRectTransform, eventData.position, eventData.enterEventCamera, out localPosition);
        objectDescription.rectTransform.anchoredPosition = localPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        objectDescription.text = null;
        objectDescription.gameObject.SetActive(false);
    }

}
