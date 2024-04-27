using CoreManager;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        ShowLoadingPanel();
        InitData();
        LoadMainScene();
    }
    void ShowLoadingPanel()
    {
        UILoading01Panel.Show();
        EventMgr.Instance.Invoke(EventDef.LoadingStreamAddTaskEvent, BitDef.LoadingAtlas | BitDef.LoadingScene);
    }
    // 初始化数据结构
    void InitData()
    {
        GameMgr.Instance.Initial();
        WorldMgr.Instance.Initial();
        InputMgr.Instance.Initial();
        CameraMgr.Instance.InitCamera();
        ResManager.Instance.InitRes();
    }

    void LoadMainScene()
    {
        SceneMgr.GetInstance().LoadScene(SceneDef.MainScene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 重置游戏内容
    public void ResetGame()
    {
        TimerMgr.GetInstance().ResetManager();
    }
}
