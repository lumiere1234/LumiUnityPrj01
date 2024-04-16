using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using CoreManager;

public class ExcelToCSV : Editor
{
    [MenuItem("Tools/Excel/ExcelToCSV")]
    public static void DoExcelToCSV()
    {
        // clear all files
        MainUtility.ClearAllFile(PathDefine.TargetDir);
        MainUtility.ClearAllFile(PathDefine.ConfigClassDir);
        
        FileInfo[] files = MainUtility.GetFiles(PathDefine.ExcelDir, "*.xlsx");
        for (int i = 0; i < files.Length; i++)
        {
            ExcelUtility eu = new ExcelUtility(files[i].FullName);
            Encoding encoding = Encoding.GetEncoding("utf-8");
            eu.ConvertAllSheetToCSV(PathDefine.TargetDir, encoding);
        }

        AssetDatabase.Refresh();
        Debug.Log("Lumiere ExcelToCSV success!");

        DoCreateCSConfig();
    }
    [MenuItem("Tools/Excel/Create Config Class")]
    public static void DoCreateCSConfig()
    {
        MainUtility.CheckAndCreateDir(PathDefine.TargetDir);
        MainUtility.CheckAndCreateDir(PathDefine.ConfigClassDir);

        FileInfo[] csvFiles = MainUtility.GetFiles(PathDefine.TargetDir, "*.csv");
        for (int i = 0; i < csvFiles.Length; i++)
        {
            MainUtility.CreateConfigFile(csvFiles[i], PathDefine.ConfigClassDir);
        }
        AssetDatabase.Refresh();
        Debug.Log("Lumiere Generate Config Class success!");
    }
}
