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

        UIMgr.Instance.ShowPanel(UIDef.UISceneLoading, currentInfo); // next scene
        // 加载关卡可能用到的图集
        string[] atlasList = curSceneCfg.loadingAtlas.Split(',', StringSplitOptions.RemoveEmptyEntries);
        if (atlasList != null && atlasList.Length > 0)
        {
            ResManager.Instance.DoPreloadSpriteAtlas(atlasList);
        }
    }
    private void DoAfterLoadScene()
    {
        // 播放入场景音乐
        DoPlayEnterBGM();
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
    // Start is called before the first frame update
    void Start()
    {
           
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
