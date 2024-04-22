using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialogMgr : SingletonAutoMono<DialogMgr>
{
    public DialogInfo GetDialogInfo(int dialogId)
    {
        DialogInfo dialogInfo = new DialogInfo(dialogId);
        return dialogInfo;
    }
}
