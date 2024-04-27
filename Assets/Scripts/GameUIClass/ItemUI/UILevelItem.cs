using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelItem : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text lblTitle;
    [SerializeField] private TMPro.TMP_Text lblName;
    [SerializeField] private GameObject GroupInfo;
    [SerializeField] private TMPro.TMP_Text lblScoreTitle;
    [SerializeField] private TMPro.TMP_Text lblScore;
    [SerializeField] private TMPro.TMP_Text lblDiffTitle;
    [SerializeField] private UIStarItem starItem;
    
    public void RefreshLevelData(int id)
    {

        starItem.RefreshStarTen(7, false);
    }
}
