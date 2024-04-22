using CoreManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog : BasePanel
{
    [SerializeField] private Image imgCharacter;
    [SerializeField] private TMPro.TMP_Text txtName;
    [SerializeField] private TMPro.TMP_Text txtContent;

    private int CurId = -1; // 当前对话ID
    private DialogInfo dialogInfo = null;
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        if (args.Length > 0)
        {
            CurId = (int)args[0];
        }
        UpdateData();
        RefreshPanel();
    }

    private void UpdateData()
    {
        dialogInfo = DialogMgr.GetInstance().GetDialogInfo(CurId);
    }
    private void RefreshPanel()
    {
        if (dialogInfo == null)
            return;

        var charaCfg = dialogInfo.charaCfg;
        imgCharacter.sprite = ResManager.GetInstance().LoadSprite(charaCfg.iconName);
        txtName.text = charaCfg.name;
        txtContent.text = dialogInfo.dataCfg.dialogStr;
    }
}
