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
    public static Dictionary<EUISortingType, int> SortingOrderDef = new Dictionary<EUISortingType, int>();

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
