using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITeamTagItem : MonoBehaviour
{
    [SerializeField] private Toggle TeamTag;
    [SerializeField] private TMPro.TMP_Text LblName;

    public void SetIsChecked(bool bCheck)
    {
        TeamTag.isOn = bCheck;
    }
    public void SetName(string name)
    {
        LblName.text = name;
    }
}
