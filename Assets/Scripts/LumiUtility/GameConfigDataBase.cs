using CoreManager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

public class GameConfigDataBase
{
    protected virtual string getFilePath()
    {
        return "";
    }
    static Dictionary<string, Dictionary<string, GameConfigDataBase>> dataDic = new Dictionary<string, Dictionary<string, GameConfigDataBase>>();

    public static T GetConfigData<T>(string key, string fileName = null) where T : GameConfigDataBase
    {
        Type setT = typeof(T);
        if(fileName == null)
        {
            fileName = setT.Name;
        }

        if (!dataDic.ContainsKey(fileName))
        {
            ReadConfigData<T>(fileName);
        }

        if (dataDic.ContainsKey(fileName))
        {
            Dictionary<string, GameConfigDataBase> objDic = dataDic[fileName];
            if (!objDic.ContainsKey(key))
            {
                throw new Exception("no this config");
            }
            return (T)(objDic[key]);
        }
        return null;
    }

    public static T ReadConfigData<T>(string fileName = null) where T : GameConfigDataBase
    {
        T obj = Activator.CreateInstance<T>();
        if (fileName == null)
            fileName = obj.getFilePath();

        string path = $"{PathDefine.TargetDir}/{fileName}.csv";
        if (!File.Exists(path)) {
            return null;
        }

        Dictionary<string, GameConfigDataBase> curCfg = new Dictionary<string, GameConfigDataBase>(); 
        using(FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read)) 
        {
            using (StreamReader sr = new StreamReader(fs, Encoding.GetEncoding("utf-8")))
            {
                string strLine = "";
                string[] nameLine = null;
                string[] typeLine = null;
                string[] anyLine = null;
                int count = 0;
                string curName = string.Empty;
                string curType = string.Empty;
                while((strLine = sr.ReadLine()) != null) {
                    strLine = strLine.Trim();
                    if (count == 0)
                    {
                        nameLine = strLine.Split(',');
                    }
                    else if(count == 1)
                    {
                        typeLine = strLine.Split(",");
                    }
                    else if(count > 2)
                    {
                        anyLine = strLine.Split(",");
                        T configData = Activator.CreateInstance<T>();
                        Type type = typeof(T);

                        for (int i = 0; i < nameLine.Length - 1; i++) 
                        {
                            curName = nameLine[i];
                            curType = typeLine[i];
                            if(curType == "int")
                            {
                                type.GetField(curName).SetValue(configData, int.Parse(anyLine[i]));
                            }
                            else if(curType == "string")
                            {
                                type.GetField(curName).SetValue(configData, anyLine[i]);
                            }
                        }
                        curCfg.Add(anyLine[0], configData);
                    }
                    count++;
                }
            }
        }
        dataDic.Add(fileName, curCfg);
        return null;
    }
}
