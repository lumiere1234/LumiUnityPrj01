using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreManager
{
    public class ABInfo
    {
        private Dictionary<string, string> infoDict = new Dictionary<string, string>();
        private Dictionary<string, string> nickNameDict = new Dictionary<string, string>(); // xxx => infoDict.key
        private Dictionary<string, string> sceneDict = new Dictionary<string, string>();
        private Dictionary<string, string> _shaderDict = new Dictionary<string, string>();
        public Dictionary<string, string> ShaderDict => _shaderDict;
        public ABInfo()
        {
            infoDict.Clear();
            sceneDict.Clear();
            _shaderDict.Clear();
        }
        private void AddNickName(string assetName)
        {
            string nameStr = StringUtility.GetNameWithType(assetName);
            nickNameDict.Add(nameStr, assetName);
        }
        public void ReadLine(string strLine)
        {
            if (strLine == null)
                return;
            if (strLine.StartsWith("Shaders:"))
            {
                string shaderStr = strLine.Trim().Substring(8);
                string[] splitStr = shaderStr.Split(',');
                foreach (var str in splitStr)
                {
                    string pureStr = StringUtility.GetPureName(str);
                    _shaderDict.Add(str, pureStr.ToLower());
                }
            }
            if (strLine.StartsWith("Scene:"))
            {
                string sceneStr = strLine.Trim().Substring(6);
                string[] splitStr = sceneStr.Split(',');
                foreach(var str in splitStr)
                {
                    string pureStr = StringUtility.GetPureName(str);
                    sceneDict.Add($"{pureStr.ToLower()}", str);
                }
            }
            else if (strLine.StartsWith("Atlas:"))
            {
                string atlasStr = strLine.Trim().Substring(6);
                string[] splitStr = atlasStr.Split(',');
                foreach (var str in splitStr)
                {
                    string pureStr = StringUtility.GetPureName(str);
                    infoDict.Add(str, $"{pureStr.ToLower()}");
                }
            }
            else if (strLine.StartsWith("Music:"))
            {
                string musicStr = strLine.Trim().Substring(6);
                string[] splitStr = musicStr.Split(',');
                foreach (var str in splitStr)
                {
                    string pureStr = StringUtility.GetPureName(str);
                    infoDict.Add(str, $"{pureStr.ToLower()}");

                    AddNickName(str);
                }
            }
            else
            {
                string[] splitStr = strLine.Trim().Split('=');
                if (splitStr.Length >= 2)
                {
                    infoDict.Add(splitStr[1], splitStr[0]);
                    AddNickName(splitStr[1]);
                }
            }
        }
        public string CheckIsNickName(string nameShort)
        {
            if (nickNameDict.ContainsKey(nameShort))
            {
                return nickNameDict[nameShort];
            }
            return null;
        }
        public string GetBundleNameByAssetPath(string path)
        {
            if (infoDict.ContainsKey(path))
            { 
                return infoDict[path];
            }
            return "";
        }
        public string GetSceneAssetPath(string bundleName)
        {
            if (sceneDict.ContainsKey(bundleName))
            {
                return sceneDict[bundleName];
            }
            return "";
        }
    }
}
