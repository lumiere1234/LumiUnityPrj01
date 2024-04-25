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
        EventMgr.Instance.Register(EventDef.LoadingStreamAddTaskEvent, OnAddTaskStatusEvent);
    }
    public override void UnregistCustomEvent()
    {
        base.UnregistCustomEvent();
        EventMgr.Instance.UnRegister(EventDef.LoadingStreamCompleteEvent, OnSceneTaskCompleteEvent);
        EventMgr.Instance.UnRegister(EventDef.LoadingStreamAddTaskEvent, OnAddTaskStatusEvent);
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);
        LoadingFactor = -1;
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
    private void SetTask(int id)
    {
        if (LoadingFactor < 0) LoadingFactor = 0;
        LoadingFactor |= id;
    }
    private void TaskComplete(int id)
    {
        if ((LoadingFactor & id) > 0)
        {
            LoadingFactor -= id;
        }
    }
    private void OnSceneTaskCompleteEvent(params object[] args)
    {
        if (args.Length > 0)
        {
            TaskComplete((int)args[0]);
        }
    }
    private void OnAddTaskStatusEvent(params object[] args)
    {
        if (args.Length > 0)
        {
            SetTask((int)args[0]);
        }
    }
}
