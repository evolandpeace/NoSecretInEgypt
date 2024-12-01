using UnityEngine;
using UnityEngine.UI;

public class UIRoomObject : MonoBehaviour
{
    private ScrollRect m_scrollRect;
    private RectTransform m_contentTransform;
    private void UpdateObjectLayout(GameObject gb)
    {
        gb.transform.SetParent(m_contentTransform.transform,false);
        
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        RoomManager.roomManagerInstance.OnInstantiateObjectForRoom += UpdateObjectLayout;
        m_scrollRect = GetComponentInChildren<ScrollRect>();
        m_contentTransform = m_scrollRect.content;
    }
}
