using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ButtonClickScale : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private float scaleFactor = 1.1f;
    public void OnPointerDown(PointerEventData eventData)
    {
        transform.localScale = Vector3.one * scaleFactor;
    }
    public void OnPointerUp(PointerEventData eventData) 
    {
        transform.localScale = Vector3.one;
    }
}
