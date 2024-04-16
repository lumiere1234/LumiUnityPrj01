using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class Lumiere {
    public int lumi1 = 0;
    public string lumiStr = string.Empty;
}
public class LumiereGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartGameTest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void StartGameTest()
    {
        FirstCfg data = GameConfigDataBase.GetConfigData<FirstCfg>("3");
        if (data != null)
        {
            Debug.Log($"Lumiere get Data : {data.mainImg}");
        }
    }

    void StartGameTest2() 
    {
        Type type = typeof(Lumiere);
        FieldInfo property = type.GetField("lumi1");
        Lumiere lumi01 = new Lumiere();
        property.SetValue(lumi01, 100);
        Debug.Log(lumi01.lumi1);
    }
}
