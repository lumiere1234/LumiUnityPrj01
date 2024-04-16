using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        ConfFirstCfg data = GameConfigDataBase.GetConfigData<ConfFirstCfg>("3");
    }
}
