using LumiAudio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start main scene");
        DoSceneInitial();
    }

    private void DoSceneInitial()
    {
        //AudioMgr.Instance.PlayBGM("Music_01.mp3");

        UIMgr.GetInstance().ShowPanel(UIDef.UIMainPlayer, 25);
        UIMgr.GetInstance().ShowPanel(UIDef.UITestPanel);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
