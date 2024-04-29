using CoreManager;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        if (LoadDefaultSaveData())
        {
            ShowLoadingPanel();
            InitData();
            LoadMainScene();
        }
        else
        {
            Debug.Log("lumiere No SaveData");
            ShowLoadingPanel();
            InitData();
            LoadMainScene();
        }
    }
    bool LoadDefaultSaveData()
    {
        int saveId = GameMgr.Instance.defaultSaveId;
        return SaveMgr.Instance.DoLoadFile(saveId);
    }
    void ShowLoadingPanel()
    {
        UILoading01Panel.Show();
        EventMgr.Instance.Invoke(EventDef.Scene_LoadingTaskAdd, BitDef.LoadingAtlas | BitDef.LoadingScene);
    }
    // ��ʼ�����ݽṹ
    void InitData()
    {
        DataMgr.Instance.Initial();
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

    // ������Ϸ����
    public void ResetGame()
    {
        TimerMgr.GetInstance().ResetManager();
    }
}
