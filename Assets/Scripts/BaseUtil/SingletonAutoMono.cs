using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstance()
    {
        if (instance == null)
        {
            GameObject go = new GameObject();
            go.name = typeof(T).Name;
            DontDestroyOnLoad(go);
            instance = go.AddComponent<T>();
        }
        return instance;
    }
}
