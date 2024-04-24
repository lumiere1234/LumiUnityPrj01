using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace CoreManager
{
    public class ABManager : SingletonAutoMono<ABManager>
    {
        private AssetBundle mainAssetBundle = null;
        private AssetBundleManifest manifest = null;
        
        private Dictionary<string, AssetBundle> abDict = new Dictionary<string, AssetBundle>();

        private string GetBundleFile(string fileName)
        {
            return $"{PathDefine.AssetBundlePath}/{fileName}";
        }
        public void LoadAB(string abName)
        {
            if (mainAssetBundle == null)
            {
                mainAssetBundle = AssetBundle.LoadFromFile(GetBundleFile(PathDefine.MainABName));
                manifest = mainAssetBundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            }

            AssetBundle ab = null;
            string[] strs = manifest.GetAllDependencies(abName);
            for(int i = 0; i < strs.Length; i++)
            {
                if (!abDict.ContainsKey(strs[i]))
                {
                    ab = AssetBundle.LoadFromFile(GetBundleFile(strs[i]));
                    abDict.Add(strs[i], ab);
                }
            }
            if (!abDict.ContainsKey(abName))
            {
                ab = AssetBundle.LoadFromFile(GetBundleFile(abName));
                abDict.Add(abName, ab);
            }
        }
        #region Load Res Sync
        public Object LoadRes(string abName, string resName)
        {
            LoadAB(abName);

            if (!abDict.ContainsKey(abName)) return null;
            return abDict[abName].LoadAsset(resName);
        }

        public Object LoadRes(string abName, string resName, System.Type type)
        {
            LoadAB(abName);

            if (!abDict.ContainsKey(abName)) return null;
            return abDict[abName].LoadAsset(resName, type);
        }

        public Object LoadRes<T>(string abName, string resName) where T : Object
        {
            LoadAB(abName);

            if (!abDict.ContainsKey(abName)) return null;
            return abDict[abName].LoadAsset<T>(resName);
        }

        public void Unload(string abName, bool clearData = false)
        {
            if(abDict.ContainsKey(abName))
            {
                abDict[abName].Unload(clearData);
                abDict.Remove(abName);
            }
        }
        public void ClearUnusedAsset(bool clearAB)
        {
            if (clearAB)
            {
                ClearAllAssetBundle(true);
            }
            Resources.UnloadUnusedAssets();
        }
        public void ClearAllAssetBundle(bool bFlag = false)
        {
            AssetBundle.UnloadAllAssetBundles(bFlag);
            abDict.Clear();

            mainAssetBundle = null;
            manifest = null;
        }
        #endregion
        #region Load Async
        public void LoadResAsync(string abName, string resName, Action<Object> callback)
        {
            StartCoroutine(ReallyLoadResAsync(abName, resName, callback));
        }
        private IEnumerator ReallyLoadResAsync(string abName, string resName, Action<Object> callback)
        {
            LoadAB(abName);

            if (!abDict.ContainsKey(abName))
            {
                yield break;
            }

            AssetBundleRequest abr = abDict[abName].LoadAssetAsync(resName);
            yield return abr;

            callback(abr.asset);
        }
        public void LoadResAsync(string abName, string resName, System.Type type, Action<Object> callback)
        {
            StartCoroutine(ReallyLoadResAsync(abName, resName, type, callback));
        }
        private IEnumerator ReallyLoadResAsync(string abName, string resName, System.Type type, Action<Object> callback)
        {
            LoadAB(abName);
            if (!abDict.ContainsKey(abName)) yield break;

            AssetBundleRequest abr = abDict[abName].LoadAssetAsync(resName, type);
            yield return abr;
            callback(abr.asset);
        }
        public void LoadResAsync<T>(string abName, string resName, UnityAction<Object> callback)
        {
            StartCoroutine(ReallyLoadResAsync<T>(abName, resName, callback));
        }
        private IEnumerator ReallyLoadResAsync<T>(string abName, string resName, UnityAction<Object> callback)
        {
            LoadAB(abName);
            if (!abDict.ContainsKey(abName)) yield break;

            AssetBundleRequest abr = abDict[abName].LoadAssetAsync<T>(resName);
            yield return abr;
            callback(abr.asset);
        }
        #endregion
    }
}
