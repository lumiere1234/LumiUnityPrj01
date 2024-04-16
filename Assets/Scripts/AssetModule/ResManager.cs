using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace CoreManager
{
    public enum ResType
    {
        ResSingle, // single res
        AssetBundle, // AB包资源
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
        /// <summary>
        /// 获取普通资源
        /// </summary>
        public Object GetAsset(string assetKey, Type type)
        {
            if (AssetListRes.ContainsKey(assetKey))
            {
                return AssetListRes[assetKey].Asset;
            }

#if UNITY_EDITOR
            var asset = AssetDatabase.LoadAssetAtPath(assetKey, type);
#else
            // todo add assetbundle
#endif
            if (asset == null) {
                CoreResource coreRes = new CoreResource(curType, assetKey, asset);
                AssetListRes.Add(assetKey, coreRes);
            }
            return asset;
        }
        /// <summary>
        /// 异步获得asset
        /// </summary>
        /// <param name="assetKey"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public void GetAssetAsync(string assetKey, Type type, Action<Object> loadFunc)
        {
#if UNITY_EDITOR
            StartCoroutine(DoGetAssetAsync(assetKey, type, loadFunc));   
#else
            // todo load from assetbundle
#endif 
        }

        IEnumerator DoGetAssetAsync(string assetKey, Type type, Action<Object> loadFunc)
        {
            string requestPath = $"{Directory.GetParent(Application.dataPath)}/{assetKey}";
            UnityWebRequest req = UnityWebRequest.Get(requestPath);
            yield return req.SendWebRequest();
            byte[] data = req.downloadHandler.data;

            //if (req.isDone) {req.downloadHandler.data
            //    loadFunc(req.downloadHandler.data);
            //}
        }

        /// <summary>
        /// 异步加载场景
        /// </summary>
        public void LoadScene(string sceneName, Action callBack)
        {
#if UNITY_EDITOR
            StartCoroutine(DoLoadSceneAsync(sceneName, callBack));
#else
            // todo load from assetbundle
#endif
        }
        IEnumerator DoLoadSceneAsync(string sceneName, Action callBack)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            if (callBack != null)
                callBack();
            yield return Resources.UnloadUnusedAssets();
        }
        public void ClearAllAsset()
        {
            AssetListRes.Clear();
            ABManager.GetInstance().ClearAllAssetBundle();
        }
    }
}