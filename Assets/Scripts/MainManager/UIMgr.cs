using CoreManager;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UILoadParams
{
    public object[] args = null;
    public bool bDefaultScene = false;
}

public class UIMgr : SingletonAutoMono<UIMgr>
{
    private GameObject _root = null;
    public GameObject UIRoot { get
        {
            if (_root == null)
            {
                _root = GameObject.Find("UIRoot");
            }
            return _root;
        } }

    private Dictionary<EUISortingType, List<string>> CurUIDict = new Dictionary<EUISortingType, List<string>>();
    private Dictionary<string, BasePanel> PanelDict = new Dictionary<string, BasePanel>();
    private HashSet<string> ExpireNames = new HashSet<string>(); // 待清除UI
    private Stack<string> FPStack = new Stack<string>();
    private BasePanel GetPanel(string panelName) { return PanelDict[panelName]; }
    private BasePanel _curFullPanel;
    private BasePanel CurFullPanel => _curFullPanel;
    private void Awake()
    {
        CurUIDict.Clear();
        CurUIDict.Add(EUISortingType.Default, new List<string>());
        CurUIDict.Add(EUISortingType.FullScreen, new List<string>());
        CurUIDict.Add(EUISortingType.FullOverlay, new List<string>());
        CurUIDict.Add(EUISortingType.Tips, new List<string>());
        CurUIDict.Add(EUISortingType.System, new List<string>());
        EventMgr.GetInstance().Register(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
        InvokeRepeating("CheckExpiredUIUpdate", 1, 1);
    }
    private void OnDestroy()
    {
        EventMgr.GetInstance().UnRegister(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
    }
    public void ShowPanel(string uiName, params object[] args)
    {
        if (string.IsNullOrEmpty(uiName)) return;

        BasePanel panelInfo = null;
        if (PanelDict.ContainsKey(uiName))
        {
            panelInfo = PanelDict[uiName];
            if (!panelInfo.isActive)
            {
                // 设置sortingLayer
                panelInfo.canvas.sortingOrder = GetNextSortingTypeID(panelInfo.uiType);
            }
        }
        else
        {
            GUIConfig gCfg = GameConfigDataBase.GetConfigData<GUIConfig>(uiName);
            EUISortingType curType = (EUISortingType)gCfg.sortingType;
            GameObject go = ResManager.GetInstance().GetAsset(gCfg.path, typeof(GameObject)) as GameObject;
            if (go != null)
            {
                var panelGo = GameObject.Instantiate(go);
                panelGo.transform.SetParent(UIRoot.transform);
                panelInfo = panelGo.GetComponent<BasePanel>();
                if (panelInfo == null)
                {
                    throw new Exception($"No Panel Info in {uiName}");
                }
                panelInfo.uiCfg = gCfg;
                panelInfo.uiType = curType;
                panelInfo.uiName = uiName;
                // 设置摄像机
                panelInfo.DoInitial();
                // 设置sortingLayer
                panelInfo.canvas.sortingOrder = GetNextSortingTypeID(panelInfo.uiType);
                PanelDict.Add(uiName, panelInfo);
                CurUIDict[curType].Add(uiName);
            }
        }
        // 设置初始参数
        if (args != null && args.Length > 0 && args[0] is UILoadParams)
        {
            var paramInfo = args[0] as UILoadParams;
            if (paramInfo.bDefaultScene)
                SceneMgr.Instance.LoadDefaultUIOver();
            panelInfo.DoShowPanel(paramInfo.args);
        }
        else
            panelInfo.DoShowPanel(args);
    }
    private int GetNextSortingTypeID(EUISortingType eType)
    {
        int count = CurUIDict[eType].Count;
        int baseLayer = GlobalDef.SortingOrderDef[eType];
        if (count == 0)
        {
            return baseLayer;
        }

        var lastPanelName = CurUIDict[eType][count - 1];
        Canvas lastCanvas = GetPanel(lastPanelName).canvas;
        int lastId = lastCanvas.sortingOrder;
        if (lastId - baseLayer - count > 200)
        {
            lastId = ResetSortingOrder(eType);
        }
        return lastId + 1;
    }
    // 隐藏Panel
    public void HidePanel(string uiName)
    {
        if (!PanelDict.ContainsKey(uiName))
            return;
        BasePanel panel = PanelDict[uiName];
        panel.DoHidePanel();
        ExpireNames.Add(uiName);
    }
    // 销毁panel
    public void DestroyPanel(string uiName)
    {
        if (!PanelDict.ContainsKey(uiName))
            return;

        BasePanel panel = PanelDict[uiName];
        var list = CurUIDict[panel.uiType];
        for(int i = 0; i < list.Count; i++)
        {
            if (list[i].Equals(uiName))
            {
                list.RemoveAt(i);
                break;
            }
        }
        panel.DestroyPanel();
        PanelDict.Remove(uiName);
    }
    public void SaveFullPanelStack(string uiName)
    {
        FPStack.Push(uiName);
    }
    public void DoBackPanel(string uiName)
    {
        HidePanel(uiName);

        string lastPanel = FPStack.Pop();
        if (!string.IsNullOrEmpty(lastPanel))
        {
            ShowPanel(lastPanel);
        }
    }
    public void ResetPanel()
    {
        CurUIDict[EUISortingType.Default].Clear();
        CurUIDict[EUISortingType.FullScreen].Clear();
        CurUIDict[EUISortingType.FullOverlay].Clear();
        CurUIDict[EUISortingType.Tips].Clear();
        CurUIDict[EUISortingType.System].Clear();
    }
    public int ResetSortingOrder(EUISortingType eType)
    {
        int sortingLayer = GlobalDef.SortingOrderDef[eType];
        var list = CurUIDict[eType];
        foreach(var panelName in list)
        {
            var canvas = GetPanel(panelName).canvas;
            canvas.sortingOrder = sortingLayer++;
        }
        return sortingLayer - 1;
    }
    public void ReturnToMainUI(Action callBack)
    {
        FPStack.Clear();
        ShowPanel(UIDef.UIMainPanel);
    }

    #region Expire Panel
    private List<string> clearList = new List<string>();
    private void CheckExpiredUIUpdate()
    {
        if (ExpireNames.Count == 0)
            return;
        double curTime = DateTime.UtcNow.Ticks / 10000000.0;
        clearList.Clear();
        foreach (var name in ExpireNames)
        {
            if (PanelDict.ContainsKey(name))
            {
                var panel = PanelDict[name];
                if (!panel.CheckIsAlive(curTime))
                {
                    DestroyPanel(name);
                    clearList.Add(name);
                }
            }
            else
            {
                clearList.Add(name);
            }
        }
        if (clearList.Count > 0)
        {
            foreach(var name in clearList)
            {
                ExpireNames.Remove(name);
            }
            clearList.Clear();
        }
    }
    #endregion
    public void CloseUILoadScene()
    {
        FPStack.Clear();
        List<string> list = PanelDict.Keys.ToList();
        foreach (var name in list)
        {
            if (name == UIDef.UISceneLoadingPanel)
                continue;

            if (PanelDict[name].isActive)
            {
                HidePanel(name);
            }
        }
    }
    // Event
    private void OnSceneLoadedComplete(params object[] args)
    {

    }
}
