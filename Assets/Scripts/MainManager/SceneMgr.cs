using CoreManager;
using LumiAudio;
using System;
public class SceneInfo
{
    public string SceneName;
    public SceneDataCfg SceneCfg;
    public bool IsValid => SceneCfg != null;

    public SceneInfo(string sceneName)
    {
        this.SceneName = sceneName;
        SceneCfg = GameConfigDataBase.GetConfigData<SceneDataCfg>(sceneName);
    }
}
public class SceneMgr : SingletonAutoMono<SceneMgr>
{
    public SceneInfo lastInfo;
    public SceneInfo currentInfo;
    public bool bFirstEnter = true;
    private SceneDataCfg curSceneCfg => currentInfo?.SceneCfg;

    public void LoadScene(string sceneName, Action callBack = null)
    {
        lastInfo = currentInfo;
        currentInfo = new SceneInfo(sceneName);
        DoBeforeLoadScene();
        // close ui
        UIMgr.GetInstance().CloseUILoadScene();
        if (currentInfo.IsValid)
        {
            ResManager.GetInstance().LoadScene(sceneName, () => {
                DoAfterLoadScene();
                EventMgr.GetInstance().Invoke(EventDef.SceneLoadCompleteEvent);
                EventMgr.GetInstance().Invoke(EventDef.LoadingStreamCompleteEvent, BitDef.LoadingScene);
                callBack?.Invoke();
                });
        }
    }
    private void DoBeforeLoadScene()
    {
        if (bFirstEnter)
        {
            bFirstEnter = false;
            return;
        }

        // loading scene
        LoadTransScene();
        
        // 加载关卡可能用到的图集
        string[] atlasList = curSceneCfg.loadingAtlas.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (atlasList != null && atlasList.Length > 0)
        {
            EventMgr.Instance.Invoke(EventDef.LoadingStreamAddTaskEvent, BitDef.LoadingAtlas);
            ResManager.Instance.DoPreloadSpriteAtlas(atlasList);
        }
    }
    private void LoadTransScene()
    {
        UIMgr.Instance.ShowPanel(UIDef.UISceneLoadingPanel); // next scene
        int factor = BitDef.LoadingScene;
        if (curSceneCfg.defaultUIList.Length > 0)
        {
            factor |= BitDef.LoadingUI;
        }
        EventMgr.Instance.Invoke(EventDef.LoadingStreamAddTaskEvent, factor);
    }
    private void DoAfterLoadScene()
    {
        // 播放入场景音乐
        DoPlayEnterBGM();
        // 加载默认UI
        DoLoadDefaultUI();
    }
    private void DoPlayEnterBGM()
    {
        if (currentInfo.IsValid)
        {
            // Play BGM
            var sceneCfg = currentInfo.SceneCfg;
            if (sceneCfg.bNoBGM)
                AudioMgr.Instance.StopBGM();
            else
            {
                if (!sceneCfg.sceneBGM.Equals(string.Empty))
                {
                    AudioMgr.Instance.PlayBGM(sceneCfg.sceneBGM, sceneCfg.bForceBGM);
                }
            }
        }
    }
    private int _loadUICount = 0;
    private void DoLoadDefaultUI()
    {
        _loadUICount = curSceneCfg.defaultUIList.Length;
        if (_loadUICount > 0)
        {
            EventMgr.Instance.Invoke(EventDef.LoadingStreamAddTaskEvent, BitDef.LoadingUI);
        }

        foreach (var ui in curSceneCfg.defaultUIList)
        {
            var param = new UILoadParams();
            param.bDefaultScene = true;
            UIMgr.Instance.ShowPanel(ui, param);
        }
    }
    public void LoadDefaultUIOver()
    {
        if (_loadUICount > 0)
        {
            _loadUICount--;
            if (_loadUICount == 0)
            {
                EventMgr.Instance.Invoke(EventDef.LoadingStreamCompleteEvent, BitDef.LoadingUI);
            }
        }
    }
}
