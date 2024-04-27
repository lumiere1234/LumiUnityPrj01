using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILevelSelect : BasePanel
{
    [SerializeField] private Button BtnReturn;
    [SerializeField] private Button BtnHome;
    [SerializeField] private TMPro.TMP_Text lblTitle;
    [SerializeField] private UILevelItem[] levelItems;
    protected override void Awake()
    {
        base.Awake();
        BtnReturn.onClick.AddListener(OnClickBtnReturn);
        BtnHome.onClick.AddListener(OnClickBtnHome);
    }

    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        RefreshPanel();
    }
    private void RefreshPanel()
    {
        for (int i = 0; i < levelItems.Length; i++)
        {
            levelItems[i].RefreshLevelData(i);
        }
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
