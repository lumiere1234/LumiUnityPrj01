using System;
using UnityEngine;

public class ConfirmInfo
{
    public string Title;
    public string Content;
    public string ConfirmStr;
    public string CancelStr;
    public bool bShowConfirm = true;
    public bool bShowCancel = true;
    public Action ConfirmCallback;
    public Action CancelCallback;

    public bool bForceFirst = false;
}

public class TipsMgr : SingletonAutoMono<TipsMgr>
{

    #region Confirm Show
    public void ShowConfirmMain(string _content, string _title, Action _confirmBack, Action _cancelBack, string _confirmStr, string _cancelStr, bool _bForceFirst = false, bool _showConfirm = true, bool _showCancel = true)
    {
        ConfirmInfo info = new ConfirmInfo();
        info.Title = _title;
        info.Content = _content;
        info.ConfirmStr = _confirmStr;
        info.CancelStr = _cancelStr;
        info.ConfirmCallback = _confirmBack;
        info.CancelCallback = _cancelBack;
        info.bShowConfirm = _showConfirm;
        info.bShowCancel = _showCancel;
        info.bForceFirst = _bForceFirst;

        UIMgr.Instance.ShowPanel(UIDef.UIConfirmTIps, info);
    }
    public void ShowConfirmNormal(string _content, string _title, Action _confirmBack, Action _cancelBack, string _confirmStr, string _cancelStr, bool _bForceFirst = false)
    {
        ShowConfirmMain(_content, _title, _confirmBack, _cancelBack, _confirmStr, _cancelStr, _bForceFirst);
    }
    public void ShowConfirm(string _content, string _title, Action _confirmBack, Action _cancelBack = null, bool _bForceFirst = false)
    {
        ShowConfirmMain(_content, _title, _confirmBack, _cancelBack, null, null, _bForceFirst);
    }
    public void ShowNotify(string _content, string _title, Action _confirmBack, bool _bForceFirst = false, string _confirmStr = null)
    {
        ShowConfirmMain(_content, _title, _confirmBack, null, _confirmStr, null, _bForceFirst, true, false);
    }
    #endregion
}
