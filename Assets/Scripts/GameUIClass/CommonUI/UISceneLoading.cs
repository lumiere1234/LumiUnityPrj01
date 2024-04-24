using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneLoading : BasePanel
{
    private int LoadingFactor = -1;
    protected override void Awake()
    {
        base.Awake();
        LoadingFactor = -1;
    }
    public override void RegistCustomEvent()
    {
        base.RegistCustomEvent();
        EventMgr.Instance.Register(EventDef.LoadingStreamCompleteEvent, OnSceneTaskCompleteEvent);
    }
    public override void UnregistCustomEvent()
    {
        base.UnregistCustomEvent();
        EventMgr.Instance.UnRegister(EventDef.LoadingStreamCompleteEvent, OnSceneTaskCompleteEvent);
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);
        if (args.Length > 0)
        {
            SceneInfo info = (SceneInfo)args[0];
            SceneDataCfg dataCfg = info?.SceneCfg;
            LoadingFactor = dataCfg == null ? 0 : dataCfg.loadingFactor;
        }
        else
            LoadingFactor = 0;
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (LoadingFactor == 0)
        {
            DoCloseLoading();
        }
    }
    public override void DoHidePanel()
    {
        base.DoHidePanel();
        LoadingFactor = -1;
    }
    private void DoCloseLoading()
    {
        UIMgr.Instance.HidePanel(uiName);
    }
    private void OnSceneTaskCompleteEvent(params object[] args)
    {
        if (args.Length > 0)
        {
            LoadingFactor -= (int)args[0];
        }
    }
}
