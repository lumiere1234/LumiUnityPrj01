using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class UIConfirmTips : BasePanel
{
    [SerializeField] private Image imgBG;
    [SerializeField] private TMPro.TMP_Text lblTitle;
    [SerializeField] private TMPro.TMP_Text lblContent;
    [SerializeField] private TMPro.TMP_Text lblConfirm;
    [SerializeField] private TMPro.TMP_Text lblCancel;
    [SerializeField] private Button btnConfirm;
    [SerializeField] private Button btnCancel;

    private Queue<ConfirmInfo> nextInfoQueue = new Queue<ConfirmInfo>();
    private ConfirmInfo curInfo;

    private Action curConfirmAction => curInfo?.ConfirmCallback;
    private Action curCancelAction => curInfo?.CancelCallback;
    private string TitleStr => curInfo?.Title ?? StringDef.TipsTitleDefaultStr;
    private string ContentStr => curInfo?.Content ?? string.Empty;
    private bool bShowConfirm => curInfo?.bShowConfirm ?? true;
    private bool bShowCancel => curInfo?.bShowCancel ?? true;
    private string ConfirmStr => curInfo?.ConfirmStr ?? StringDef.TipsComfirmDefaultStr;
    private string CancelStr => curInfo?.CancelStr ?? StringDef.TipsCancelDefaultStr;
    protected override void Awake()
    {
        base.Awake();

        EventTrigger trigger = imgBG.GetComponent<EventTrigger>();
        imgBG.AddTrigger(trigger, OnClickBG);
        btnConfirm.onClick.AddListener(OnClickBtnConfirm);
        btnCancel.onClick.AddListener(OnClickBtnCancel);
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        if (args.Length == 0) return;

        ConfirmInfo info = args[0] as ConfirmInfo;
        if (info.bForceFirst)
        {
            if (curInfo != null)
            {
                nextInfoQueue.Enqueue(curInfo);
            }
            curInfo = info;
            RefreshPanel();
        }
        else
        {
            nextInfoQueue.Enqueue(info);
            if (curInfo == null)
            {
                curInfo = nextInfoQueue.Dequeue();
                RefreshPanel();
            }
        }
    }
    private void DoNextInfo()
    {
        if (nextInfoQueue.Count == 0)
        {
            DoHidePanel();
            return;
        }
        curInfo = nextInfoQueue.Dequeue();
        RefreshPanel();
    }
    private void RefreshPanel()
    {
        lblTitle.text = TitleStr;
        lblContent.text = ContentStr;
        btnConfirm.gameObject.SetActive(bShowConfirm);
        btnCancel.gameObject.SetActive(bShowCancel);
        if (bShowConfirm)
            lblConfirm.text = ConfirmStr;
        if (bShowCancel)
            lblCancel.text = CancelStr;
    }
    private void OnClickBG(BaseEventData data)
    {
        OnClickBtnCancel();
    }
    public override void DoHidePanel()
    {
        base.DoHidePanel();

        nextInfoQueue.Clear();
        curInfo = null;
    }
    private void OnClickBtnCancel()
    {
        curCancelAction?.Invoke();
        DoNextInfo();
    }
    private void OnClickBtnConfirm()
    {
        curConfirmAction?.Invoke();
        DoNextInfo();
    }
}
