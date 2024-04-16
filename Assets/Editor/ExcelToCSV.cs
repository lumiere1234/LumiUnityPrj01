using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class ExcelToCSV : Editor
{
    [MenuItem("Tools/Excel/ExcelToCSV")]
    public static void DoExcelToCSV()
    {
        // clear all files
        MainUtility.ClearAllFile(MainUtility.TargetDir);
        MainUtility.ClearAllFile(MainUtility.ConfigClassDir);
        MainUtility.CheckAndCreateDir(MainUtility.TargetDir);
        MainUtility.CheckAndCreateDir(MainUtility.ConfigClassDir);

        FileInfo[] files = MainUtility.GetFiles(MainUtility.ExcelDir, "*.xlsx");
        for (int i = 0; i < files.Length; i++)
        {
            ExcelUtility eu = new ExcelUtility(files[i].FullName);
            Encoding encoding = Encoding.GetEncoding("utf-8");
            eu.ConvertAllSheetToCSV(MainUtility.TargetDir, encoding);
        }

        AssetDatabase.Refresh();
        Debug.Log("Lumiere ExcelToCSV success!");
    }
    [MenuItem("Tools/Excel/Create Config Class")]
    public static void DoCreateCSConfig()
    {
        FileInfo[] csvFiles = MainUtility.GetFiles(MainUtility.TargetDir, "*.csv");
        for (int i = 0; i < csvFiles.Length; i++)
        {
            MainUtility.CreateConfigFile(csvFiles[i], MainUtility.ConfigClassDir);
        }
        AssetDatabase.Refresh();
        Debug.Log("Lumiere Generate Config Class success!");
    }
}
