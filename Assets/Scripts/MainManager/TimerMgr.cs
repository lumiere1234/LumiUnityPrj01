using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using UnityEngine;

public class TimerData
{
    public int repeat; // repeat <= 0 无限触发
    public float duration;
    public object[] args;
    public TimerData(float duration = 0, int repeat = 0)
    {
        this.repeat = repeat;
        this.duration = duration;
        this.args = null;
    }
    public void SetArgs(params object[] args)
    {
        this.args = args;
    }
}
enum ETimerNodeType
{
    NoParam,
    WithParam,
}
class TimerNode
{
    public int timerId; // 标识这个Timer的唯一ID
    private ETimerNodeType type = ETimerNodeType.NoParam;
    private TimerMgr.TimerHandler timerHandler;
    private TimerMgr.TimerHanderNoParam timerHanderNoParam;
    private TimerData timerData;
    private float Duration => timerData.duration;
    private float Repeat => timerData.repeat;
    private object[] args => timerData.args;

    private bool _isRemoved = false;
    public bool isRemoved => _isRemoved;
    public float passedTime;
    public float curTime;
    public float curCount;
    public TimerNode(int timerId, TimerMgr.TimerHandler handler, TimerData td)
    {
        type = ETimerNodeType.WithParam;
        this.timerHandler = handler;
        this.timerId = timerId;
        this.timerData = td;

        this._isRemoved = false;
        passedTime = 0;
        curTime = 0; // 0 - duration
        curCount = 0;
    }
    public TimerNode(int timerId, TimerMgr.TimerHanderNoParam handler, TimerData td)
    {
        type = ETimerNodeType.NoParam;
        this.timerHanderNoParam = handler;
        this.timerId = timerId;
        this.timerData = td;

        this._isRemoved = false;
        passedTime = 0;
        curTime = 0; // 0 - duration
        curCount = 0;
    }

    public void DoRemove()
    {
        _isRemoved = true;
    }
    public void UpdateTimer(float dt)
    {
        curTime += dt;
        if (curTime >= Duration)
        {
            if (type == ETimerNodeType.NoParam)
            {
                timerHanderNoParam?.Invoke();
            }
            else if(type == ETimerNodeType.WithParam)
            {
                timerHandler?.Invoke(args);
            }
            curCount++;
            curTime = 0;

            // set remove
            if (Repeat > 0 && curCount >= Repeat)
            {
                DoRemove();
            }
        }
    }
}
public class TimerMgr : SingletonAutoMono<TimerMgr>
{
    public delegate void TimerHandler(params object[] args);
    public delegate void TimerHanderNoParam();
    private Dictionary<int, TimerNode> timers = null;

    private List<TimerNode> removeList = null;
    private List<TimerNode> addTimersList = null;

    private int uniqueId = 1;

    private void Awake()
    {
        timers = new Dictionary<int, TimerNode>();
        uniqueId = 1;
        removeList = new List<TimerNode>();
        addTimersList = new List<TimerNode>();
    }
    
    // 下一帧执行一次
    public int CreateNextFrame(TimerHanderNoParam func)
    {
        int newId = uniqueId++;
        TimerNode node = new TimerNode(newId, func, new TimerData(0.01f, 1));
        addTimersList.Add(node);
        return newId;
    }
    public int CreateNextFrame(TimerHandler func)
    {
        int newId = uniqueId++;
        TimerNode node = new TimerNode(newId, func, new TimerData(0.01f, 1));
        addTimersList.Add(node);
        return newId;
    }
    // 创建Timer并开始等待执行
    public int CreateTimer(TimerHanderNoParam func, TimerData timeDef)
    {
        int newId = uniqueId++;
        TimerNode node = new TimerNode(newId, func, timeDef);
        addTimersList.Add(node);
        return newId;
    }

    public int CreateTimer(TimerHandler func, TimerData timeDef)
    {
        int newId = uniqueId++;
        TimerNode node = new TimerNode(newId, func, timeDef);
        addTimersList.Add(node);
        return newId;
    }
    public void RemoveTimer(int timerId)
    {
        if (timers.ContainsKey(timerId))
        {
            timers[timerId].DoRemove();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // add new timers
        if (addTimersList.Count > 0)
        {
            for (int i = 0; i < addTimersList.Count; i++)
            {
                timers.Add(addTimersList[i].timerId, addTimersList[i]);
            }
            addTimersList.Clear();
        }
        if (removeList.Count > 0)
        {
            for(int i = 0; i < removeList.Count; i++)
            {
                timers.Remove(removeList[i].timerId);
            }
            removeList.Clear();
        }

        foreach(var timer in timers.Values)
        {
            if (timer.isRemoved)
            {
                removeList.Add(timer);
                continue;
            }
            timer.UpdateTimer(Time.deltaTime);
        }
    }

    public void ResetManager()
    {
        timers.Clear();
        uniqueId = 1;
        addTimersList.Clear();
        removeList.Clear();
    }
}
