using CoreManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        //TestAtlas();
        LoadMainScene();
        //LoadUI();
    }
    void LoadMainScene()
    {
        //ResManager.GetInstance().LoadScene("MainScene", null);
        SceneMgr.GetInstance().LoadScene("MainScene");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // ÷ÿ÷√”Œœ∑ƒ⁄»›
    public void ResetGame()
    {
        TimerMgr.GetInstance().ResetManager();
    }
}
