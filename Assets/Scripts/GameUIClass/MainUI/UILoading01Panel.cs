using UnityEngine;

public class UILoading01Panel : MonoBehaviour
{
    private static UILoading01Panel instance;
    public static UILoading01Panel Instance { get { return instance; } }
    private int NeedBit = 0x0;
    public static void Show()
    {
        if (Instance == null)
        {
            GameObject go = Resources.Load("UI-Loading01") as GameObject;
            GameObject uiRoot = GameObject.Find("UIRoot");
            GameObject goIns = GameObject.Instantiate(go);
            if (uiRoot != null)
            {
                goIns.transform.SetParent(uiRoot.transform);
            }
            instance = goIns.GetComponent<UILoading01Panel>();
        }
    }
    private void Awake()
    {
        EventMgr.Instance.Register(EventDef.Scene_LoadingTaskComplete, OnCompleteStatusEvent);
        EventMgr.Instance.Register(EventDef.Scene_LoadingTaskAdd, OnAddTaskStatusEvent);
    }
    public static void Hide()
    {
        if (Instance != null)
        {
            Destroy(Instance.gameObject);
            instance = null;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        NeedBit = -1;
        //NeedBit = BitDef.LoadingScene + BitDef.LoadingAtlas;
    }
    // Update is called once per frame
    void Update()
    {
        if (NeedBit == 0x0)
            Hide();
    }
    private void OnDestroy()
    {
        NeedBit = -1;
        EventMgr.Instance.UnRegister(EventDef.Scene_LoadingTaskAdd, OnAddTaskStatusEvent);
        EventMgr.Instance.UnRegister(EventDef.Scene_LoadingTaskComplete, OnCompleteStatusEvent);
    }
    private void SetTask(int id)
    {
        if (NeedBit < 0) NeedBit = 0;
        NeedBit |= id;
    }
    private void TaskComplete(int id)
    {
        if ((NeedBit & id) > 0)
        {
            NeedBit -= id;
        }
    }
    private void OnCompleteStatusEvent(params object[] args)
    {
        if (args.Length > 0)
        {
            TaskComplete((int)args[0]);
        }
    }
    private void OnAddTaskStatusEvent(params object[] args)
    {
        if (args.Length > 0)
        {
            SetTask((int)args[0]);
        }
    }
}
