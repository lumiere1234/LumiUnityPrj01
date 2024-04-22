using CoreManager;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        InitData();
        //TestAtlas();
        LoadMainScene();
        //LoadUI();
    }
    // 初始化数据结构
    void InitData()
    {
        CameraMgr.GetInstance().InitCamera();
        ResManager.GetInstance().InitRes();
    }

    void LoadMainScene()
    {
        //ResManager.GetInstance().LoadScene("MainScene", null);
        SceneMgr.GetInstance().LoadScene("MainScene");
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
