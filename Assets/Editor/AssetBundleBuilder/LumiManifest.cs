using System.IO;
using System.Collections.Generic;
using System.Text;
using UnityEditor;
using System.Linq;
using CoreManager;

public enum EBundleInfoType
{
    Normal,
    Scenes,
    Shaders,
    Atlas,
    Music,
    Prefab,
}
public enum EBundleWriteType
{
    One,
    Many,
}

class BundleInfo
{
    public string bundleName;
    public string[] assetNames;
    public EBundleInfoType type;
    public BundleInfo(AssetBundleBuild bundle, EBundleInfoType type)
    {
        bundleName = bundle.assetBundleName;
        assetNames = bundle.assetNames;
        this.type = type;
    }
    public BundleInfo(string[] names, EBundleInfoType type)
    {
        assetNames = names;
        this.type = type;
    }
    public string GetBundleLine()
    {
        StringBuilder sb = new StringBuilder();
        if (type == EBundleInfoType.Normal)
        {
            sb.Append(bundleName);
            sb.Append('=');
            for(int i = 0; i < assetNames.Length; i++)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append(assetNames[i]);
            }
        }
        else if(type == EBundleInfoType.Scenes)
        {
            sb.Append("Scene:");
            for (int i = 0; i < assetNames.Length; i++)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append(assetNames[i]);
            }
        }
        else if(type == EBundleInfoType.Shaders)
        {
            sb.Append("Shaders:");
            for (int i = 0; i < assetNames.Length; i++)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append(assetNames[i]);
            }
        }
        else if (type == EBundleInfoType.Atlas)
        {
            sb.Append("Atlas:");
            for (int i = 0; i < assetNames.Length; i++)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append(assetNames[i]);
            }
        }
        else if (type == EBundleInfoType.Music)
        {
            sb.Append("Music:");
            for (int i = 0; i < assetNames.Length; i++)
            {
                if (i > 0)
                    sb.Append(',');
                sb.Append(assetNames[i]);
            }
        }
        return sb.ToString();
    }
}
public class LumiManifest
{
    private List<BundleInfo> infoList = new List<BundleInfo>();
    public LumiManifest()
    {
        infoList.Clear();
    }
    public void AddBundleData(AssetBundleBuild bundleData)
    {
        BundleInfo info = new BundleInfo(bundleData, EBundleInfoType.Normal);
        infoList.Add(info);
    }
    public void AddSpecialData(string[] paths, EBundleInfoType type)
    {
        BundleInfo info = new BundleInfo(paths, type);
        infoList.Add(info);
    }
    public void Clear()
    {
        infoList.Clear();
    }
    public void WriteToFile(string filePath)
    {
        FileInfo file = new FileInfo(filePath);
        if(!file.Exists)
        {
            file.Create().Close();
        }
        infoList = infoList.OrderBy(info => info.type).ToList();
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            foreach(var info in infoList)
            {
                sw.WriteLine(info.GetBundleLine());
            }
            sw.Close();
        }
    }
    public void WriteImageToFile(string filePath)
    {
        FileInfo file = new FileInfo(filePath);
        if (!file.Exists)
        {
            file.Create().Close();
        }

        if (!Directory.Exists(PathDefine.AtlasDir))
        {
            return;
        }

        string[] fileNames = Directory.GetFiles(PathDefine.AtlasDir);
        using (StreamWriter sw = new StreamWriter(filePath, true, Encoding.UTF8))
        {
            foreach (var info in fileNames)
            {
                if(info.EndsWith(".spriteatlasv2"))
                {
                    string imageName = StringUtility.GetPureName(StringUtility.GetAssetPathStringTrans(info));
                    WriteImageLine(sw, imageName, $"{PathDefine.TextureDir}/{imageName}");
                }
            }
            sw.Close();
        }
    }
    private void WriteImageLine(StreamWriter sw, string atlasName, string path)
    {
        if (!Directory.Exists(path))
        {
            return;
        }
        StringBuilder sb = new StringBuilder();
        sb.Append(atlasName);
        sb.Append('=');
        string[] fileNames = Directory.GetFiles(path);
        bool bFirst = true;
        foreach(var fileName in fileNames)
        {
            if(!fileName.EndsWith(".meta"))
            {
                if (!bFirst)
                    sb.Append(',');
                else
                    bFirst = false;
                sb.Append(fileName.Substring(StringUtility.GetAssetPathStringTrans(fileName).LastIndexOf('/') + 1));
            }
        }
        sw.WriteLine(sb.ToString());
    }
}
