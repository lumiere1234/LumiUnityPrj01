using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene05 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SceneMgr.Instance.LoadScene(SceneDef.StageScene06);
        }
    }
}
