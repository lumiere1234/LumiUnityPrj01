using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CameraMgr : SingletonAutoMono<CameraMgr>
{
    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;

    private Camera _UICamera;
    public Camera UICamera {
        get { 
            if (_UICamera == null)
            {
                _UICamera = GameObject.Find("UICamera").GetComponent<Camera>();
            }
            return _UICamera; 
        }
        set { _UICamera = value; }
    }
    public void InitCamera()
    {

    }
    private void Awake()
    {
        EventMgr.GetInstance().Register(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
    }
    private void OnDestroy()
    {
        EventMgr.GetInstance().UnRegister(EventDef.SceneLoadCompleteEvent, OnSceneLoadedComplete);
    }

    public void SetCurrentMainCamera()
    {
        _mainCamera = Camera.main;
        AddOverlayCamera(UICamera);
    }

    public void AddOverlayCamera(Camera data)
    {
        var cameraData = MainCamera.GetUniversalAdditionalCameraData();
        cameraData.cameraStack.Add(data);
    }

    // event
    private void OnSceneLoadedComplete(params object[] args)
    {
        SetCurrentMainCamera();
    }
}
