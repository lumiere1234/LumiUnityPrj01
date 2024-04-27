using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelBackPanel : BasePanel
{
    [SerializeField] private Button btnBack;
    [SerializeField] private TMPro.TMP_Text lblWorldName;
    protected override void Awake()
    {
        base.Awake();
        btnBack.onClick.AddListener(OnClickBtnBack);
    }

    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        RefreshPanel();
    }
    private void RefreshPanel()
    {
        WorldDataCfg wdCfg = WorldMgr.Instance.mCurWorldCfg;
        if (wdCfg == null) return;
        lblWorldName.text = wdCfg.worldName;
    }
    private void OnClickBtnBack()
    {
        WorldMgr.Instance.DoLeaveCurrWorld();
    }
}
