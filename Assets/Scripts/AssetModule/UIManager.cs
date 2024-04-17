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
    public void ShowPanel(string uiName, Action callback = null)
    {
        GUIConfig gCfg = GameConfigDataBase.GetConfigData<GUIConfig>(uiName);
        GameObject panel = ResManager.GetInstance().GetAsset(gCfg.path, typeof(GameObject)) as GameObject;
        if (panel != null)
        {
            var go = GameObject.Instantiate(panel); 
            go.transform.parent = UIRoot.transform;
        }

        callback?.Invoke();
    }

    public void HidePanel(string uiName)
    {

    }
}
