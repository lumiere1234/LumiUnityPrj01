using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITestPanel : BasePanel
{
    [SerializeField] private Button btnShow;
    [SerializeField] private Button btnHide;
    protected override void Awake()
    {
        btnShow.onClick.AddListener(OnClickBtnShow);
        btnHide.onClick.AddListener(OnClickBtnHide);
    }
    private void OnClickBtnShow()
    {
        UIMgr.GetInstance().ShowPanel("UI-MainPlayer");
    }
    private void OnClickBtnHide()
    {
        UIMgr.GetInstance().HidePanel("UI-MainPlayer");
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
        btnShow.onClick.RemoveAllListeners();
        btnHide.onClick.RemoveAllListeners();
    }
}
