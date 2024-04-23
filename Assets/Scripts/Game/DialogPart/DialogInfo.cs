using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogInfo
{
    public int dialogId;
    public DialogDataCfg dataCfg;
    private DialogCharaCfg _charaCfg;
    public EDialogChatType DialogType
    {
        get
        {
            return dataCfg != null ? (EDialogChatType)dataCfg.speakerType : EDialogChatType.Default;
        }
    }
    public string ActionStr => dataCfg != null ? dataCfg.action : string.Empty;
    public DialogCharaCfg charaCfg
    {
        get
        {
            if (dataCfg.speakerType != (int)EDialogChatType.Character)
            {
                return null;
            }
            if (_charaCfg == null)
            {
                int charaId = dataCfg == null ? 0 : dataCfg.character;
                _charaCfg = GameConfigDataBase.GetConfigData<DialogCharaCfg>(charaId.ToString());
            }
            return _charaCfg;
        }
    }
    public DialogInfo()
    {
        dialogId = 0;
        dataCfg = null;
    }
    public DialogInfo(int dialogId)
    {
        this.dialogId = dialogId;
        dataCfg = GameConfigDataBase.GetConfigData<DialogDataCfg>(dialogId.ToString());
    }
    public int GetNextId()
    {
        return dataCfg != null ? dataCfg.nextDialog : 0;
    }
    public float GetAutoTime()
    {
        return dataCfg != null ? dataCfg.autoTime : -1;
    }
    
}
