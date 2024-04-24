public enum EventDef
{
    Default = 0,

    // System 1 - 1000
    LumiFirstEvent                  = 1,
    LumiSecondEvent                 = 2,

    AudioTotalVolumeChangedEvent    = 100,

    // scene 1001 - 1200
    SceneLoadCompleteEvent          = 1001,
    LoadingStreamCompleteEvent      = 1002, // 完成loading任务

    // Dialog 1201 - 1300
    Dialog_RefreshPanel             = 1201,
}
