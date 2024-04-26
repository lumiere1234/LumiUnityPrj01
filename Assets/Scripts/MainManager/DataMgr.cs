using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : SingletonAutoMono<DataMgr>
{
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
                EventMgr.Instance.Invoke(EventDef.ChangeMainHeroineId);
            }
        }
    }
}
