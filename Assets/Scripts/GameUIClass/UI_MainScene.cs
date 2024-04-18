using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MainScene : BasePanel
{
    // Start is called before the first frame update
    void Start()
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
