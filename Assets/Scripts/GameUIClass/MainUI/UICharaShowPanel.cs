using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaShowPanel : BasePanel
{
    [SerializeField] private Button BtnReturn;
    [SerializeField] private Button BtnHome;
    [SerializeField] private LumiScrollGrid CardGrid;

    List<CharaCardInfo> cardInfos = new List<CharaCardInfo>();
    protected override void Awake()
    {
        base.Awake();
        BtnReturn.onClick.AddListener(OnClickBtnReturn);
        BtnHome.onClick.AddListener(OnClickBtnHome);
        CardGrid.SetScrollGridUpdateFunc(OnScrollListUpdate);
    }
    protected override void RegistCustomEvent()
    {
        base.RegistCustomEvent();
        EventMgr.Instance.Register(EventDef.CharaCard_AddCard, OnEvent_CharaCard_AddCard);
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        UpdateData();
        RefreshPanel();
    }
    private void UpdateData()
    {
        cardInfos = CharaCardMgr.Instance.CardInfos;
    }
    private void RefreshPanel()
    {
        CardGrid.ResetList();
        CardGrid.AddItems(0, cardInfos.Count);
        CardGrid.DoForceUpdate(true);
    }
    private void OnClickBtnReturn()
    {
        UIMgr.Instance.DoBackPanel(uiName);
    }
    private void OnClickBtnHome()
    {
        GameMgr.Instance.BackToMainScene();
    }
    private void OnScrollListUpdate(LumiScrollItem item, int index, int typeId)
    {
        // card
        CharaCardInfo cardInfo1 = cardInfos[index];
        UICharaCard cardUI = item.gameObject.GetComponent<UICharaCard>();
        if (cardUI == null)
            return;
        cardUI.SetCardInfo(cardInfo1);
    }
    public override void DoHidePanel()
    {
        base.DoHidePanel();
        CardGrid.ResetList();
    }

    // event
    private void OnEvent_CharaCard_AddCard(params object[] args)
    {
        UpdateData();
        RefreshPanel();
    }
}
