using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIMainPanel : BasePanel
{
    [SerializeField] private Button BtnEnter;
    [SerializeField] private Button BtnChara;
    [SerializeField] private Button BtnTeam;
    [SerializeField] private TMP_Text LblLevelTitle;
    [SerializeField] private TMP_Text LblLevel;
    [SerializeField] private TMP_Text LblChara;
    [SerializeField] private GameObject GroupChara;
    [SerializeField] private Image ImgChara;

    // data 
    CharacterCfg _curCharaCfg;
    CharacterCfg curCharaCfg {
        get {
            if (_curCharaCfg == null)
            {
                _curCharaCfg = GameConfigDataBase.GetConfigData<CharacterCfg>(DataMgr.Instance.CurMainHeroineId.ToString());
            }
            return _curCharaCfg;
        }
        set {
            curCharaCfg = value;
        }
    }
    protected override void Awake()
    {
        base.Awake();
        BtnEnter.onClick.AddListener(OnClickBtnEnter);
        BtnChara.onClick.AddListener(OnClickBtnChara);
        BtnTeam.onClick.AddListener(OnClickBtnTeam);
    }
    protected override void RegistCustomEvent()
    {
        base.RegistCustomEvent();
        EventMgr.Instance.Register(EventDef.Main_ChangeDisplayHeroine, OnChangeMainHeroineEvent);
    }
    public override void DoShowPanel(params object[] args)
    {
        base.DoShowPanel(args);

        UpdateData();
        RefreshPanel();       
    }
    private void UpdateData()
    {
        _curCharaCfg = GameConfigDataBase.GetConfigData<CharacterCfg>(DataMgr.Instance.CurMainHeroineId.ToString());
    }
    private void RefreshPanel()
    {
        GroupChara.SetActive(curCharaCfg != null);
        if (curCharaCfg != null)
        {
            // update image
            ImgChara.SetSprite(curCharaCfg.fullIcon);
        }
    }
    protected override void OnDestroy()
    {
        base.OnDestroy();
    }
    private void OnClickBtnEnter()
    {
        UIMgr.Instance.ShowPanel(UIDef.UILevelSelectPanel);
        UIMgr.Instance.SaveFullPanelStack(uiName);
        UIMgr.Instance.HidePanel(uiName);
    }
    private void OnClickBtnChara()
    {
        UIMgr.Instance.ShowPanel(UIDef.UICharaShowPanel);
        UIMgr.Instance.SaveFullPanelStack(uiName);
        UIMgr.Instance.HidePanel(uiName);
    }
    private void OnClickBtnTeam()
    {
        UIMgr.Instance.ShowPanel(UIDef.UICharaTeamPanel);
        UIMgr.Instance.SaveFullPanelStack(uiName);
        UIMgr.Instance.HidePanel(uiName);
    }
    #region Event
    private void OnChangeMainHeroineEvent(params object[] args)
    {
        UpdateData();
        RefreshPanel();
    }
    #endregion
}
