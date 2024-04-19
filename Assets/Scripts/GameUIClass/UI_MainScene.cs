using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using UnityEngine;
using TMPro;

public class UI_MainScene : BasePanel
{
    [SerializeField] private int TargetId = 0;
    [SerializeField] private TMP_Text txtLumi;
    public override void Initial(params object[] args)
    {
        base.Initial(args);

        if(args.Length > 0)
        {
            TargetId = (int)args[0];
        }

        RefreshPanel();
    }

    protected override void Awake()
    {
        base.Awake();

    }

    private int Timer = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    protected override void OnEnable()
    {
        base.OnEnable();
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

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ClearTimer();
    }

    private void RefreshPanel()
    {
        txtLumi.text = string.Format(StringDef.MainTips2, TargetId);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventMgr.GetInstance().Invoke(EventDef.LumiFirstEvent, 100);
        }
    }

    void DoTimerAction()
    {
        Debug.Log("Lumiere get Action");
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
