using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ResizeFullImage : MonoBehaviour
{
    // resize类型
    public enum EFullType
    {
        Shrink, // 收缩
        Expansion, // 拉伸
    }
    [SerializeField] private EFullType fullType = EFullType.Expansion;
    public EFullType mFullType { 
        get 
        { 
            return fullType; 
        }
        set
        {
            if (fullType != value)
            {
                fullType = value;
                ResizePanel();
            }
        }
    }
    private RectTransform _curRect;
    private RectTransform CurRect
    {
        get
        {
            if (_curRect == null)
            {
                _curRect = GetComponent<RectTransform>();
            }
            return _curRect;
        }
    }
    private void OnEnable()
    {
        ResizePanel();
    }

    private void ResizePanel()
    {
        var aspect = (float)Screen.height / Screen.width;
        //Debug.Log($"Lumiere Height :{Screen.height} {Screen.width}");
        Rect rect = CurRect.rect;
        //Debug.Log($"Lumi rect : {rect.height} {rect.width}");
        var curAspect = (float)rect.height / rect.width;
        if (mFullType == EFullType.Shrink)
        {
            if (aspect < curAspect)
            {
                float newScale = aspect / curAspect;
                transform.localScale = Vector3.one * newScale;
            }
        }
        else if (mFullType == EFullType.Expansion)
        {
            if (aspect > curAspect)
            {
                float newScale = aspect / curAspect;
                transform.localScale = Vector3.one * newScale;
            }
        }
    }
}
