using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace CoreManager
{
    public static class PathDefine
    {
        // Excel file path
        public static string ExcelDir => $"{Directory.GetParent(Application.dataPath)}/Configs";
        // CSV file Path
        public static string TargetDir => $"{Application.dataPath}/ConfigCSV";
        // CSV->C# file Path
        public static string ConfigClassDir => $"{Application.dataPath}/Scripts/ConfigClass";
        public static string GetAssetPath(string path)
        {
            return path.Substring(path.IndexOf("Assets"));
        }
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
