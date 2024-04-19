using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class EventData {
    private HashSet<EventInfo> events;
    public EventDef eventType { get; set; } = EventDef.Default;
    public EventData()
    {
        this.eventType = EventDef.Default;
    }
    public EventData(EventDef eventType)
    {
        this.eventType = eventType;
    }
    public void Invoke(params object[] args)
    {
        if (events == null)
            return;

        foreach (var evt in events)
        {
            evt.Invoke(args);
        }
    }
    public void RegistEvent(EventInfo info)
    {
        if (events == null)
            events = new HashSet<EventInfo>();
        events.Add(info);
    }
    public void UnRegistEvent(EventInfo info)
    {
        if (events == null)
            return;
        if (events.Contains(info))
        {
            events.Remove(info);
        }

        if (events.Count == 0)
        {
            EventMgr.GetInstance().RemoveKey(eventType);
        }
    }
}

public delegate void EventInfo(params object[] args);
public class EventMgr : SingletonAutoMono<EventMgr>
{
    private Dictionary<EventDef, EventData> EventDict = new Dictionary<EventDef, EventData>();
    public void Register(EventDef eType, EventInfo eventInfo)
    {
        if (!EventDict.ContainsKey(eType))
        {
            var eData = new EventData(eType);
            EventDict.Add(eType, eData);
        }
        EventDict[eType].RegistEvent(eventInfo);
    }
    public void UnRegister(EventDef key, EventInfo eventInfo)
    {
        if (EventDict.ContainsKey(key))
        {
            EventDict[key].UnRegistEvent(eventInfo);
        }
    }
    public void ClearAll()
    {
        EventDict.Clear();
    }
    public void Invoke(EventDef eType, params object[] args)
    {
        if(EventDict.ContainsKey(eType))
        {
            EventDict[eType].Invoke(args);
        }
    }
    public void RemoveKey(EventDef eType)
    {
        if (EventDict.ContainsKey(eType))
        { 
            EventDict.Remove(eType);
        }
    }
}
