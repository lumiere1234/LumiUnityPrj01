using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * 1.1f;
    }
    public void OnPointerUp(PointerEventData eventData) 
    {
        transform.localScale = Vector3.one;
    }
}
