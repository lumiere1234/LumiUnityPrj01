using CoreManager;
using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveDataInfo
{
    public int saveId = -1;
    public List<CharaCardSaveData> cardList = new List<CharaCardSaveData>();
    public GameMainSaveData gameData = null;
    public List<ItemSaveData> itemList = new List<ItemSaveData>();
}
[Serializable]
public class CharaCardSaveData
{
    public int cardId;
    public int cardStar;
}
[Serializable]
public class GameMainSaveData
{
    public string playerId;
    public string playerName;
    public int showHeroineId;
}
[Serializable]
public class ItemSaveData
{
    public int itemId;
    public int itemCount;
}

/// <summary>
/// 存档管理类
/// </summary>
public class SaveMgr : SingletonAutoMono<SaveMgr>
{
    private string preSaveFile = "savedata";
    private SaveDataInfo saveInfo = null;
    private void SaveGameByJson(int index)
    {
        if (index > 999) return;

        if (saveInfo == null)
            return;

        try
        {
            // check folder
            string saveDir = PathDefine.SaveDataDir;
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            string filePath = $"{saveDir}/{preSaveFile}{index:000}.json";
            FileInfo fileInfo = new FileInfo(filePath);
            if(!fileInfo.Exists)
            {
                fileInfo.Create().Dispose();
            }

            using (StreamWriter sw = new StreamWriter(filePath)) 
            {
                string jsonStr = JsonUtility.ToJson(saveInfo);
                sw.Write(jsonStr);
            }
        }
        catch (Exception e) 
        {
            Debug.Log(e.Message);
        }
    }
    private void LoadGameFromJson(int index)
    {
        if (index > 999) return;

        saveInfo = new SaveDataInfo();
        try
        {
            string filePath = $"{PathDefine.SaveDataDir}/{preSaveFile}{index:000}.json";
            if (!File.Exists(filePath)) { return; }
            using(StreamReader sr = new StreamReader(filePath)) 
            {
                string jsonStr = sr.ReadToEnd();
                saveInfo = JsonUtility.FromJson(jsonStr, typeof(SaveDataInfo)) as SaveDataInfo;
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    public void DoSaveFile(int index)
    {
        // GetSaveData
        saveInfo = new SaveDataInfo();
        saveInfo.saveId = index;
        DataMgr.Instance.DoAddSaveData(ref saveInfo);
        CharaCardMgr.Instance.DoAddSaveData(ref saveInfo);
        // setdefault id
        GameMgr.Instance.defaultSaveId = index;
        // save data
        SaveGameByJson(index);
    }
    public bool DoLoadFile(int index)
    {
        // load file
        saveInfo = new SaveDataInfo();
        LoadGameFromJson(index);

        //if (saveInfo == null)
        //    return;
        
        // load data
        DataMgr.Instance.DoLoadSaveData(saveInfo);
        CharaCardMgr.Instance.DoLoadSaveData(saveInfo);

        // 是否成功初始化
        return saveInfo.saveId >= 0;
    }
}
