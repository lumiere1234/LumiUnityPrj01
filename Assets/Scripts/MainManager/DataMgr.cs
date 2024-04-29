using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : SingletonAutoMono<DataMgr>
{
    private string _playerName = string.Empty;
    public string PlayerName => _playerName;
    private int _defaultMainHerineId = 10001;
    private int _curMainHeroineId = 0;
    public int CurMainHeroineId 
    {
        get 
        { 
            if (_curMainHeroineId == 0)
            {
                _curMainHeroineId = _defaultMainHerineId;
            }
            return _curMainHeroineId;
        }
        set
        {
            if (value != _curMainHeroineId)
            {
                _curMainHeroineId = value;
                EventMgr.Instance.Invoke(EventDef.Main_ChangeDisplayHeroine);
            }
        }
    }
    
    public void Initial()
    {
        
    }
    public void DoAddSaveData(ref SaveDataInfo info)
    {
        var gameData = new GameMainSaveData();
        gameData.showHeroineId = CurMainHeroineId;
        gameData.playerName = PlayerName;

        info.gameData = gameData;
    }
    public void DoLoadSaveData(SaveDataInfo data)
    {
        if (data.saveId < 0)
        {
            // ¿Õ³õÊ¼»¯
        }
        else
        {
            var gameData = data.gameData;
            CurMainHeroineId = gameData.showHeroineId;
        }
    }
}
