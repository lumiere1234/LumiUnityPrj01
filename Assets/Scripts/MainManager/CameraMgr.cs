using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMgr : SingletonAutoMono<CameraMgr>
{
    private Camera _mainCamera;
    public Camera MainCamera => _mainCamera;
    
    public void SetCurrentMainCamera()
    {

    }
}
