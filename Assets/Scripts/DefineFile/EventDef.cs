public enum EventDef
{
    Default = 0,

    // System 1 - 1000
    LumiFirstEvent                  = 1,
    LumiSecondEvent                 = 2,

    AudioTotalVolumeChangedEvent    = 100,

    // scene 1001 - 1200
    SceneLoadCompleteEvent          = 1001,

    Scene_LoadingTaskAdd            = 1003, // 设置laoding任务
    Scene_LoadingTaskComplete       = 1004, // 完成loading任务

    // Dialog 1201 - 1300
    Dialog_RefreshPanel             = 1201,

    // Main Window 1301 - 1400
    Main_ChangeDisplayHeroine       = 1301, // 更换展示主角

    // Chara card 1401 - 1500
    CharaCard_AddCard               = 1401,
    CharaCard_RemoveCard            = 1402,
    CharaCard_CardChange            = 1403,
}
