using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
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
        FirstCfg data = GameConfigDataBase.GetConfigData<FirstCfg>("4");
        if (data != null)
        {
            Debug.Log($"Lumiere get Data : {data.mainImg}");
        }

        string cubePath = data.path;
        GameObject go = AssetDatabase.LoadAssetAtPath<GameObject>(cubePath);
        var instance = Instantiate(go);
        instance.transform.parent = transform;

        LumiTestConfig testCfg = GameConfigDataBase.GetConfigData<LumiTestConfig>("13");
        if(testCfg != null ) 
        {
            instance.transform.position = testCfg.position.ToVector3();
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
