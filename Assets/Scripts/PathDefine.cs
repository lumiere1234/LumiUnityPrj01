using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CoreManager
{
    public static class PathDefine
    {
        public static string MainABName
        {
            get
            {
#if UNITY_IOS
                return "IOS";
#elif UNITY_ANDROID
                return "Android";
#else
                return "LumiPC";
#endif
            }
        }
        public static string AssetBundlePath
        {
            get { return Directory.GetParent(Application.dataPath).Name + "/AssetBundles/" + MainABName; }
        }
    }
}
public class PathDefine : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
