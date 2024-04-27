using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaShowPanel : BasePanel
{
    [SerializeField] private Button BtnReturn;
    [SerializeField] private Button BtnHome;
    protected override void Awake()
    {
        base.Awake();
        BtnReturn.onClick.AddListener(OnClickBtnReturn);
        BtnHome.onClick.AddListener(OnClickBtnHome);
    }

    private void OnClickBtnReturn()
    {
        UIMgr.Instance.DoBackPanel(uiName);
    }
    private void OnClickBtnHome()
    {
        GameMgr.Instance.BackToMainScene();
    }
}
