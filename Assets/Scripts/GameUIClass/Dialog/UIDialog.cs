using CoreManager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDialog : BasePanel
{
    [SerializeField] private Image imgCharacter;
    [SerializeField] private TMPro.TMP_Text txtName;
    [SerializeField] private WordPrint txtContent;
    [SerializeField] private GameObject imgNameBG;
    [SerializeField] private Image imgBG;
    [SerializeField] private Toggle togAuto;

    UnityEngine.UIElements.ScrollView mm;

    protected override void Awake()
    {
        base.Awake();

        EventTrigger trigger = imgBG.GetComponent<EventTrigger>();
        imgBG.AddTrigger(trigger, OnClickBG);
        togAuto.onValueChanged.AddListener(OnClickToggleAuto);
    }
    public override void DoInitial()
    {   
        base.DoInitial();
    }
    protected override void RegistCustomEvent()
    {
        base.RegistCustomEvent();
        EventMgr.GetInstance().Register(EventDef.Dialog_RefreshPanel, OnEventRefreshPanel);
        DialogMgr.GetInstance().m_UIDialog = this;
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);
        InitPanelState();
        RefreshPanel();
    }
    private void InitPanelState()
    {
        togAuto.SetIsOnWithoutNotify(DialogMgr.GetInstance().bAutoFlag);
    }
    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnCheckNextClick();
        }
    }
    private void RefreshPanel()
    {
        DialogInfo curInfo = DialogMgr.GetInstance().CurDialog;
        if (curInfo == null)
            return;

        Sprite sprite = null;
        string showName = string.Empty;
        bool bShowNameBG = false;
        bool bShowSprite = false;
        if (curInfo.DialogType == EDialogChatType.Character)
        {
            var charaCfg = curInfo.charaCfg;
            sprite = ResManager.GetInstance().LoadSprite(charaCfg.dialogSmallIcon);
            showName = charaCfg.name;
            bShowNameBG = true;
            bShowSprite = true;
        }
        else
        {

        }
        imgNameBG.SetActive(bShowNameBG);
        txtName.text = showName;
        imgCharacter.gameObject.SetActive(bShowSprite);
        if (bShowSprite) imgCharacter.sprite = sprite;
        txtContent.SetPrintTxt(curInfo.dataCfg.dialogStr);
        //txtContent.text = curInfo.dataCfg.dialogStr;
    }
    private void OnClickBG(BaseEventData data)
    {
        OnCheckNextClick();
    }
    private void OnCheckNextClick()
    {
        if (txtContent.CheckClickShow())
        {
            return; 
        }
        DialogMgr.GetInstance().DoNextDialog();
    }
    protected override void UnregistCustomEvent()
    {
        base.UnregistCustomEvent();
        DialogMgr.GetInstance().m_UIDialog = null;
        EventMgr.GetInstance().UnRegister(EventDef.Dialog_RefreshPanel, OnEventRefreshPanel);
    }
    public override void DoHidePanel()
    {
        base.DoHidePanel();
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
