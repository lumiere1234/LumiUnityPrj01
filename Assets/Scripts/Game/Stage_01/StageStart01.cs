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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
