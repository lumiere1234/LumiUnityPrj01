using CoreManager;
using Newtonsoft.Json.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : SingletonAutoMono<UIManager>
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

    private void Awake()
    {
        EventManager.GetInstance().Register(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
    }
    private void OnDestroy()
    {
        EventManager.GetInstance().UnRegister(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
    }
    public void ShowPanel(string uiName, params object[] args)
    {
        GUIConfig gCfg = GameConfigDataBase.GetConfigData<GUIConfig>(uiName);
        GameObject panel = ResManager.GetInstance().GetAsset(gCfg.path, typeof(GameObject)) as GameObject;
        if (panel != null)
        {
            var go = GameObject.Instantiate(panel);
            go.transform.parent = UIRoot.transform;
            BasePanel panelInfo = go.GetComponent<BasePanel>();
            if (panelInfo != null)
            {
                panelInfo.Initial(args);
            }
        }
    }
    public void HidePanel(string uiName)
    {

    }
    // Event
    private void OnSceneLoadedComplete(params object[] args)
    {
        
    }
}
