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

    public void LoadScene(string sceneName, Action callBack = null)
    {
        DoBeforeLoadScene();
        lastInfo = currentInfo;
        currentInfo = new SceneInfo(sceneName);
        // close ui
        UIMgr.GetInstance().CloseUILoadScene();
        if (currentInfo.IsValid)
        {
            ResManager.GetInstance().LoadScene(sceneName, () => {
                DoAfterLoadScene();
                EventMgr.GetInstance().Invoke(EventDef.SceneLoadCompleteEvent);
                callBack?.Invoke();
                });
        }
    }
    private void DoBeforeLoadScene()
    {

    }
    private void DoAfterLoadScene()
    {
        // ≤•∑≈»Î≥°æ∞“Ù¿÷
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
