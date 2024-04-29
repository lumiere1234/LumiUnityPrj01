using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EUISortingType
{
    Default,
    FullScreen,
    FullOverlay,
    Tips,
    System,
}
public static class GlobalDef
{
    public static int MainUIHeight = 1080;
    public static int MainUIWidth = 1920;
    public static Dictionary<EUISortingType, int> SortingOrderDef = new Dictionary<EUISortingType, int>();
    public static Dictionary<int, string> QualityCardBG = new Dictionary<int, string>();

    static GlobalDef()
    {
        InitGameDefine();
    }
    private static void InitGameDefine()
    {
        SortingOrderDef.Clear();
        SortingOrderDef.Add(EUISortingType.Default, 0);
        SortingOrderDef.Add(EUISortingType.FullScreen, 500);
        SortingOrderDef.Add(EUISortingType.FullOverlay, 1500);
        SortingOrderDef.Add(EUISortingType.Tips, 2500);
        SortingOrderDef.Add(EUISortingType.System, 3500);

        QualityCardBG.Clear();
        QualityCardBG.Add(1, "Img_BoardBgGray.png");
        QualityCardBG.Add(2, "Img_BoardBgGreen.png");
        QualityCardBG.Add(3, "Img_BoardBgOrange.png");
        QualityCardBG.Add(4, "Img_BoardBgRed.png");
    }

    public static string GetQualityBGStr(int quality)
    {
        if (QualityCardBG.ContainsKey(quality))
            return QualityCardBG[quality];
        return QualityCardBG[1];
    }
}
public static class SceneDef
{
    public static string MainScene = "MainScene";
    public static string FirstStage = "FirstStage";
    public static string LumiereScene = "LumiereScene";
    public static string SampleScene = "SampleScene";
    public static string StageScene01 = "StageScene01";
    public static string StageScene02 = "StageScene02";
    public static string StageScene03 = "StageScene03";
    public static string StageScene04 = "StageScene04";
    public static string StageScene05 = "StageScene05";
    public static string StageScene06 = "StageScene06";
}
public static class BitDef
{
    public static int LoadingScene = 0x1;
    public static int LoadingAtlas = 0x2;
    public static int LoadingUI = 0x4;
    public static int Bit4 = 0x8;
}
public static class ImageDef
{
    public static string ImgStarY = "Img_StarYellow01.png";
    public static string ImgStarYH= "Img_StarHalfYellow01.png";
    public static string ImgStarBlank = "Img_StarBlank01.png";
}
public static class WorldTypeDef
{
    public static int World1 = 1;
    public static int World2 = 2;
    public static int World3 = 3;
    public static int World4 = 4;
}
public static class CharaCardTypeDef
{
    public static int Sword = 1;
    public static int Spear = 2;
    public static int Magic = 3;
    public static int Arrow = 4;
}

public enum EItemType
{ 
    Default = 0,
    Money = 1,
}
