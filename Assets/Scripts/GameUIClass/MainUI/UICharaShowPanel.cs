using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICharaShowPanel : BasePanel
{
    [SerializeField] private Button BtnReturn;

    protected override void Awake()
    {
        base.Awake();
        BtnReturn.onClick.AddListener(OnClickBtnReturn);
    }

    private void OnClickBtnReturn()
    {
        UIMgr.Instance.DoBackPanel(uiName);
    }
}
