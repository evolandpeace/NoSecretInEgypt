using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIDeaths : MonoBehaviour
{
    public TextMeshProUGUI number;

    private void Update()
    {
        UndateDeathsNum();
    }
    private void UndateDeathsNum()
    {
        number.text = LevelManager.instance.infoPool.numOfDeaths.ToString();
    }
}
