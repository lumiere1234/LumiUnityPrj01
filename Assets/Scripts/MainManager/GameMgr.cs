using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : SingletonAutoMono<GameMgr>
{
    private int _defaultSaveId = -1;
    public int defaultSaveId 
    {
        get {
            if (_defaultSaveId < 0)
            {
                if (PlayerPrefs.HasKey(StringDef.DefaultSaveId))
                {
                    _defaultSaveId = PlayerPrefs.GetInt(StringDef.DefaultSaveId);
                }
                else
                    _defaultSaveId = 0;
            }
            return _defaultSaveId;
        }
        set
        { 
            if (value > 0 && value < 1000)
            {
                _defaultSaveId = value;
                PlayerPrefs.SetInt(StringDef.DefaultSaveId, value);
            }
        }
    }
    public void Initial()
    {

    }
    public void BackToMainScene(Action callBack = null)
    {
        SceneInfo curScene = SceneMgr.Instance.currentInfo;
        if (curScene != null && curScene.SceneCfg.sn == SceneDef.MainScene)
        {
            UIMgr.Instance.ReturnToMainUI(callBack);
        }
        else
        {
            SceneMgr.Instance.LoadScene(SceneDef.MainScene, callBack);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TipsMgr.Instance.ShowConfirm(StringDef.TipsLeaveGameStr, null, () =>
            {
                Debug.Log("Lumiere leave Game");
                Application.Quit();
            });
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            TipsMgr.Instance.ShowConfirm(StringDef.TipsTitleDefaultStr, "lumiere", () =>
            {
                Debug.Log("First");
                Application.Quit();
            });
        }
#if UNITY_EDITOR
        // 快速保存 T+S
        if (Input.GetKey(KeyCode.T))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                RequireSaveData();
            }
        }
#else
        // 快速保存 Ctrl+S
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                RequireSaveData();
            }
        }
#endif 
    }
    private void RequireSaveData()
    {
        string saveStr = string.Format(StringDef.TipsRequireSaveStr, $"savedata{defaultSaveId:000}");
        TipsMgr.Instance.ShowConfirm(saveStr, null, () =>
        {
            SaveMgr.Instance.DoSaveFile(defaultSaveId);
        });
    }
    // 快速保存
    public void DoQuickSave()
    {
        if (defaultSaveId < 0)
        {
            defaultSaveId = 0;
        }
        SaveMgr.Instance.DoSaveFile(defaultSaveId);
    }
}
