using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text;
using UnityEngine;
using System.Xml;

public static class MainUtility
{
    public static FileInfo[] GetFiles(string path, string pattern = "*")
    {
        if (!Directory.Exists(path)) {
            return null;
        }

        DirectoryInfo directory = new DirectoryInfo(path);
        FileInfo[] files = directory.GetFiles(pattern);
        return files;
    }

    public static void ClearAllFile(string path)
    {
        if (!Directory.Exists(path))
        {
            return;
        }
        try
        {
            DirectoryInfo directory = new DirectoryInfo(path);
            FileSystemInfo[] files = directory.GetFileSystemInfos();
            foreach (FileSystemInfo item in files)
            {
                if (item is DirectoryInfo)
                {
                    DirectoryInfo subDir = new DirectoryInfo(item.FullName);
                    subDir.Delete(true);
                }
                else
                {
                    File.Delete(item.FullName);
                }
            }
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    public static void CheckAndCreateDir(string path)
    {
        DirectoryInfo directory = new DirectoryInfo(path);
        if (!directory.Exists)
        {
            directory.Create();
        }
    }
    /// <summary>
    /// 创建C#配置表文件
    /// </summary>
    public static void CreateConfigFile(FileInfo csvFile, string targetDir)
    {
        if (!File.Exists(csvFile.FullName))
            return;

        int index = csvFile.Name.LastIndexOf(".");
        string fileName = csvFile.Name.Substring(0, index);
        string confClassPath = $"{targetDir}/{fileName}.cs";

        List<string> fileData;
        ReadCsvFileTitle(csvFile.FullName, out fileData);
        if (fileData.Count < 3)
            return;

        StreamWriter sw = new StreamWriter(confClassPath);
        sw.WriteLine("using UnityEngine;\nusing System.Collections;\n");
        sw.WriteLine("public partial class " + fileName + ": GameConfigDataBase");
        sw.WriteLine("{");

        string[] nameList = fileData[0].Split(';');
        string[] typeList = fileData[1].Split(';');
        string[] descList = fileData[2].Split(';');
        if(nameList.Length == typeList.Length)
        {
            for(int i = 0; i < nameList.Length - 1; i++)
            {
                if (nameList[i].Length > 0 && typeList[i].Length > 0)
                    sw.WriteLine($"\tpublic {typeList[i]} {nameList[i]}; // {descList[i]}");
            }
        }
        sw.WriteLine($"\tprotected override string getFilePath()");
        sw.WriteLine("\t{");
        sw.WriteLine($"\t\treturn \"{fileName}\";");
        sw.WriteLine("\t}");
        sw.WriteLine("}");
        sw.Close();
    }

    public static void ReadCsvFileTitle(string path, out List<string> data)
    {
        StreamReader sr;
        data = new List<string>();
        try
        {
            using (sr = new StreamReader(path, Encoding.GetEncoding("utf-8")))
            {
                string str = "";
                while((str = sr.ReadLine()) != null)
                {
                    data.Add(str);
                }
            }
        }
        catch(Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}
