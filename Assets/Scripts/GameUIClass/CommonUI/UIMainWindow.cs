using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CoreManager;

public class UIMainWindow : BasePanel
{
    [SerializeField] private Button BtnEnter;
    [SerializeField] private Button BtnChara;
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
    }
    protected override void RegistCustomEvent()
    {
        base.RegistCustomEvent();
        EventMgr.Instance.Register(EventDef.ChangeMainHeroineId, OnChangeMainHeroineEvent);
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
        Debug.Log("Lumiere enter");
    }
    private void OnClickBtnChara()
    {
        Debug.Log("Lumiere Chara");
    }
    #region Event
    private void OnChangeMainHeroineEvent(params object[] args)
    {
        UpdateData();
        RefreshPanel();
    }
    #endregion
}
