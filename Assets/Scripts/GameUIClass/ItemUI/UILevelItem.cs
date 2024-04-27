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
    [SerializeField] private Button btnLevelItem;

    private int CurIndex = -1;
    private void Awake()
    {
        btnLevelItem.onClick.AddListener(OnClickBtnLevelItem);
    }
    public void RefreshLevelData(int id)
    {
        CurIndex = id;
        RefreshLevelPanel();
    }
    private void RefreshLevelPanel()
    {
        var worldCfg = WorldMgr.Instance.GetWorldDataCfgById(CurIndex);
        if (worldCfg == null) return;

        lblTitle.text = $"World{CurIndex + 1}";
        lblName.text = worldCfg.worldName;
        starItem.RefreshStarTen(worldCfg.difficulty, false);
    }

    private void OnClickBtnLevelItem()
    {
        if (!WorldMgr.Instance.DoClickEnterWorld(CurIndex))
        {
            Debug.Log("error");
        }
    }
}
