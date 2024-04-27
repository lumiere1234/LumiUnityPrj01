using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMgr : SingletonAutoMono<WorldMgr>
{
    public List<int> WorldList = new List<int> ();
    private WorldDataCfg curWorldCfg = null;
    public WorldDataCfg mCurWorldCfg => curWorldCfg;
    public void Initial()
    {
        WorldList.Clear();
        WorldList.Add(10001);
        WorldList.Add(10002);
        WorldList.Add(10003);
        WorldList.Add(10004);
    }
    public WorldDataCfg GetWorldDataCfgById(int index)
    {
        if (index > WorldList.Count)
        {
            return null;
        }
        return GameConfigDataBase.GetConfigData<WorldDataCfg>(WorldList[index].ToString());
    }
    public bool DoClickEnterWorld(int index)
    {
        var worldCfg = GetWorldDataCfgById(index);
        if (worldCfg == null) return false;

        SceneMgr.Instance.LoadScene(worldCfg.sceneName);
        curWorldCfg = worldCfg;
        return true;
    }
    public void ResetCurWorldData()
    {
        curWorldCfg = null;
    }
    public void DoLeaveCurrWorld()
    {
        Action leaveWorldHandler = () =>
        {
            ResetCurWorldData();
            SceneMgr.Instance.LoadMainScene();
        };

        TipsMgr.Instance.ShowConfirm("do you leave scene", null, leaveWorldHandler);
    }
}
