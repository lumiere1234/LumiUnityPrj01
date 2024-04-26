public enum EventDef
{
    Default = 0,

    // System 1 - 1000
    LumiFirstEvent                  = 1,
    LumiSecondEvent                 = 2,

    AudioTotalVolumeChangedEvent    = 100,

    // scene 1001 - 1200
    SceneLoadCompleteEvent          = 1001,

    LoadingStreamAddTaskEvent       = 1003, // 设置laoding任务
    LoadingStreamCompleteEvent      = 1004, // 完成loading任务

    // Dialog 1201 - 1300
    Dialog_RefreshPanel             = 1201,
    
    // Main Window
    ChangeMainHeroineId             = 1301, // 更换展示主角
}
