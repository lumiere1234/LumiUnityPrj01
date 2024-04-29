using System.IO;
using UnityEngine;

namespace CoreManager
{
    public static class PathDefine
    {
        // Excel file path
        public static string ExcelDir => $"{Directory.GetParent(Application.dataPath)}/Configs";
        // CSV file Path
        public static string TargetDir => $"{Application.streamingAssetsPath}/ConfigCSV";
        // CSV->C# file Path
        public static string ConfigClassDir => $"{Application.dataPath}/Scripts/ConfigClass";
        public static string AtlasBaseDir => $"{Application.dataPath}/GameRes/ImgAtlas";
        public static string SpriteBaseDir => $"{Application.dataPath}/Texture";
        public static string GetAssetPath(string path)
        {
            return path.Substring(path.IndexOf("Assets"));
        }
        public static string BundleBaseDir => $"{Directory.GetParent(Application.dataPath)}/AssetBundles";
        public static string BundleSubWindow => "LumiPC";
        public static string BundleSubAndroid => "Android";
        public static string BundleSubIOS => "IOS";
        public static string MainABName
        {
            get
            {
#if UNITY_IOS
                return BundleSubIOS;
#elif UNITY_ANDROID
                return BundleSubAndroid;
#else
                return BundleSubWindow;
#endif
            }
        }
        public static string AssetBundlePath => $"{BundleBaseDir}/{MainABName}";
        public static string LumiManifestName => "LumiManifest";
        public static string LumiImageRelation => "LumiImageRelation";
        public static string SceneDir => $"{Application.dataPath}/GameRes/Scene";
        public static string ShaderDir => $"{Application.dataPath}/GameRes/Shaders";
        public static string AtlasDir => $"{Application.dataPath}/GameRes/ImgAtlas";
        public static string GameUIDir => $"{Application.dataPath}/GameRes/GameUI";
        public static string AudioDir => $"{Application.dataPath}/GameRes/Audio";
        public static string PrefabDir => $"{Application.dataPath}/GameRes/Prefab";
        public static string TextureDir => $"{Application.dataPath}/Texture";
        public static string GetAtlasPathInBundle(string atlasName)
        {
            return $"Assets/GameRes/ImgAtlas/{atlasName}.spriteatlasv2";
        }
        public static string GetTexturePath(string atlasName, string imageName)
        {
            return $"Assets/Texture/{atlasName}/{imageName}";
        }
        public static string SaveDataDir => $"{Directory.GetParent(Application.dataPath)}/SaveData";
    }
}
