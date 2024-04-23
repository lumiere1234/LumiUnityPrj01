using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class GameSettingDef
{
    public static double DefaultDestroyUITime = 60;
}

public static class GameTextSpeedDef
{
    private static Dictionary<int, float> _factorDict;
    public static Dictionary<int, float> FactorDict 
    { 
        get 
        { 
            if ( _factorDict == null )
            {
                _factorDict = new();
                _factorDict.Add(1, 1);
                _factorDict.Add(2, 2);
                _factorDict.Add(3, 4);
                _factorDict.Add(4, 8);
            }
            return FactorDict;
        } 
    }
    private static float baseSpeed = 10f;
    private static float _minSpeed = 1;
    public static float MinSpeed => _minSpeed;
    private static float _maxSpeed = 100;

    public static float MaxSpeed => _maxSpeed;
    private static int _factor = -1;
    public static int Factor
    {
        get
        {
            if (_factor < 0)
            {
                _factor = PlayerPrefs.GetInt(StringDef.TextSpeedFactor);
                if (_factor <= 0)
                {
                    _factor = 1;
                }
            }
            return _factor;
        }
    } 
    private static float _currentSpeed = 0;
    public static float CurrentSpeed
    {
        get
        {
            if (_currentSpeed < MinSpeed)
            {
                _currentSpeed = PlayerPrefs.GetFloat(StringDef.TextSpeedSetting);
                if (_currentSpeed < MinSpeed)
                {
                    _currentSpeed = MinSpeed;
                }
            }
            return _currentSpeed;
        }
    }
    public static void SetSpeed(float Speed)
    {
        _currentSpeed = Mathf.Max(Mathf.Min(Speed, MaxSpeed), MinSpeed);
        PlayerPrefs.SetFloat(StringDef.TextSpeedSetting, CurrentSpeed);
    }
    public static void SetSpeedFactor(int index)
    {
        _factor = Mathf.Max(Mathf.Min(index, 4), 1);
        PlayerPrefs.SetFloat(StringDef.TextSpeedFactor, Factor);
    }
    public static float GetCurTextSpeed()
    {
        return CurrentSpeed * baseSpeed;
    }
}
