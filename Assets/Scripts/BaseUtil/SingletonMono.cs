using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstance()
    {
        return instance;
    }
    public static T Instance => instance;
    private void Awake()
    {
        instance = this as T;
    }

    private void OnDestroy()
    {
        instance = null;
    }
}
