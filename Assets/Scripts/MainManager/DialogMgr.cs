using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EDialogChatType
{
    Default = 0,
    Character = 1,
    Normal = 2,
    Muitl = 3,
}
public class DialogMgr : SingletonAutoMono<DialogMgr>
{
    private DialogInfo _curDialog;
    public DialogInfo CurDialog { get { return _curDialog; } }
    public UIDialog m_UIDialog { get; set; } = null;
    private bool _bAutoFlag;
    public bool bAutoFlag { get { return _bAutoFlag; } }

    private float mTimeTarget = -1;
    private float mCurTime = 0;

    private void Update()
    {
        if (bAutoFlag && mTimeTarget > 0)
        {
            mCurTime += Time.deltaTime;
            if (mCurTime > mTimeTarget)
            {
                mCurTime = 0;
                mTimeTarget = -1;
                DoNextDialog();
            }
        }
    }
    public void DoStartDialog(int dialogId)
    {
        _curDialog = new DialogInfo(dialogId);
        if (m_UIDialog == null)
        {
            UIMgr.GetInstance().ShowPanel(UIDef.UIDialog);
        }
        else
        {
            EventMgr.GetInstance().Invoke(EventDef.Dialog_RefreshPanel);
        }
        if (bAutoFlag)
            DoStartAuto();
    }
    public DialogInfo GetDialogInfo(int dialogId)
    {
        DialogInfo dialogInfo = new DialogInfo(dialogId);
        return dialogInfo;
    }
    public void DoNextDialog()
    {
        if (CurDialog == null) return;
        // do dialog end event
        if (!CurDialog.ActionStr.Equals(string.Empty))
        {
            GMOpeMgr.GetInstance().DoOperation(CurDialog.ActionStr);
        }

        int nextId = CurDialog.GetNextId();
        if (nextId == 0)
        {
            _curDialog = null;
            UIMgr.GetInstance().HidePanel(UIDef.UIDialog);
            return;
        }
        DoStartDialog(nextId);
    }
    public void DoSetAuto(bool bAuto)
    {
        _bAutoFlag = bAuto;
        if (bAuto)
        {
            DoStartAuto();
        }
    }
    public void DoStartAuto()
    {
        mTimeTarget = CurDialog.GetAutoTime();
        mCurTime = 0;
    }
}
