using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GMOpeMgr : SingletonAutoMono<GMOpeMgr>
{
    public void DoOperation(string opeStr)
    {
        string[] strList = opeStr.Trim().Split(',');
        if (strList.Length == 0)
            return;

        string gmType = strList[0];
        if (gmType.Equals("OpenScene"))
        {
            string param1 = strList[1];
            SceneMgr.GetInstance().LoadScene(param1);
        }
        else if (gmType.Equals("AddItem"))
        {
            int param1 = strList.Length > 1 ? int.Parse(strList[1]) : 0;
            int param2 = strList.Length > 2 ? int.Parse(strList[2]) : 1;
            ItemMgr.Instance.ItemChange(param1, param2);
        }
        else if (gmType.Equals("AddCard"))
        {
            int param1 = strList.Length > 1 ? int.Parse(strList[1]) : 0;

        }

    }
}
