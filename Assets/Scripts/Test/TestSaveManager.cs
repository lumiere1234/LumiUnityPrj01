using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TestSaveManager : SingletonAutoMono<TestSaveManager>
{
    private int SavedScore01 = 0;
    private int SavedScore02 = 0;

    public int Score01 = 0;
    public int Score02 = 0;

    private void Awake()
    {
        LoadFromBin();
    }

    private TestData GetGameData()
    {
        TestData data = new TestData();
        return data;
    }
    private void SetGameData(TestData data)
    {
        Score01 = data.Score1;
        Score02 = data.Score2;
    }

    private string saveDir => Directory.GetParent(Application.dataPath) + "/SaveData";
    private string saveFile => "data001.dat";
    private string jsonFile => "dataJson001.json";
    private void SaveByBin()
    {
        try
        {
            TestData data = GetGameData();
            data.Score1 = Score01;
            data.Score2 = Score02;
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            string filePath = $"{saveDir}/{saveFile}";
            FileInfo info = new FileInfo(filePath);
            if (!info.Exists) 
            {
                info.Create().Dispose();
            }
            
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(filePath))
            {
                bf.Serialize(fs, data);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void LoadFromBin()
    {
        try
        {
            if (!Directory.Exists(saveDir))
                return;
            string targetPath = $"{saveDir}/{saveFile}";

            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(targetPath, FileMode.Open))
            {
                TestData data = (TestData)bf.Deserialize(fs);
                SetGameData(data);
            }
        }
        catch(Exception e) 
        {
            Debug.Log(e.Message);
        }
    }
    private void SaveJson()
    {
        try
        {
            TestData data = GetGameData();
            data.Score1 = Score01;
            data.Score2 = Score02;
            if (!Directory.Exists(saveDir))
            {
                Directory.CreateDirectory(saveDir);
            }
            string filePath = $"{saveDir}/{jsonFile}";
            FileInfo info = new FileInfo(filePath);
            if (!info.Exists)
            {
                info.Create().Dispose();
            }

            //BinaryFormatter bf = new BinaryFormatter();
            using (StreamWriter sw = new StreamWriter(filePath))
            {
                string targetStr = JsonUtility.ToJson(data);
                sw.Write(targetStr);
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    private void LoadJson()
    {
        try
        {
            string targetPath = $"{saveDir}/{jsonFile}";
            if (!File.Exists(targetPath))
            {
                return;
            }
            using (StreamReader sr = new StreamReader(targetPath))
            {
                string jsonStr = sr.ReadToEnd();
                
                TestData data = JsonUtility.FromJson(jsonStr, typeof(TestData)) as TestData;
                if (data != null)
                {
                    SetGameData(data);
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    public void DoSaveData()
    {
        //SaveByBin();
        SaveJson();
    }
    public void DoLoadData()
    {
        //LoadFromBin();    
        LoadJson();

        EventMgr.Instance.Invoke(EventDef.Default);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
