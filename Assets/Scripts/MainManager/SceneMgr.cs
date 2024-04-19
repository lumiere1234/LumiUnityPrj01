using CoreManager;
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

    public void LoadScene(string sceneName, Action callBack = null)
    {
        lastInfo = currentInfo;
        currentInfo = new SceneInfo(sceneName);
        if (currentInfo.IsValid)
        {
            ResManager.GetInstance().LoadScene(sceneName, () => {
                EventMgr.GetInstance().Invoke(EventDef.SceneLoadCompleteEvent);
                callBack?.Invoke();
                });
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
