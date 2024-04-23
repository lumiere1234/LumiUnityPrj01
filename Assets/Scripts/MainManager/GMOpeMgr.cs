using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GMOpeMgr : SingletonAutoMono<GMOpeMgr>
{
    public void DoOperation(string opeStr)
    {
        string[] strList = opeStr.Trim().Split(',');
        if (strList.Length == 0)
            return;

        string gmType = strList[0];
        if (strList.Length > 2)
        {

        }
        else if (strList.Length > 1)
        {
            if (gmType.Equals("OpenScene"))
            {
                string param1 = strList[1];
                SceneMgr.GetInstance().LoadScene(param1);
            }
        }
    }
}
