using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainTIps : BasePanel
{
    [SerializeField] private GameObject PanelTips;
    [SerializeField] private GameObject TipsRoot;
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);
    }
}
