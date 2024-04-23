using CoreManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDialog : BasePanel
{
    [SerializeField] private Image imgCharacter;
    [SerializeField] private TMPro.TMP_Text txtName;
    [SerializeField] private TMPro.TMP_Text txtContent;
    [SerializeField] private Image imgBG;
    [SerializeField] private Toggle togAuto;
    public override void DoInitial()
    {   
        base.DoInitial();

        EventTrigger trigger = imgBG.GetComponent<EventTrigger>();
        imgBG.AddTrigger(trigger, OnClickBG);
        togAuto.onValueChanged.AddListener(OnClickToggleAuto);
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);
        EventMgr.GetInstance().Register(EventDef.Dialog_RefreshPanel, OnEventRefreshPanel);
        DialogMgr.GetInstance().m_UIDialog = this;

        InitPanelState();
        RefreshPanel();
    }
    private void InitPanelState()
    {
        togAuto.SetIsOnWithoutNotify(DialogMgr.GetInstance().bAutoFlag);
    }
    private void RefreshPanel()
    {
        DialogInfo curInfo = DialogMgr.GetInstance().CurDialog;
        if (curInfo == null)
            return;

        var charaCfg = curInfo.charaCfg;
        imgCharacter.sprite = ResManager.GetInstance().LoadSprite(charaCfg.iconName);
        txtName.text = charaCfg.name;
        WordPrint wp = txtContent.gameObject.GetComponent<WordPrint>();
        wp.SetPrintTxt(curInfo.dataCfg.dialogStr);
        //txtContent.text = curInfo.dataCfg.dialogStr;
    }
    private void OnClickBG(BaseEventData data)
    {
        DialogMgr.GetInstance().DoNextDialog();
    }
    public override void DoHidePanel()
    {
        base.DoHidePanel();
        DialogMgr.GetInstance().m_UIDialog = null;
        EventMgr.GetInstance().UnRegister(EventDef.Dialog_RefreshPanel, OnEventRefreshPanel);
    }
    #region Event
    // event
    private void OnEventRefreshPanel(params object[] args)
    {
        RefreshPanel();
    }
    private void OnClickToggleAuto(bool bAuto)
    {
        DialogMgr.GetInstance().DoSetAuto(bAuto);
    }
    #endregion
}
