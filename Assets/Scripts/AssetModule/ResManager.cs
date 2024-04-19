using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.U2D;
using UnityEngine.Windows.Speech;
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
            // 初始化图集
            InitSpriteAtlas();
            EventMgr.GetInstance().Register(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
        }

        private void Start()
        {

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
            if (asset != null) {
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
            StartCoroutine(DoGetAssetEditorAsync(assetKey, type, loadFunc));   
#else
            // todo load from assetbundle
#endif 
        }

        IEnumerator DoGetAssetEditorAsync(string assetKey, Type type, Action<Object> loadFunc)
        {
            Object asset = null;
            if (AssetListRes.ContainsKey(assetKey))
            {
                asset = AssetListRes[assetKey].Asset;
            }
            if (asset == null)
            {
                asset = AssetDatabase.LoadAssetAtPath(assetKey, type);
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

        #region Sprite
        private Dictionary<string, SpriteAtlas> atlasList = new Dictionary<string, SpriteAtlas>();
        private Dictionary<string, string> spriteNameDict = new Dictionary<string, string>();
        public Sprite LoadSprite(string spriteName, string atlasName)
        {
            Sprite sprite = GetAtlasByName(atlasName)?.GetSprite(spriteName);
            if (sprite != null && !spriteNameDict.ContainsKey(spriteName))
            {
                spriteNameDict.Add(spriteName, atlasName);
            }
            return sprite;
        }
        public Sprite LoadSprite(string spriteName)
        {
            Sprite sprite = null;
#if UNITY_EDITOR
            if (spriteNameDict.ContainsKey(spriteName))
            {
                return GetAtlasByName(spriteNameDict[spriteName])?.GetSprite(spriteName);
            }

            foreach (var atlas in atlasList)
            {
                sprite = atlas.Value.GetSprite(spriteName);
                if (sprite != null)
                {
                    spriteNameDict.Add(spriteName, atlas.Key);
                    break;
                }
            }
#else
            // todo find sprite from assetbundle
#endif
            return sprite;
        }
        private void InitSpriteAtlas()
        {
            spriteNameDict.Clear();
            atlasList.Clear();
            if(!Directory.Exists(PathDefine.AtlasBaseDir))
            {
                return;
            }
            string[] allAtlas = Directory.GetFiles(PathDefine.AtlasBaseDir, "*.spriteatlasv2");
            foreach(var atlas in allAtlas)
            {
                string atlasPath = atlas.Substring(atlas.LastIndexOf("Assets"));
                SpriteAtlas sa = GetAsset(atlasPath, typeof(SpriteAtlas)) as SpriteAtlas;
                atlasList[sa.name] = sa;
            }
        }
        private SpriteAtlas GetAtlasByName(string name)
        {
            if(atlasList.ContainsKey(name))
            {
                return atlasList[name];
            }
            return null;
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