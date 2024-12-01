using UnityEngine;
using UnityEngine.EventSystems;

public class CTRLMap : MonoBehaviour, IDragHandler
{
    
    
    public RectTransform m_transform;
    private float windowWidth_UD=100f;
    private float windowWidth_LR =100f;
    public RectTransform centerRoom;


    public void OnDrag(PointerEventData eventData)
    {
        m_transform.anchoredPosition += eventData.delta;
        //BoundaryCheak();
    }
    public void MoveMaptoCenter(RoomBase bornroom)
    {
        m_transform.anchoredPosition = new Vector2(0, 2170);
        RectTransform bornTransform = bornroom.roomTransform;
        Vector2 offsets = bornTransform.anchoredPosition-centerRoom.anchoredPosition;
        m_transform.anchoredPosition -= offsets;
        Debug.Log(m_transform.anchoredPosition);
    }
    public void MoveMapForPlayerCenter(Player player)
    {
        m_transform.anchoredPosition = new Vector2(0, 2170);
        RectTransform playerTransform = player.playerTransform;
        Vector2 offsets = playerTransform.anchoredPosition - centerRoom.anchoredPosition;
        m_transform.anchoredPosition -= offsets;
        Debug.Log(m_transform.anchoredPosition);
    }
    private void BoundaryCheak()
    {
        float x = Mathf.Clamp(m_transform.anchoredPosition.x,
            -(m_transform.sizeDelta.x*m_transform.localScale.x/4-windowWidth_LR),
            (m_transform.sizeDelta.x * m_transform.localScale.x/ 4 - windowWidth_LR));
        float y = Mathf.Clamp(m_transform.anchoredPosition.y, 
            -(m_transform.sizeDelta.y * m_transform.localScale.y / 4 - windowWidth_UD),
            (m_transform.sizeDelta.y * m_transform.localScale.y / 4 - windowWidth_UD));
        m_transform.anchoredPosition=new Vector2(x,y);

    }
    private void MapScale()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            float x = m_transform.localScale.x + 0.02f;
            x = Mathf.Clamp(x, 1, 50);
            float y = x * m_transform.localScale.y / m_transform.localScale.x;
            m_transform.localScale = new Vector3(x, y, m_transform.localScale.z);
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            float x = m_transform.localScale.x - 0.02f;
            x = Mathf.Clamp(x, 0.5f, 50);
            float y =x*m_transform.localScale.y/m_transform.localScale.x;
            m_transform.localScale = new Vector3(x, y, m_transform.localScale.z);
        }
    }


    private void Update()
    {
        //MapScale();
    }
}
