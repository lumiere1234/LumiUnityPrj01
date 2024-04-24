using LumiAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageStart01 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // load playerHead
        UIMgr.GetInstance().ShowPanel(UIDef.UIMainHead);

        //AudioMgr.Instance.PlayBGM("Music_03.mp3");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M))
        {
            SceneMgr.Instance.LoadScene(SceneDef.StageScene01);
        }
    }
}
