using CoreManager;
using UnityEngine;
using UnityEngine.UI;

public class UIMainHead : BasePanel
{
    [SerializeField] private Image imgHead;

    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        RefreshPanel();
    }
    private void RefreshPanel()
    {
        imgHead.sprite = ResManager.GetInstance().LoadSprite("Head_Halloween.jpg");
    }
}
