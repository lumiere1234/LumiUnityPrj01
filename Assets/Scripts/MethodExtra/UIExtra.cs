using CoreManager;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public static class UIExtra
{
    public static void AddTrigger(this Image self, EventTrigger trigger, Action<BaseEventData> onclick)
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((data) => { onclick(data); });
        trigger.triggers.Add(entry);
    }
    public static void SetSprite(this Image self, string spriteName)
    {
        Sprite sprite = ResManager.Instance.LoadSprite(spriteName);
        self.sprite = sprite;
    }
}
