using UnityEngine;
using TMPro;
using UnityEngine.UI;
using CoreManager;

public class UIMainPlayer : BasePanel
{
    [SerializeField] private int TargetId = 0;
    [SerializeField] private TMP_Text txtLumi;
    [SerializeField] private Button btnExit;
    [SerializeField] private Button btnClose;
    [SerializeField] private Image imgBG;
    private int Timer = 0;
    public override void DoInitial()
    {
        base.DoInitial();
        btnExit.onClick.AddListener(OnClickBtnExit);
        btnClose.onClick.AddListener(OnClickBtnClose);
    }
    public override void DoShowPanel(params object[] args)
    {
        if (!isActive)
        {
            // regist event
        }

        base.DoShowPanel(args);
        if (args.Length > 0)
        {
            TargetId = (int)args[0];
        }
        RefreshPanel();
        StartTimer();
    }
    private void StartTimer()
    {
        ClearTimer();
        Timer = TimerMgr.GetInstance().CreateTimer(DoTimerAction, new TimerData(2));
    }
    private void ClearTimer()
    {
        if (Timer > 0)
        {
            TimerMgr.GetInstance().RemoveTimer(Timer);
            Timer = 0;
        }
    }
    public override void DoHidePanel()
    {
        base.DoHidePanel();
        ClearTimer();
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnExit.onClick.RemoveAllListeners();
        btnClose.onClick.RemoveAllListeners();
    }
    private void RefreshPanel()
    {
        //txtLumi.text = string.Format(StringDef.MainTips2, TargetId);
        txtLumi.text = StringUtility.GetStringById(10001);
        imgBG.sprite = ResManager.GetInstance().LoadSprite("BG01.jpg");
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventMgr.GetInstance().Invoke(EventDef.LumiFirstEvent, 100);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            canvasGroup.alpha -= 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            canvasGroup.alpha += 0.2f;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            imgBG.sprite = ResManager.GetInstance().LoadSprite("BG01.jpg");
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            UIMgr.GetInstance().HidePanel(uiName);
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogMgr.GetInstance().DoStartDialog(10002);
        }
    }
    void DoTimerAction()
    {
        Debug.Log("Lumiere get Action");
    }
    private void OnClickBtnExit()
    {
        Application.Quit();
    }
    private void OnClickBtnClose()
    {
        UIMgr.GetInstance().HidePanel(uiName);
    }
    // Event part
    void LumiEventFunc(params object[] args)
    {
        Debug.Log("CheckLumiFunc");
    }
    void LumiEventWithParam(params object[] args)
    {
        if (args.Length >= 1)
        {
            Debug.Log($"CheckLumiFunc with param {args[0]}");
        }
    }
}
