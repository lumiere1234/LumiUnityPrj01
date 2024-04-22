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
