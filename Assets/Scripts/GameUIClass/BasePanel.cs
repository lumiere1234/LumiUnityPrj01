using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    public CanvasGroup canvasGroup
    {
        get
        {
            if (_canvasGroup == null)
            {
                _canvasGroup = GetComponent<CanvasGroup>();
            }
            return _canvasGroup;
        }
    }
    private Canvas _canvas;
    public Canvas canvas
    {
        get
        {
            if (_canvas == null)
            {
                _canvas = GetComponent<Canvas>();
            }
            return _canvas;
        }
    }
    public GUIConfig uiCfg { get; set; }
    public EUISortingType uiType { get; set; }
    public string uiName { get; set; }
    public bool isActive { get; set; }
    private double expiredTime = -1;
    public double destroyTime => uiCfg.destroyTime == 0 ? GameSettingDef.DefaultDestroyUITime : uiCfg.destroyTime;
    // 初始化 注册按钮相关事件
    public virtual void DoInitial()
    {
        canvas.worldCamera = CameraMgr.GetInstance().UICamera;
        expiredTime = -1;
        isActive = false;
    }
    // 显示界面 注册Custom事件
    public virtual void DoShowPanel(params object[] args)
    {
        expiredTime = -1;
        isActive = true;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 1;
        }
    }
    // 隐藏界面 注销Custom事件
    public virtual void DoHidePanel()
    {
        expiredTime = DateTime.UtcNow.Ticks / 10000000.0;
        isActive = false;
        if (canvasGroup != null)
        {
            canvasGroup.alpha = 0;
        }
    }
    // 销毁界面 注销按钮相关事件
    public virtual void DestroyPanel()
    {
        Destroy(gameObject);
    }
    public bool CheckIsAlive(double curTime)
    {
        if (expiredTime < 0)
            return true;
        return (expiredTime + destroyTime) > curTime;
    }
    protected virtual void Awake()
    {

    }
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    protected virtual void LateUpdate()
    {

    }
    protected virtual void OnEnable()
    {

    }
    protected virtual void OnDisable()
    {

    }
    protected virtual void OnDestroy()
    {

    }
}
