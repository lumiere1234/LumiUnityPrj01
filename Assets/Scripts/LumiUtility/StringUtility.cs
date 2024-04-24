using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringUtility
{
    public static T[] SplitStringByType<T>(string str, string signal = ",") where T : struct
    {
        string[] splitStr = str.Split(signal);
        if (splitStr.Length == 1 && splitStr[0].Equals(string.Empty)){
            return new T[0];
        }

        T[] result = new T[splitStr.Length];
        try
        {
            for(int i = 0; i < splitStr.Length; i++)
            { 
                splitStr[i] = splitStr[i].Trim();
                if (splitStr.Equals(string.Empty))
                    result[i] = (T)Convert.ChangeType(0, typeof(T));
                else
                    result[i] = (T)Convert.ChangeType(splitStr[i], typeof(T));
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
        return result;
    }
    public static string[] SplitString(string str, string signal = ",")
    {
        string[] splitStr = str.Split(signal);
        for(int i = 0; i < splitStr.Length; i++)
        {
            splitStr[i] = splitStr[i].Trim();
        }
        return splitStr;
    }
    public static string GetNameWithType(string path)
    {
        return path.Substring(path.LastIndexOf('/') + 1);
    }
    public static string GetPureName(string path)
    {
        int startIndex = path.LastIndexOf('/') + 1;
        int length = path.LastIndexOf('.') - startIndex;
        return path.Substring(startIndex, length);
    }
    public static string GetAssetPathStringTrans(string path)
    {
        string ret = path.Replace(@"\", "/");
        return ret;
    }
    public static string GetStringById(int id, params object[] args)
    {
        StringCfg cfg = GameConfigDataBase.GetConfigData<StringCfg>(id.ToString());
        if (cfg == null)
            return "";
        return string.Format(cfg.str, args);
    }
}
