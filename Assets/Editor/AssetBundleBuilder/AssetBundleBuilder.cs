using CoreManager;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

public enum EPutInBundleType
{
    Directory, // file directory
    OneByOne, // file single by single
}

public abstract class AssetBundleBuilder
{

}

public abstract class AssetBundleBuilder<T> : AssetBundleBuilder
{ 
    protected LumiManifest manifestInfo = new LumiManifest();
    protected List<AssetBundleBuild> assetBundleBuilds = new List<AssetBundleBuild>();
    public virtual void DoBuildAssetBundle()
    {
        manifestInfo.Clear();
        // SetBundleName
        PutInBundleName();
        // manifest 
        CreateManifestFile();
        // image relation
        CreateImageRelation();
        // Create AssetBundle
        DoBuildAction();
    }
    public virtual void DoBuildAction()
    {
        // do in child class
    }
    // 将asset分类装入bundlemap
    public virtual void PutInBundleName()
    {
        // AddScene
        DoPutInScene();
        // AddShaders
        DoPutInShaders();
        // AddAtlas
        DoPutInAtlas();
        // GameUI
        DoPutInAllSubFiles(PathDefine.GameUIDir, EBundleInfoType.Normal, EBundleWriteType.One);
        // Audio
        DoPutInAllSubFiles(PathDefine.AudioDir, EBundleInfoType.Music, EBundleWriteType.Many);
        // Prefab
        DoPutInAllSubFiles(PathDefine.PrefabDir, EBundleInfoType.Normal, EBundleWriteType.One);
    }
    // 写入manifest
    public virtual void CreateManifestFile()
    {
        string path = $"{PathDefine.AssetBundlePath}/{PathDefine.LumiManifestName}";
        manifestInfo.WriteToFile(path);
    }
    public virtual void CreateImageRelation()
    {
        string path = $"{PathDefine.AssetBundlePath}/{PathDefine.LumiImageRelation}";
        manifestInfo.WriteImageToFile(path);
    }
    public virtual void DoPutInScene()
    {
        if (!Directory.Exists(PathDefine.SceneDir))
        {
            return;
        }
        List<string> nameList = new List<string>();
        string[] files = Directory.GetFiles(PathDefine.SceneDir);
        foreach (var file in files)
        {
            if (file.EndsWith(".unity"))
            {
                string fileStr = StringUtility.GetAssetPathStringTrans(file.Trim());
                string assetStr = fileStr.Substring(file.LastIndexOf("Assets"));
                AssetBundleBuild build = new AssetBundleBuild();
                string pureStr = StringUtility.GetPureName(assetStr);
                build.assetBundleName = $"{pureStr}";
                build.assetNames = new string[] { assetStr };
                assetBundleBuilds.Add(build);
                nameList.Add(assetStr);
            }
        }
        manifestInfo.AddSpecialData(nameList.ToArray(), EBundleInfoType.Scenes);
    }
    public virtual void DoPutInShaders()
    {
        if (!Directory.Exists(PathDefine.ShaderDir))
        {
            return;
        }
        List<string> nameList = new List<string>();
        string[] files = Directory.GetFiles(PathDefine.ShaderDir);
        foreach (var file in files)
        {
            if (file.EndsWith(".shader"))
            {
                string fileStr = StringUtility.GetAssetPathStringTrans(file.Trim());
                string assetStr = fileStr.Substring(file.LastIndexOf("Assets"));
                AssetBundleBuild build = new AssetBundleBuild();
                string pureStr = StringUtility.GetPureName(assetStr);
                build.assetBundleName = pureStr;
                build.assetNames = new string[] {assetStr};
                assetBundleBuilds.Add(build);
                nameList.Add(assetStr);
            }
        }
        manifestInfo.AddSpecialData(nameList.ToArray(), EBundleInfoType.Shaders);
    }
    public virtual void DoPutInAtlas()
    {
        if (!Directory.Exists(PathDefine.AtlasDir))
        {
            return;
        }
        List<string> nameList = new List<string>();
        string[] files = Directory.GetFiles(PathDefine.AtlasDir);
        foreach (var file in files)
        {
            if (file.EndsWith(".spriteatlasv2"))
            {
                string fileStr = StringUtility.GetAssetPathStringTrans(file.Trim());
                string assetStr = fileStr.Substring(file.LastIndexOf("Assets"));
                AssetBundleBuild build = new AssetBundleBuild();
                string pureStr = StringUtility.GetPureName(assetStr);
                build.assetBundleName = $"{pureStr}";
                build.assetNames = new string[] { assetStr };
                assetBundleBuilds.Add(build);
                nameList.Add(assetStr);
            }
        }
        manifestInfo.AddSpecialData(nameList.ToArray(), EBundleInfoType.Atlas);
    }
    //public void DoPutInGameUI()
    //{
    //    if (!Directory.Exists(PathDefine.GameUIDir))
    //    {
    //        return;
    //    }
    //    string[] files = Directory.GetFiles(PathDefine.GameUIDir);
    //    foreach (var file in files)
    //    {
    //        if (file.EndsWith(".prefab"))
    //        {
    //            string fileStr = StringUtility.GetAssetPathStringTrans(file.Trim());
    //            string assetStr = fileStr.Substring(file.LastIndexOf("Assets"));
    //            AssetBundleBuild build = new AssetBundleBuild();
    //            string pureStr = StringUtility.GetPureName(assetStr);
    //            build.assetBundleName = pureStr.ToLower();
    //            build.assetNames = new string[] { assetStr };
    //            assetBundleBuilds.Add(build);
    //            manifestInfo.AddBundleData(build);
    //        }
    //    }
    //}
    public void DoPutInAllSubFiles(string path, EBundleInfoType bType, EBundleWriteType wType)
    {
        if (!Directory.Exists(path))
        {
            return;
        }
        List<string> nameList = new List<string>();
        Queue<string> dirQueue = new Queue<string>();
        dirQueue.Enqueue(path);
        while (dirQueue.Count > 0)
        {
            string dirStr = dirQueue.Dequeue();
            DirectoryInfo dirInfo = new DirectoryInfo(dirStr);
            if (!dirInfo.Exists) { continue; }
            FileSystemInfo[] fsInfos = dirInfo.GetFileSystemInfos();
            foreach (FileSystemInfo fsi in fsInfos)
            {
                if (fsi is DirectoryInfo)
                {
                    dirQueue.Enqueue(fsi.FullName);
                }
                else if (!fsi.FullName.EndsWith(".meta"))
                {
                    string fileStr = StringUtility.GetAssetPathStringTrans(fsi.FullName.Trim());
                    string assetStr = fileStr.Substring(fileStr.LastIndexOf("Assets"));
                    AssetBundleBuild build = new AssetBundleBuild();
                    string pureStr = StringUtility.GetPureName(assetStr);
                    build.assetBundleName = pureStr.ToLower();
                    build.assetNames = new string[] { assetStr };
                    assetBundleBuilds.Add(build);

                    if (wType == EBundleWriteType.One)
                        manifestInfo.AddBundleData(build);
                    else
                        nameList.Add(assetStr);      
                }
            }
        }
        if (wType == EBundleWriteType.Many)
            manifestInfo.AddSpecialData(nameList.ToArray(), bType);
    }
}