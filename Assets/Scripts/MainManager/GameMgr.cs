using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : SingletonAutoMono<GameMgr>
{
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
    }
}
