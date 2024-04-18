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

    // Start is called before the first frame update
    protected override void Start()
    {
        
    }

    private void OnEnable()
    {
        EventManager.GetInstance().Register(EventDef.LumiFirstEvent, LumiEventFunc);
    }

    private void OnDisable()
    {
        EventManager.GetInstance().UnRegister(EventDef.LumiFirstEvent, LumiEventFunc);
    }

    private void RefreshPanel()
    {
        txtLumi.text = $"Lumiere {TargetId}";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            EventManager.GetInstance().Invoke(EventDef.LumiFirstEvent, 100);
        }
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
