using CoreManager;
using System.Collections;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using UnityEditor;
using UnityEngine;

public class BuildTools : Editor
{
    [MenuItem("Bundle/BuildWindows")]
    public static void BuildWindowsBundle()
    {
        // CreateBundleFile
        string bundleDir = PathDefine.BundleBaseDir;
        if (!Directory.Exists(bundleDir))
        {
            Directory.CreateDirectory(bundleDir);
            AssetDatabase.Refresh();
        }
        string bundleMainDir = $"{PathDefine.BundleBaseDir}/{PathDefine.BundleSubWindow}";
        if (!Directory.Exists(bundleMainDir))
        {
            Directory.CreateDirectory(bundleMainDir);
            AssetDatabase.Refresh();
        }
        // Directory Clear
        DirectoryInfo directory = new DirectoryInfo(bundleMainDir);
        FileInfo[] files = directory.GetFiles();
        foreach(var file in files)
        {
            File.Delete(file.FullName);
        }

        WindowsBuilder windowsBuilder = new WindowsBuilder();
        windowsBuilder.DoBuildAssetBundle();
    }
}
