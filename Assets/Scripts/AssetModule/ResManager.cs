using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using Object = UnityEngine.Object;

namespace CoreManager
{
    public enum ResType
    {
        ResSingle, // single res
        AssetBundle, // AB����Դ
    }
    public class ResManager : SingletonAutoMono<ResManager>
    {
        public ResType curType;
        public static ABInfo abInfo = null;
        // key, resource
        public static Dictionary<string, CoreResource> AssetListRes = new Dictionary<string, CoreResource>();
        private void Awake()
        {
#if UNITY_EDITOR
            curType = ResType.ResSingle;
#else
            curType = ResType.AssetBundle;
#endif
            EventMgr.GetInstance().Register(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
        }

        public void InitRes()
        {
            // ��ʼ������
            InitABInfoList();
            // load All Shaders
            InitAllShaders();
            // ��ʼ��ͼƬ����
            InitImageSpriteIndex();
        }
        public void InitAllShaders()
        {
            var shaderList = abInfo.ShaderDict;
            if(shaderList != null && shaderList.Count > 0)
            {
                foreach(var info in shaderList)
                {
                    Shader shader = ABManager.GetInstance().LoadRes<Shader>(info.Value, info.Key) as Shader;
                }
            }
        }
        public void InitABInfoList()
        {
            abInfo = new ABInfo();

            string filePath = $"{PathDefine.AssetBundlePath}/{PathDefine.LumiManifestName}";
            FileInfo file = new FileInfo(filePath);
            if(!file.Exists)
            {
                return;
            }

            using (StreamReader sr = new StreamReader(filePath, Encoding.UTF8))
            {
                string strLine;
                while ((strLine = sr.ReadLine()) != null)
                {
                    abInfo.ReadLine(strLine);
                }
                sr.Close();
            }
        }
        /// <summary>
        /// ��ȡ��ͨ��Դ
        /// </summary>
        public Object GetAsset(string assetKey, Type type)
        {
            if (AssetListRes.ContainsKey(assetKey))
            {
                return AssetListRes[assetKey].Asset;
            }

//#if UNITY_EDITOR
//            var asset = UnityEditor.AssetDatabase.LoadAssetAtPath(assetKey, type);
//#else
            string bundleName = abInfo.GetBundleNameByAssetPath(assetKey);
            var asset = ABManager.GetInstance().LoadRes(bundleName, assetKey, type);
//#endif
            if (asset != null) {
                CoreResource coreRes = new CoreResource(curType, assetKey, asset);
                AssetListRes.Add(assetKey, coreRes);
            }
            return asset;
        }
        /// <summary>
        /// �첽���asset
        /// </summary>
        /// <param name="assetKey"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public void GetAssetAsync(string assetKey, Type type, Action<Object> loadFunc)
        {
//#if UNITY_EDITOR
//            StartCoroutine(DoGetAssetEditorAsync(assetKey, type, loadFunc));
//#else
            DoGetAssetBundleAsync(assetKey, type, loadFunc);
//#endif
        }
#if UNITY_EDITOR
        IEnumerator DoGetAssetEditorAsync(string assetKey, Type type, Action<Object> loadFunc)
        {
            Object asset = null;
            if (AssetListRes.ContainsKey(assetKey))
            {
                asset = AssetListRes[assetKey].Asset;
            }
            if (asset == null)
            {
                asset = UnityEditor.AssetDatabase.LoadAssetAtPath(assetKey, type);
            }
            if (asset != null)
            {
                CoreResource coreRes = new CoreResource(curType, assetKey, asset);
                AssetListRes.Add(assetKey, coreRes);
            }
            yield return null;
            if (loadFunc != null)
            {
                loadFunc.Invoke(asset);
            }
        }
#endif
        void DoGetAssetBundleAsync(string assetKey, Type type, Action<Object> loadFunc)
        {
            Object asset = null;
            if (AssetListRes.ContainsKey(assetKey))
            {
                asset = AssetListRes[assetKey].Asset;
                loadFunc.Invoke(asset);
            }
            string bundleName = abInfo.GetBundleNameByAssetPath(assetKey);
            ABManager.GetInstance().LoadResAsync(bundleName, assetKey, type, (obj) =>
            {
                if (obj != null)
                {
                    CoreResource coreRes = new CoreResource(curType, assetKey, obj);
                    AssetListRes.Add(assetKey, coreRes);

                    loadFunc?.Invoke(obj);
                }
            });
        }
        /// <summary>
        /// �첽���س���
        /// </summary>
        public void LoadScene(string sceneName, Action callBack)
        {
#if UNITY_EDITOR
            StartCoroutine(DoLoadSceneEditorAsync(sceneName, callBack));
#else
            StartCoroutine(DoLoadSceneInBundleAsync(sceneName, callBack));
#endif
        }
#if UNITY_EDITOR
        IEnumerator DoLoadSceneEditorAsync(string sceneName, Action callBack)
        {
            yield return SceneManager.LoadSceneAsync(sceneName);
            if (callBack != null)
                callBack();
        }
#endif
        IEnumerator DoLoadSceneInBundleAsync(string sceneName, Action callBack)
        {
            string bundleName = $"scene{sceneName.ToLower()}";

            string bundlePath = $"{PathDefine.AssetBundlePath}/{bundleName}";
            UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle($"file://{bundlePath}");
            yield return req.SendWebRequest();
            if (req.isDone)
            {
                AssetBundle ab = (req.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
                yield return SceneManager.LoadSceneAsync(sceneName);
                if (callBack != null) callBack();

                ab.Unload(false);
                Resources.UnloadUnusedAssets();
            }
        }
#region Sprite
        private Dictionary<string, SpriteAtlas> atlasList = new Dictionary<string, SpriteAtlas>();
        private Dictionary<string, string> imageAtlasDict = new Dictionary<string, string>();
        public Sprite LoadSprite(string spriteName)
        {
            if (!imageAtlasDict.ContainsKey(spriteName))
            {
                return null;
            }
            string atlasName = imageAtlasDict[spriteName];
            
            if (!atlasList.ContainsKey(atlasName))
            {
#if UNITY_EDITOR
                LoadAtlasInEditor(atlasName);
#else
                LoadAtlasInBundle(atlasName);
#endif
            }
            if (atlasList.ContainsKey(atlasName))
            {
                string[] nameSplit = spriteName.Split('.');
                if (nameSplit.Length > 0)
                    return atlasList[atlasName].GetSprite(nameSplit[0]);
            }
            return null;
        }
#if UNITY_EDITOR
        private void LoadAtlasInEditor(string atlasName)
        {
            string atlasPath = $"{PathDefine.AtlasDir}/{atlasName}.spriteatlasv2";
            atlasPath = atlasPath.Substring(atlasPath.LastIndexOf("Assets"));
            SpriteAtlas sa = GetAsset(atlasPath, typeof(SpriteAtlas)) as SpriteAtlas;
            atlasList.Add(atlasName, sa);
        }
#endif
        private void LoadAtlasInBundle(string atlasName)
        {
            string bundleName = $"atlas{atlasName.ToLower()}";
            string atlasPath = PathDefine.GetAtlasPathInBundle(atlasName);
            SpriteAtlas atlas = ABManager.GetInstance().LoadRes(bundleName, atlasPath, typeof(SpriteAtlas)) as SpriteAtlas;
            if (atlas != null)
            {
                atlasList.Add(atlasName, atlas);
            }
        }
        private void InitImageSpriteIndex()
        {
            imageAtlasDict.Clear();
            string filePath = $"{PathDefine.AssetBundlePath}/{PathDefine.LumiImageRelation}";
            if(!File.Exists(filePath))
            {
                return;
            }
            using(StreamReader sr = new StreamReader(filePath))
            {
                string strLine;
                while((strLine = sr.ReadLine()) != null)
                {
                    ReadImageRelationLine(strLine);
                }
                sr.Close();
            }
        }
        private void ReadImageRelationLine(string strLine)
        {
            string[] strSplit = strLine.Trim().Split('=');
            if (strSplit.Length < 2)
            {
                return;
            }
            string[] nameSplit = strSplit[1].Split(',');
            foreach(var name in nameSplit)
            {
                imageAtlasDict.Add(name, strSplit[0]);
            }
        }
#endregion
        public void ClearAllAsset()
        {
            AssetListRes.Clear();
            ABManager.GetInstance().ClearAllAssetBundle();
        }
        private void OnDestroy()
        {
            EventMgr.GetInstance().UnRegister(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
        }
        // event
        private void OnSceneLoadedComplete(params object[] args)
        {
            
        }
    }
}