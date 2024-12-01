using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerBackPack : MonoBehaviour
{
    private ScrollRect m_scollRect;
    private RectTransform m_contentTranform;
    public GameObject backPackItem;
    private Player player;
    private List<GameObject> m_instances;
    public void PlayerBackPackAddObj(ObjectData obd)
    {
        GameObject pbpi = Instantiate(backPackItem);
        pbpi.transform.SetParent(m_contentTranform.transform,false);
        BackPackObject o= pbpi.GetComponentInChildren<BackPackObject>();
        o.objectData = obd;
        m_instances.Add(pbpi);
        TextMeshProUGUI t = pbpi.GetComponentInChildren<TextMeshProUGUI>();
        Image i = pbpi.GetComponentInChildren<Image>();
        t.text = obd.name;
        i.sprite = obd.objecticon;
    }
    public void PlayerBackPackRemoveObj(ObjectData obd)
    {
        for (int i = 0; i < m_instances.Count; i++)
        {
            BackPackObject o = m_instances[i].GetComponentInChildren<BackPackObject>();
            if (o.objectData==obd)
            {
                m_instances.RemoveAt(i);
                break;
            }
        }
    }

    public void PlayerBackPackClear()
    {
        foreach(var item in m_instances)
        {
            Destroy(item);
        }
    }

    private void Init()
    {
        player = RoomManager.roomManagerInstance.player;
        player.OnPlayerBackPackAdd += PlayerBackPackAddObj;
        player.OnPlayerBackPackClear += PlayerBackPackClear;
        player.OnPlayerBackPackRemove += PlayerBackPackRemoveObj;
        m_instances = new List<GameObject>();
        m_scollRect = GetComponentInChildren<ScrollRect>();
        m_contentTranform = m_scollRect.content;
    }
    private void Start()
    {
        Init();
    }

}
