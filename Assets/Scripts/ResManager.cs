using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreManager
{
    public enum ResType
    {
        ResSingle, // single res
        AssetBundle, // AB°ü×ÊÔ´
    }

    public class ResManager : SingletonAutoMono<ResManager>
    {
        public ResType curType;
        // key, resource
        public static Dictionary<string, CoreResource> AssetListRes = new Dictionary<string, CoreResource>();

        private void Awake()
        {
#if UNITY_EDITOR
            curType = ResType.ResSingle;
#else
            curType = ResType.AssetBundle;
#endif
        }

        public Object GetAsset(string assetKey)
        {
            return null;
        }
        public void TestResManager()
        {
            Debug.Log("ResManager");
        }

        public void ClearAllAsset()
        {
            AssetListRes.Clear();
            ABManager.GetInstance().ClearAllAssetBundle();
        }
    }
}