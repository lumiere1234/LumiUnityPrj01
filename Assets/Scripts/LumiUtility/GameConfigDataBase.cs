using CoreManager;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigDataBase
{
    protected virtual string getFilePath()
    {
        return "";
    }
    public string Name;
    static Dictionary<string, Dictionary<string, GameConfigDataBase>> dataDic = new Dictionary<string, Dictionary<string, GameConfigDataBase>>();

    public static T GetConfigData<T>(string key, string fileName = null) where T : GameConfigDataBase
    {
        Type setT = typeof(T);
        if(fileName != null)
        {
            fileName = setT.Name;
        }

        if (!dataDic.ContainsKey(fileName))
        {
            ReadConfigData<T>(fileName);
        }

        Dictionary<string, GameConfigDataBase> objDic = dataDic[fileName];
        if (!objDic.ContainsKey(key))
        {
            throw new Exception("no this config");
        }
        return (T)(objDic[key]);
    }

    public static T ReadConfigData<T>(string fileName = null) where T : GameConfigDataBase
    {
        T obj = Activator.CreateInstance<T>();
        if (fileName == null)
            fileName = obj.getFilePath();

        string path = $"{MainUtility.ExcelDir}/{fileName}.csv";
        string getString = (ResManager.)

        return null;
    }

    
}
