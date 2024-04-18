using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start main scene");
        LoadUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadUI()
    {
        UIManager.GetInstance().ShowPanel("UI-MainPlayer");
    }
}
